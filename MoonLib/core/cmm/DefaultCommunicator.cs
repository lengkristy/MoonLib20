using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.entity;
using MoonLib.util;

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


        public void Login(string id)
        {
            Message message = new Message();
            message.message_head.msg_id = UUIDUtil.Generator32UUID();
            message.message_head.main_msg_num = MoonProtocol.ClientLogin.SYS_MAIN_PROTOCOL_LOGIN;
            message.message_head.sub_msg_num = MoonProtocol.ClientLogin.SYS_SUB_PROTOCOL_LOGIN_FIRST;
            message.message_body.content = UUIDUtil.Generator32UUID();
            moonClient.SendMessage(message);
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
    }
}
