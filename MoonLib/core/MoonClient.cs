using System;
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
                string messageStr = JsonConvert.SerializeObject(message);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(messageStr);

                byte[] headFlag = System.Text.Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_HEAD_FLAG);

                byte[] dataLength = new byte[4];
                string strDataLength = byteArray.Length.ToString().PadLeft(4, '0');//数字转化成4位字符串，不足在前面补”0“

                dataLength = System.Text.Encoding.UTF8.GetBytes(strDataLength);

                byte[] pkg = new byte[byteArray.Length + headFlag.Length + dataLength.Length];

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

                if (message.message_head.main_msg_num == MoonProtocol.SYS_MAIN_PROTOCOL_CONNECT_INIT) 
                {
                    clientSocket.Send(pkg);
                    return;
                }
                //如果发送的消息不是连接初始化消息，那么需要设置等待服务端回复
                if (!this.lastSentMessageHasReply)
                {
                    //需要将消息放入队列，等待发送
                    msgQueue.Enqueue(message);
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
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int n = clientSocket.Receive(buffer);
                    string strMessage = Encoding.UTF8.GetString(buffer, 0, n);
                    Message message = JsonConvert.DeserializeObject <Message>(strMessage);
                    bool isUserMsg = UserDealMessage(message);

                    if (!isUserMsg)
                    {
                        DealSystemMessage(message);
                    }

                    if (this.defaultCommunicator.GetMessageCallback() != null && isUserMsg)
                    {
                        this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message);
                    }

                    
                }
                catch (Exception e)
                {
                    if (this.defaultCommunicator.GetMessageCallback() != null)
                    {
                        Message message = new Message();
                        message.message_head.main_msg_num = MoonProtocol.CommunicationException.SYS_MAIN_PROTOCOL_MSG;
                        message.message_head.sub_msg_num = MoonProtocol.CommunicationException.SYS_SUB_PROTOCOL_OUT_CONNECT;
                        message.message_body.content = e.Message;
                        this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message);
                        break;
                    }
                }
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
