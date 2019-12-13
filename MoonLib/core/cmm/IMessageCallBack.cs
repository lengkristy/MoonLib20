using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.entity;

namespace MoonLib.core.cmm
{
    /// <summary>
    /// 消息回调接口
    /// </summary>
    public interface IMessageCallBack
    {

        /// <summary>
        /// 服务消息处理
        /// </summary>
        /// <param name="message">消息体</param>
        void ServerMessageHandler(Message message);
    }
}
