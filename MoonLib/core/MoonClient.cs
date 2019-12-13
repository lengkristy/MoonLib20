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
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private DefaultCommunicator defaultCommunicator;

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnectServer(string ip, int port)
        {
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
            SendMessage(GetInitConnectMessage());

            //开启接收消息线程
            Thread th = new Thread(RecvServerMessage);
            th.IsBackground = true;
            th.Start();

        }

        /// <summary>
        /// 向服务器发送消息
        /// </summary>
        /// <param name="message"></param>
        internal void SendMessage(Message message)
        {
            string messageStr = JsonConvert.SerializeObject(message);
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(messageStr);
            clientSocket.Send(byteArray);
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
                    if (this.defaultCommunicator.GetMessageCallback() != null)
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
        /// 获取初始化连接
        /// </summary>
        /// <returns></returns>
        private Message GetInitConnectMessage()
        {
            Message message = new Message();
            message.message_head.msg_id = UUIDUtil.Generator32UUID();
            message.message_head.main_msg_num = MoonProtocol.SYS_MAIN_PROTOCOL_CONNECT_INIT;
            message.message_head.sub_msg_num = MoonProtocol.SYS_SUB_PROTOCOL_CLIENT_CON;
            ClientEnvironment clientEnvironment = new ClientEnvironment();
            clientEnvironment.client_platform = "windows";
            clientEnvironment.client_sdk_version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            clientEnvironment.connect_sdk_token = "";
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
