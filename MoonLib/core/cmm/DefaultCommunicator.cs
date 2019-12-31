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
            message.message_head.main_msg_num = MoonProtocol.PointToPointMsg.SYS_MAIN_PROTOCOL_MSG_POINT_TO_POINT;
            message.message_head.sub_msg_num = MoonProtocol.PointToPointMsg.SYS_SUB_PROTOCOL_MSG_PTP_TEXT;
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
            message.message_head.main_msg_num = MoonProtocol.ServeClientInfo.SYS_MAIN_PROTOCOL_SCI;
            message.message_head.sub_msg_num = MoonProtocol.ServeClientInfo.SYS_SUB_PROTOCOL_ALL_CLIENT_LIST;
            this.moonClient.SendMessage(message);
        }
    }
}
