using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core.cmm.sendmsg
{
    /// <summary>
    /// 发送点对点消息
    /// </summary>
    public interface ISendPTPMessage
    {
        /// <summary>
        /// 发送点对点的文本消息到某个用户
        /// </summary>
        /// <param name="toClientId">接收方的客户端id</param>
        /// <param name="context">消息内容</param>
        void SendPTPTextMessage(string toClientId,string context);

        /// <summary>
        /// 发送点对点表情消息到某个用户
        /// </summary>
        /// <param name="toClientId">接收方的客户端id</param>
        /// <param name="context">消息内容</param>
        void SendPTPEmoticonMessage(string toClientId, string context);

        /// <summary>
        /// 发送点对点图片消息到某个用户
        /// </summary>
        /// <param name="toClientId">接收方的客户端id</param>
        /// <param name="context">消息内容</param>
        void SendPTPImageMessage(string toClientId, string context);

        /// <summary>
        /// 发送点对点短视频消息到某个用户
        /// </summary>
        /// <param name="toClientId">接收方的客户端id</param>
        /// <param name="context">消息内容</param>
        void SendPTPVideoMessage(string toClientId, string context);

        /// <summary>
        /// 发送点对点文件消息到某个用户
        /// </summary>
        /// <param name="toClientId">接收方的客户端id</param>
        /// <param name="context">消息内容</param>
        void SendPTPFileMessage(string toClientId, string context);
    }
}
