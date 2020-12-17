using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity
{
    /// <summary>
    /// 消息大小
    /// </summary>
    [Serializable]
    public class MessageHead
    {
        /// <summary>
        /// 消息id，在整个网络中流转的消息唯一标识，由路由节点名称+服务节点名称+uuid构成，如：
        /// rout2:moonserver1:709482f5aeaa4cbc96cf1bb15a72bdb3
        /// 这样的目的是为了解决消息路由节点做消息转发的时候能够快速定位发送到哪一个消息路由节点对应的服务节点
        /// 
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 消息次序
        /// </summary>
        public int MsgOrder { get; set; }

        /// <summary>
        /// 主消息号
        /// </summary>
        public int MainMsgNum { get; set; }

        /// <summary>
        /// 子消息号
        /// </summary>
        public int SubMsgNum { get; set; }

        /// <summary>
        /// 消息大小，整个消息body的大小
        /// </summary>
        public int MsgSize { get; set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public string MsgTime { get; set; }

        /// <summary>
        /// 发送消息的客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 消息结束标识，解决多侦的问题，对于同一个消息id的消息如果消息没有传输完成，那么则为1，消息传输完成则为0
        /// </summary>
        public int MsgEnd { get; set; }
    }
}
