﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using MoonLib.entity;
using MoonLib.util;
using MoonLib.entity.message;
using MoonLib.core.cmm;
using System.Threading;
using MoonLib.core.common;

namespace MoonLib.core
{
    /// <summary>
    /// moon客户端类
    /// </summary>
    internal class MoonClient : IMoonClient
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        private string client_id;

        /// <summary>
        /// 发送消息同步锁
        /// </summary>
        private object sendMsgSyncLock = new object();

        /// <summary>
        /// 上一次发送的消息时间
        /// </summary>
        private long lastSendMessageTime;

        /// <summary>
        /// 上一次发送的消息服务端是否回复
        /// </summary>
        private bool lastSentMessageHasReply = true;

        /// <summary>
        /// 上一次发送的系统消息id
        /// </summary>
        private string lastSentMessageId = string.Empty;

        /// <summary>
        /// 消息队列
        /// </summary>
        private Queue<Message> msgQueue = new Queue<Message>();

        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private DefaultCommunicator defaultCommunicator;


        public string getClientId()
        {
            return this.client_id;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnectServer(string clientId,string ip, int port)
        {

            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("client id can not be null");
            }
            this.client_id = clientId;

            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(ipAddress, port);
            try
            {
                clientSocket.Connect(point);
            }
            catch (Exception e)
            {
                throw e;
            }

            //连接成功，发送第一条默认注册客户端消息
            SendMessage(GetInitConnectMessage(clientId));

            //开启接收消息线程
            Thread th = new Thread(RecvServerMessage);
            th.IsBackground = true;
            th.Start();

        }

        /// <summary>
        /// 向服务器发送消息
        /// 消息报文格式定义：
        /// 消息的前4个字节为消息的头部定义：MNPH
        /// 消息的第4-7字节为消息报文的大小，该消息报文的大小并不包括消息头部定义大小
        /// </summary>
        /// <param name="message"></param>
        internal void SendMessage(Message message)
        {
            lock (sendMsgSyncLock)
            {
                //设置客户端id
                message.message_head.client_id = this.client_id;
                //设置发送时间
                message.message_head.msg_time = DateTimeUtil.GetTimeStamp();
                //设置消息id
                message.message_head.msg_id = CreateMsgId("NodeOne");

                string messageStr = JsonConvert.SerializeObject(message);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(messageStr);

                byte[] headFlag = System.Text.Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_HEAD_FLAG);

                byte[] tailFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG);

                byte[] dataLength = new byte[4];
                string strDataLength = byteArray.Length.ToString().PadLeft(4, '0');//数字转化成4位字符串，不足在前面补”0“

                dataLength = System.Text.Encoding.UTF8.GetBytes(strDataLength);

                byte[] pkg = new byte[byteArray.Length + headFlag.Length + tailFlag.Length + dataLength.Length];

                int pkgPos = 0;

                for (pkgPos = 0; pkgPos < headFlag.Length; pkgPos++)
                {
                    pkg[pkgPos] = headFlag[pkgPos];
                }

                for (int i = 0; i < dataLength.Length; i++, pkgPos++)
                {
                    pkg[pkgPos] = dataLength[i];
                }

