using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core.cmm.callback
{
    /// <summary>
    /// 接收广播消息的回调
    /// </summary>
    public interface BroadcastMessageCallback
    {
        /// <summary>
        /// 接收文本新消息
        /// </summary>
        /// <param name="fromClientId">发送方的客户端id</param>
        /// <param name="groupId">群id</param>
        /// <param name="content">文本消息内容</param>
        void ReceiveTextNewMessage(string fromClientId,string groupId, string content);

        /// <summary>
        /// 接收表情新消息
        /// </summary>
        /// <param name="fromClientId">发送方的客户端id</param>
        /// <param name="groupId">群id</param>
        /// <param name="content">文本消息内容</param>
        void ReceiveEmoticonNewMessage(string fromClientId, string groupId, string content);

        /// <summary>
        /// 接收图片新消息
        /// </summary>
        /// <param name="fromClientId">发送方的客户端id</param>
        /// <param name="groupId">群id</param>
        /// <param name="content">文本消息内容</param>
        void ReceiveImageNewMessage(string fromClientId, string groupId, string content);

        /// <summary>
        /// 接收短视频新消息
        /// </summary>
        /// <param name="fromClientId">发送方的客户端id</param>
        /// <param name="groupId">群id</param>
        /// <param name="content">文本消息内容</param>
        void RecevieVideoNewMessage(string fromClientId, string groupId, string content);

        /// <summary>
        /// 接收文件新消息
        /// </summary>
        /// <param name="fromClientId">发送方的客户端id</param>
        /// <param name="groupId">群id</param>
        /// <param name="content">文本消息内容</param>
        void ReceiveFileNewMessage(string fromClientId, string groupId, string content);
    }
}
