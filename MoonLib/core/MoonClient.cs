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
using MoonLib.core.common;
using MoonLib.exp.pkg;

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
        /// 服务端是否同意接受客户端连接
        /// </summary>
        public bool serviceAgreeAccept = false;

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
                if (message.Head.MainMsgNum == MoonProtocol.InitProtocol.MN_PROTOCOL_MAIN_CONNECT_INIT) 
                {
                    clientSocket.Send(pkg);
                    return;
                }
                //如果服务端不同意接受连接则不能发送数据包
                if (!this.serviceAgreeAccept) return;
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
                    Message message2 = new Message();
                    message2.Head = new MessageHead();
                    message2.Head.MainMsgNum = MoonProtocol.LocalProtocol.MN_PROTOCOL_MAIN_SERVER_NOT_REPLY;
                    message2.Body = new MessageBody();
                    message2.Body.Content = this.lastSentMessageId;
                    this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message2);
                    return;
                }
                clientSocket.Send(pkg);
                lastSentMessageId = message.Head.MsgId;
                this.lastSentMessageHasReply = false;
            }
        }

        /// <summary>
        /// 接收服务端消息
        /// </summary>
        private void RecvServerMessage()
        {
            byte[] data = new byte[MoonProtocol.Packge.PKG_BYTE_MAX_LENGTH * 2];//数据缓存
            int currentLen = 0;//当前接收的数据长度
            while (true)
            {
                byte[] buffer = null;
                int count = 0;
                List<string> strDataPkg = null;
                try
                {
                    buffer = new byte[1024]; //每次读取1K的内容
                    count = this.clientSocket.Receive(buffer); //方法会被阻塞

                    //解析数据
                    strDataPkg = ParsePkgData(buffer, count);
                }
                catch (Exception exception)
                {
                    LogUtil.Debug("解析数据出错：", exception.Message + "\r\n" + exception.StackTrace);
                }
                
                try
                {
                    if (strDataPkg != null && strDataPkg.Count > 0)
                    {
                        for (int i = 0; i < strDataPkg.Count; i++)
                        {
                            DataPackageParse(strDataPkg[i]);
                        }
                    }
                    else
                    {
                        Buffer.BlockCopy(buffer, 0, data, currentLen, count);
                        currentLen += count;
                        //拷贝完成之后，查找是否有包尾标识，如果有那么表示有完整数据
                        ProcessCacheDataPkg(ref data,ref currentLen);
                    }
                }
                catch (Exception exception)
                {
                    LogUtil.Error("接收数据错误", exception.Message + "\r\n" + exception.StackTrace);
                    if (this.defaultCommunicator != null && this.defaultCommunicator.GetMessageCallback() != null)
                    {
                        Message message = new Message
                        {
                            Head =
                            {
                                MainMsgNum = MoonProtocol.CommunicationException.MN_PROTOCOL_MAIN_MSG,
                                SubMsgNum = MoonProtocol.CommunicationException.MN_PROTOCOL_SUB_OUT_CONNECT
                            },
                            Body = { Content = exception.Message }
                        };
                        this.defaultCommunicator.GetMessageCallback().ServerMessageHandler(message);
                    }
                }
                Thread.Sleep(10);
            }

        }

        //解析包数据，如果成功返回string json数据包，如果失败则返回null，如果是多条，那么返回list
        private List<string> ParsePkgData(byte[] data,int len)
        {
            //判断包头是否一致
            byte[] headFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_HEAD_FLAG);
            byte[] tailFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG);
            for (int i = 0; i < headFlag.Length; i++)
            {
                if (data[i] != headFlag[i])
                {
                    //LogUtil.Error("解析数据包出错，数据包头部标识错误，数据包内容：", Encoding.UTF8.GetString(data));
                    return null;
                }
            }

            //判断包尾是否一致
            for (int i = len - tailFlag.Length, j = 0; i < len; i++,j++)
            {
                if (tailFlag[j] != data[i])
                {
                    //LogUtil.Error("解析数据包出错，数据包尾部标识错误，数据包内容：", Encoding.UTF8.GetString(data));
                    return null;
                }
            }
            byte[] bData = new byte[len - MoonProtocol.Packge.PKG_HEAD_LENGTH - MoonProtocol.Packge.PKG_TAIL_LENGTH];
            //
            for (int i = MoonProtocol.Packge.PKG_HEAD_LENGTH, j = 0; i < len - tailFlag.Length; i++, j++)
            {
                bData[j] = data[i];
            }

            //数组的第9到12个字节为数据体长度
            byte[] bLen = new byte[4];
            bLen[0] = data[8];
            bLen[1] = data[9];
            bLen[2] = data[10];
            bLen[3] = data[11];
            string sLen = Encoding.UTF8.GetString(bLen);
            string tmpLen = sLen;
            //去掉高位无效的填充0
            for (int i = 0; i < sLen.Length; i++)
            {
                if (sLen[i] != '0')
                    break;
                tmpLen.Remove(i, 1);
            }
            int iLen = Convert.ToInt32(tmpLen);
            if (iLen != len - MoonProtocol.Packge.PKG_HEAD_LENGTH - MoonProtocol.Packge.PKG_TAIL_LENGTH)
            {
                //有可能是多条包在一起
                byte[] tmpData = new byte[len];
                Buffer.BlockCopy(data, 0, tmpData, 0, len);
                List<string> pkgStrList = ParseMultiPkg(tmpData);
                if (pkgStrList == null || pkgStrList.Count == 0)
                {
                    LogUtil.Error("解析的数据包发生错误", Encoding.UTF8.GetString(data));
                    return null;
                }
                return pkgStrList;
            }
            List<string> dataPkgStrList = new List<string>();
            dataPkgStrList.Add(Encoding.UTF8.GetString(bData));
            return dataPkgStrList;
        }

        /// <summary>
        /// 解析多条数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<string> ParseMultiPkg(byte[] data)
        {
            //查找第一次出现包结束标识的位置
            List<string> strPkgList = new List<string>();
            int pos = FindBytesFirstPos(data, Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG));
            while (pos != -1)
            {
                //
                try
                {
                    byte[] tmpData = new byte[pos + MoonProtocol.Packge.PKG_TAIL_LENGTH];
                    for (int i = 0; i < pos + MoonProtocol.Packge.PKG_TAIL_LENGTH; i++)
                    {
                        tmpData[i] = data[i];
                    }
                    string strPkg = ParseSinglePkg(tmpData);
                    if (!string.IsNullOrEmpty(strPkg))
                    {
                        strPkgList.Add(strPkg);
                    }
                    //去掉已经处理的数据包字节
                    if (data.Length - pos - MoonProtocol.Packge.PKG_TAIL_LENGTH > 0)
                    {
                        byte[] remainData = new byte[data.Length - pos - MoonProtocol.Packge.PKG_TAIL_LENGTH];
                        for (int i = pos + MoonProtocol.Packge.PKG_TAIL_LENGTH, j = 0; i < data.Length; i++, j++)
                        {
                            remainData[j] = data[i];
                        }
                        data = remainData;
                        pos = FindBytesFirstPos(data, Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG));
                    }
                    else
                    {
                        break;
                    }
                }
                catch (PkgHeadFlagNotMatchException e)
                {
                    LogUtil.Error("解析多条数据包时发生错误：",e.Message);
                    continue;
                }
                catch (PkgLengthNotMatchException e)
                {
                    LogUtil.Error("解析多条数据包时发生错误：", e.Message);
                    continue;
                }
                catch (PkgTailFlagNotMatchException e)
                {
                    LogUtil.Error("解析多条数据包时发生错误：", e.Message);
                    continue;
                }
            }
            return strPkgList;
        }

        /// <summary>
        /// 解析单条数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string ParseSinglePkg(byte[] data)
        {
            //判断包头是否一致
            byte[] headFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_HEAD_FLAG);
            byte[] tailFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG);
            for (int i = 0; i < headFlag.Length; i++)
            {
                if (data[i] != headFlag[i])
                {
                    //LogUtil.Error("解析数据包出错，数据包头部标识错误，数据包内容：", Encoding.UTF8.GetString(data));
                    throw new PkgHeadFlagNotMatchException("解析数据包出错，数据包头部标识错误，数据包内容：" +  System.Text.Encoding.UTF8.GetString(data));
                }
            }

            //判断包尾是否一致
            for (int i = data.Length - tailFlag.Length, j = 0; i < data.Length; i++, j++)
            {
                if (tailFlag[j] != data[i])
                {
                    //LogUtil.Error("解析数据包出错，数据包尾部标识错误，数据包内容：", Encoding.UTF8.GetString(data));
                    throw new PkgTailFlagNotMatchException("解析数据包出错，数据包尾部标识错误，数据包内容：" + Encoding.UTF8.GetString(data));
                }
            }
            byte[] bData = new byte[data.Length - MoonProtocol.Packge.PKG_HEAD_LENGTH - MoonProtocol.Packge.PKG_TAIL_LENGTH];
            //
            for (int i = MoonProtocol.Packge.PKG_HEAD_LENGTH, j = 0; i < data.Length - tailFlag.Length; i++, j++)
            {
                bData[j] = data[i];
            }

            //数组的第9到12个字节为数据体长度
            byte[] bLen = new byte[4];
            bLen[0] = data[8];
            bLen[1] = data[9];
            bLen[2] = data[10];
            bLen[3] = data[11];
            string sLen = System.Text.Encoding.UTF8.GetString(bLen);
            string tmpLen = sLen;
            //去掉高位无效的填充0
            for (int i = 0; i < sLen.Length; i++)
            {
                if (sLen[i] != '0')
                    break;
                tmpLen.Remove(i, 1);
            }
            int iLen = Convert.ToInt32(tmpLen);
            if (iLen != data.Length - MoonProtocol.Packge.PKG_HEAD_LENGTH - MoonProtocol.Packge.PKG_TAIL_LENGTH)
            {
                //LogUtil.Error("解析数据包出错，解析的数据包的大小和实际大小不一致", Encoding.UTF8.GetString(data));
                //return null;
                throw new PkgLengthNotMatchException("解析数据包出错，解析的数据包的大小和实际大小不一致,数据包内容：" + Encoding.UTF8.GetString(data));
            }
            return System.Text.Encoding.UTF8.GetString(bData);
        }

        /// <summary>
        /// 查找子字节数组第一次出现的位置，没有查找到则返回-1
        /// </summary>
        /// <param name="data"></param>
        /// <param name="findData"></param>
        /// <returns></returns>
        private int FindBytesFirstPos(byte[] data,byte[] findData)
        {
            int pos = -1;
            for (int index = 0; index < data.Length; index++)
            {
                if (index + findData.Length <= data.Length)
                {
                    int i = 0;
                    for (; i < findData.Length; i++)
                    {
                        if (data[index + i] != findData[i])
                        {
                            break;
                        }
                    }
                    if (i == MoonProtocol.Packge.PKG_TAIL_LENGTH)
                    {
                        //index的位置就是当前查找到的位置
                        pos = index;
                        break;
                    }
                }
            }
            return pos;
        }

        //处理缓存包
        private void ProcessCacheDataPkg(ref byte[] data, ref int currentLen)
        {
            byte[] tailFlag = Encoding.UTF8.GetBytes(MoonProtocol.Packge.PKG_TAIL_FLAG);
            int pos = -1;
	        for (int index = 0; index < currentLen; index++) {
		        if (index + MoonProtocol.Packge.PKG_TAIL_LENGTH <= currentLen ){
			        int i = 0;
			        for (; i < MoonProtocol.Packge.PKG_TAIL_LENGTH; i++ ){
				        if (data[index+i] != tailFlag[i]) {
					        break;
				        }
			        }
			        if (i == MoonProtocol.Packge.PKG_TAIL_LENGTH ){
				        //index的位置就是当前查找到的位置
                        pos = index;
                        break;
			        }
		        }
	        }

            if (pos != -1)
            {
                try
                {
                    //查找到包尾结束标识
                    byte[] bData = new byte[pos + MoonProtocol.Packge.PKG_TAIL_LENGTH ];
                    Buffer.BlockCopy(data, 0, bData, 0, pos + MoonProtocol.Packge.PKG_TAIL_LENGTH);

                    //将源字节数据后面的数据保留
                    currentLen = currentLen - pos - MoonProtocol.Packge.PKG_TAIL_LENGTH;
                    if (currentLen > 0)
                    {
                        byte[] remainData = new byte[currentLen];
                        Buffer.BlockCopy(data, pos + MoonProtocol.Packge.PKG_TAIL_LENGTH, remainData, 0, currentLen);

                        //将数据放入缓存
                        data = new byte[MoonProtocol.Packge.PKG_BYTE_MAX_LENGTH * 2];
                        Buffer.BlockCopy(remainData, 0, data, 0, currentLen);

                        List<string> strDataPkg = ParsePkgData(bData, pos + MoonProtocol.Packge.PKG_TAIL_LENGTH + 1);
                        if (strDataPkg != null && strDataPkg.Count > 0)
                        {
                            for (int i = 0; i < strDataPkg.Count; i++)
                            {
                                DataPackageParse(strDataPkg[i]);
                            }
                        }
                        ProcessCacheDataPkg(ref data, ref currentLen);//递归调用，可能存在多条完整数据包
                    }
                    else
                    {
                        //重置当前长度
                        currentLen = 0;
                        //将数据清空
                        data = new byte[MoonProtocol.Packge.PKG_BYTE_MAX_LENGTH * 2];
                    }
                }
                catch (Exception e)
                {
                    LogUtil.Error("处理缓存数据发生错误：", e.Message);
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
            if (message.Head.MainMsgNum == MoonProtocol.ServerReply.MN_PROTOCOL_MAIN_REPLY
                || message.Head.MainMsgNum == MoonProtocol.InitProtocol.MN_PROTOCOL_MAIN_CONNECT_INIT
                || message.Head.MainMsgNum == MoonProtocol.KeepAliveProtocol.MN_PROTOCOL_MAIN_KEEPALIVE)
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
            //如果是系统回复上一次消息
            if (MoonProtocol.ServerReply.MN_PROTOCOL_MAIN_REPLY.Equals(message.Head.MainMsgNum))
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
            else if (MoonProtocol.InitProtocol.MN_PROTOCOL_MAIN_CONNECT_INIT.Equals(message.Head.MainMsgNum))//服务端反馈客户端初始化连接消息
            {
                //服务端同意接收连接
                if (MoonProtocol.InitProtocol.MN_PROTOCOL_SUB_SERVER_ACCEPT.Equals(message.Head.SubMsgNum))
                {
                    this.serviceAgreeAccept = true;
                    defaultCommunicator = new DefaultCommunicator(this);
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
            message.Head = new MessageHead();
            message.Body = new MessageBody();
            message.Head.MsgId = UUIDUtil.Generator32UUID();
            message.Head.MainMsgNum = MoonProtocol.InitProtocol.MN_PROTOCOL_MAIN_CONNECT_INIT;
            message.Head.SubMsgNum = MoonProtocol.InitProtocol.MN_PROTOCOL_SUB_CLIENT_CON;
            ClientEnvironment clientEnvironment = new ClientEnvironment();
            clientEnvironment.ClientPlatform = "windows";
            clientEnvironment.ClientSDKVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            clientEnvironment.ConnectSDKToken = "";
            clientEnvironment.ClientId = clientId;
            clientEnvironment.OpraSystemVersion = System.Environment.OSVersion.VersionString;
            message.Body.Content = JsonConvert.SerializeObject(clientEnvironment);
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