                for (int i = 0; i < byteArray.Length; i++, pkgPos++)
                {
                    pkg[pkgPos] = byteArray[i];
                }
                for (int i = 0; i < tailFlag.Length; i++,pkgPos++)
                {
                    pkg[pkgPos] = tailFlag[i];
                }
                if (message.message_head.main_msg_num == MoonProtocol.SYS_MAIN_PROTOCOL_CONNECT_INIT) 
                {
                    clientSocket.Send(pkg);
                    return;
                }
                long responeTime = long.Parse(DateTimeUtil.GetTimeStamp()) - this.lastSendMessageTime;
                //如果服务器响应时间在允许的范围之内，那么将待发送的消息放入消息队列
                if (!(this.lastSentMessageHasReply || (responeTime >= Constant.MSG_SERVER_REPLY_TIMEOUT)))
                {
                    LogUtil.Debug("服务器对于上次消息未响应", "服务器对于上次消息未响应");
                    this.msgQueue.Enqueue(message);
                    return;
                }
                //如果服务器响应时间在不在允许的范围之内，那么通知第三方可能连接丢失或者服务器卡
                if (!(this.lastSentMessageHasReply || (responeTime < Constant.MSG_SERVER_REPLY_TIMEOUT)))
                {
                    LogUtil.Debug("服务器对于上次消息未响应", "服务器对于上次消息未响应");
                    Message message2 = new Message
                    {
                        message_head =
                        {
                            main_msg_num = MoonProtocol.LocalProtocol.SYS_MAIN_PROTOCOL_SERVER_NOT_REPLY,
                            sub_msg_num = -1
                        },
                        message_body = { content = this.lastSentMessageId }
                    };
                    this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message2);
                    return;
                }
                clientSocket.Send(pkg);
                lastSentMessageId = message.message_head.msg_id;
                this.lastSentMessageHasReply = false;
            }
        }

        /// <summary>
        /// 接收服务端消息
        /// </summary>
        private void RecvServerMessage()
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[MoonProtocol.Packge.PKG_BYTE_MAX_LENGTH];
                    int count = this.clientSocket.Receive(buffer);
                    string str = Encoding.UTF8.GetString(buffer, 0, count);
                    int length = -1;
                    int pos_tail_flag = -1;
                    while (!string.IsNullOrEmpty(str))
                    {
                        length = str.IndexOf(MoonProtocol.Packge.PKG_HEAD_FLAG);
                        if (length == -1)
                        {
                            builder.Append(str);
                            str = "";
                        }
                        else
                        {
                            if (length > 0)
                            {
                                builder.Append(str.Substring(0, length));
                                str = str.Substring(length);
                            }
                            for (int i = 0; i < MoonProtocol.Packge.PKG_HEAD_LENGTH; i++)
                            {
                                builder.Append(str[i]);
                            }
                            str = str.Substring(MoonProtocol.Packge.PKG_HEAD_LENGTH);
                        }
                    }
                    for (pos_tail_flag = builder.ToString().IndexOf(MoonProtocol.Packge.PKG_TAIL_FLAG); pos_tail_flag != -1; pos_tail_flag = builder.ToString().IndexOf(MoonProtocol.Packge.PKG_TAIL_FLAG))
                    {
                        length = builder.ToString().IndexOf(MoonProtocol.Packge.PKG_HEAD_FLAG);
                        if (length < pos_tail_flag)
                        {
                            this.DataPackageParse(builder.ToString().Substring(length, pos_tail_flag + MoonProtocol.Packge.PKG_TAIL_LENGTH));
                            builder = builder.Remove(length, pos_tail_flag + MoonProtocol.Packge.PKG_TAIL_LENGTH);
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (this.defaultCommunicator.GetMessageCallback() != null)
                    {
                        Message message = new Message
                        {
                            message_head =
                            {
                                main_msg_num = MoonProtocol.CommunicationException.SYS_MAIN_PROTOCOL_MSG,
                                sub_msg_num = MoonProtocol.CommunicationException.SYS_SUB_PROTOCOL_OUT_CONNECT
                            },
                            message_body = { content = exception.Message }
                        };
                        this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message);
                    }
                }
            }

        }

        /// <summary>
        /// 处理数据包
        /// </summary>
        /// <param name="strMessage"></param>
        private void DataPackageParse(string strMessage)
        {
            strMessage = strMessage.Replace(MoonProtocol.Packge.PKG_HEAD_FLAG, "").Replace(MoonProtocol.Packge.PKG_TAIL_FLAG, "");
            strMessage = strMessage.Substring(4);
            LogUtil.Info("消息缓存：", strMessage);
            Message message = JsonConvert.DeserializeObject<Message>(strMessage);
            bool flag = this.UserDealMessage(message);
            if (!flag)
            {
                this.DealSystemMessage(message);
            }
            if ((this.defaultCommunicator.GetMessageCallback() != null) && flag)
            {
                this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message);
            }
        }


        /// <summary>
        /// 判断是否是用户处理消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool UserDealMessage(Message message)
        {
            if (message.message_head.main_msg_num == MoonProtocol.ServerReply.SYS_MAIN_PROTOCOL_REPLY)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 处理系统默认消息
        /// </summary>
        /// <param name="message"></param>
        private void DealSystemMessage(Message message)
        {
            //如果是系统回复消息
            if (MoonProtocol.ServerReply.SYS_MAIN_PROTOCOL_REPLY.Equals(message.message_head.main_msg_num))
            {
                lock (this.sendMsgSyncLock)
                {
                    this.lastSentMessageHasReply = true;
                }
                //从队列中取出一条消息进行发送
                if (this.msgQueue.Count > 0)
                {
                    this.SendMessage(this.msgQueue.Dequeue());
                }
            }
        }


        /// <summary>
        /// 获取初始化连接
        /// </summary>
        /// <returns></returns>
        private Message GetInitConnectMessage(string clientId)
        {
            Message message = new Message();
            message.message_head.msg_id = UUIDUtil.Generator32UUID();
            message.message_head.main_msg_num = MoonProtocol.SYS_MAIN_PROTOCOL_CONNECT_INIT;
            message.message_head.sub_msg_num = MoonProtocol.SYS_SUB_PROTOCOL_CLIENT_CON;
            ClientEnvironment clientEnvironment = new ClientEnvironment();
            clientEnvironment.client_platform = "windows";
            clientEnvironment.client_sdk_version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            clientEnvironment.connect_sdk_token = "";
            clientEnvironment.client_id = clientId;
            clientEnvironment.opra_system_version = System.Environment.OSVersion.VersionString;
            message.message_body.content = clientEnvironment;
            return message;
        }

        /// <summary>
        /// 创建消息id，消息id的命名规则：通信服务节点名称+ 32位uuid
        /// </summary>
        /// <returns></returns>
        private string CreateMsgId(string serverNodeName)
        {
            return serverNodeName + "-" + UUIDUtil.Generator32UUID();
        }


        public cmm.ICommunicator GetCommunicator()
        {
            if (defaultCommunicator == null)
            {
                defaultCommunicator = new DefaultCommunicator(this);
            }
            return defaultCommunicator;
        }

        public void Close()
        {
            clientSocket.Close();
        }

    }
}
