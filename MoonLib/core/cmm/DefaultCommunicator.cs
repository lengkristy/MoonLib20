using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.entity;
using MoonLib.util;
using MoonLib.entity.message;
using MoonLib.core.cmm.callback;
using Newtonsoft.Json;

namespace MoonLib.core.cmm
{
    /// <summary>
    /// 默认通信器
    /// </summary>
    internal class DefaultCommunicator : ICommunicator,IMessageCallBack
    { 

        private MoonClient moonClient;

        //系统消息回调
        private SysMessageCallback sysMessageCallback;

        //点对点消息回调
        private PTPMessageCallback ptpMessageCallback;

        //广播消息回调
        private BroadcastMessageCallback broadcastMessageCallback;

        internal DefaultCommunicator(MoonClient moonClient)
        {
            this.moonClient = moonClient;
        }

        public void RegistCallback(SysMessageCallback sysMessageCallback, PTPMessageCallback ptpMessageCallback, BroadcastMessageCallback broadcastMessageCallback)
        {
            this.sysMessageCallback = sysMessageCallback;
            this.ptpMessageCallback = ptpMessageCallback;
            this.broadcastMessageCallback = broadcastMessageCallback;
        }


        /// <summary>
        /// 获取消息回调接口
        /// </summary>
        /// <returns></returns>
        internal IMessageCallBack GetMessageCallback()
        {
            return this;
        }


        /// <summary>
        /// 发送文本消息给某个用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="strMsg"></param>
        public void SendTextMessageToUser(string userId, string strMsg)
        {
            Message message = new Message();
            message.Head = new MessageHead();
            message.Body = new MessageBody();
            message.Head.MsgId = CreateMsgId("node1");
            message.Head.MsgOrder = 0;
            message.Head.MsgTime = DateTimeUtil.GetTimeStamp();
            message.Head.MsgSize = strMsg.Length;
            message.Head.MainMsgNum = MoonProtocol.PointToPointMsg.MN_PROTOCOL_MAIN_MSG_POINT_TO_POINT;
            message.Head.SubMsgNum = MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_TEXT;
            
            PTPMessage ptpMsg = new PTPMessage();
            ptpMsg.from_client_id = this.moonClient.getClientId();
            ptpMsg.to_client_id = userId;
            ptpMsg.context = strMsg;
            message.Body.Content = JsonConvert.SerializeObject(ptpMsg);
            this.moonClient.SendMessage(message);
        }


        public void GetServerClientInfoList()
        {
            Message message = new Message();
            message.Head = new MessageHead();
            message.Body = new MessageBody();
            message.Head.ClientId = this.moonClient.getClientId();
            message.Head.MainMsgNum = MoonProtocol.ServeClientInfo.MN_PROTOCOL_MAIN_SCI;
            message.Head.SubMsgNum = MoonProtocol.ServeClientInfo.MN_PROTOCOL_MAIN_ALL_CLIENT_LIST;
            this.moonClient.SendMessage(message);
        }



        public void SendTextMessageToGroup(string groupId, List<string> userIds, string content)
        {
            Message message = new Message();
            message.Head = new MessageHead();
            message.Body = new MessageBody();
            message.Head.MsgId = CreateMsgId("node1");
            message.Head.MsgOrder = 0;
            message.Head.MsgTime = DateTimeUtil.GetTimeStamp();
            message.Head.MsgSize = content.Length;
            message.Head.MainMsgNum = MoonProtocol.BroadcastMsg.MN_PROTOCOL_MAIN_BROADCAST;
            message.Head.SubMsgNum = MoonProtocol.BroadcastMsg.MN_PROTOCOL_SUB_USER_TEXT_BROADCAST;
            UserBroadcastMessage broadcastMessage = new UserBroadcastMessage();
            broadcastMessage.group_id = groupId;
            broadcastMessage.from_client_id = this.moonClient.getClientId();
            broadcastMessage.to_client_ids = new List<string>();
            foreach (string usrId in userIds)
            {
                broadcastMessage.to_client_ids.Add(usrId);
            }
            message.Body.Content = JsonConvert.SerializeObject(broadcastMessage);
            this.moonClient.SendMessage(message);
        }

        /// <summary>
        /// 创建消息id，消息id的命名规则：通信服务节点名称+ 32位uuid
        /// </summary>
        /// <returns></returns>
        private string CreateMsgId(string serverNodeName)
        {
            return serverNodeName + "-" + UUIDUtil.Generator32UUID();
        }

        /// <summary>
        /// 处理系统所有消息，并且解析消息类型，然后分发下去
        /// </summary>
        /// <param name="message"></param>
        public void ServerMessageHandler(Message message)
        {
            switch (message.Head.MainMsgNum)
            {
                //获取当前服务节点的客户端列表信息
                case MoonProtocol.ServeClientInfo.MN_PROTOCOL_MAIN_SCI:
                    ParseSystemMessage(message);
                    break;
                //点对点消息
                case MoonProtocol.PointToPointMsg.MN_PROTOCOL_MAIN_MSG_POINT_TO_POINT:
                    ParsePTPMessage(message);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 解析系统消息
        /// </summary>
        /// <param name="message"></param>
        private void ParseSystemMessage(Message message)
        {
            if (sysMessageCallback != null)
            {
                switch (message.Head.SubMsgNum)
                {
                    case MoonProtocol.ServeClientInfo.MN_PROTOCAL_MAIN_ALL_CLIENBT_LIST_OK: //获取服务节点所有的在线客户端列表
                        sysMessageCallback.RecvServerNodeAllOnlineClientList(Convert.ToString(message.Body.Content));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 处理点对点消息
        /// </summary>
        /// <param name="message"></param>
        private void ParsePTPMessage(Message message)
        {
            if (ptpMessageCallback != null)
            {
                PTPMessage ptpMessage = JsonConvert.DeserializeObject<PTPMessage>(Convert.ToString(message.Body.Content));
                switch (message.Head.SubMsgNum)
                {
                    case MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_TEXT://文本消息
                        ptpMessageCallback.ReceiveTextNewMessage(ptpMessage.from_client_id, ptpMessage.context);
                        break;
                    case MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_EMOTICON://表情消息
                        ptpMessageCallback.ReceiveEmoticonNewMessage(ptpMessage.from_client_id, ptpMessage.context);
                        break;
                    case MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_IMG://图片消息
                        ptpMessageCallback.ReceiveImageNewMessage(ptpMessage.from_client_id, ptpMessage.context);
                        break;
                    case MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_VIDE://短视频消息
                        ptpMessageCallback.RecevieVideoNewMessage(ptpMessage.from_client_id, ptpMessage.context);
                        break;
                    case MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_FILE: //文件传输消息
                        ptpMessageCallback.ReceiveFileNewMessage(ptpMessage.from_client_id, ptpMessage.context);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
