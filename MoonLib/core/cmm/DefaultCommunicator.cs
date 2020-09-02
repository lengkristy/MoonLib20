using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.entity;
using MoonLib.util;
using MoonLib.entity.message;

namespace MoonLib.core.cmm
{
    /// <summary>
    /// 默认通信器
    /// </summary>
    internal class DefaultCommunicator : ICommunicator
    { 

        /// <summary>
        /// 消息回调接口
        /// </summary>
        private IMessageCallBack messageCallback;

        private MoonClient moonClient;

        internal DefaultCommunicator(MoonClient moonClient)
        {
            this.moonClient = moonClient;
        }

        public void RegistServerMessageCallback(IMessageCallBack messageCallback)
        {
            this.messageCallback = messageCallback;
        }


        /// <summary>
        /// 获取消息回调接口
        /// </summary>
        /// <returns></returns>
        internal IMessageCallBack GetMessageCallback()
        {
            return this.messageCallback;
        }


        /// <summary>
        /// 发送文本消息给某个用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="strMsg"></param>
        public void SendTextMessageToUser(string userId, string strMsg)
        {
            Message message = new Message();
            message.message_head.msg_id = CreateMsgId("node1");
            message.message_head.msg_order = 0;
            message.message_head.msg_time = DateTimeUtil.GetTimeStamp();
            message.message_head.msg_size = strMsg.Length;
            message.message_head.main_msg_num = MoonProtocol.PointToPointMsg.MN_PROTOCOL_MAIN_MSG_POINT_TO_POINT;
            message.message_head.sub_msg_num = MoonProtocol.PointToPointMsg.MN_PROTOCOL_SUB_MSG_PTP_TEXT;
            
            PTPMessage ptpMsg = new PTPMessage();
            ptpMsg.from_client_id = this.moonClient.getClientId();
            ptpMsg.to_client_id = userId;
            ptpMsg.context = strMsg;
            message.message_body.content = ptpMsg;
            this.moonClient.SendMessage(message);
        }


        public void GetServerClientInfoList()
        {
            Message message = new Message();
            message.message_head.main_msg_num = MoonProtocol.ServeClientInfo.MN_PROTOCOL_MAIN_SCI;
            message.message_head.sub_msg_num = MoonProtocol.ServeClientInfo.MN_PROTOCOL_MAIN_ALL_CLIENT_LIST;
            this.moonClient.SendMessage(message);
        }



        public void SendTextMessageToGroup(string groupId, List<string> userIds, string content)
        {
            Message message = new Message();
            message.message_head.msg_id = CreateMsgId("node1");
            message.message_head.msg_order = 0;
            message.message_head.msg_time = DateTimeUtil.GetTimeStamp();
            message.message_head.msg_size = content.Length;
            message.message_head.main_msg_num = MoonProtocol.BroadcastMsg.MN_PROTOCOL_MAIN_BROADCAST;
            message.message_head.sub_msg_num = MoonProtocol.BroadcastMsg.MN_PROTOCOL_SUB_GROUP_BROADCAST;
            UserBroadcastMessage broadcastMessage = new UserBroadcastMessage();
            broadcastMessage.group_id = groupId;
            broadcastMessage.from_client_id = this.moonClient.getClientId();
            broadcastMessage.to_client_ids = new List<string>();
            foreach (string usrId in userIds)
            {
                broadcastMessage.to_client_ids.Add(usrId);
            }
            message.message_body.content = broadcastMessage;
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
    }
}
