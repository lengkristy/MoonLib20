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
        /// 消息id，在整个网络中流转的消息唯一标识，由服务节点名称+uuid构成，如：
        /// </summary>
        public string msg_id;

        /// <summary>
        /// 消息次序
        /// </summary>
        public int msg_order;

        /// <summary>
        /// 主消息号
        /// </summary>
        public int main_msg_num;

        /// <summary>
        /// 子消息号
        /// </summary>
        public int sub_msg_num;

        /// <summary>
        /// 消息大小
        /// </summary>
        public int msg_size;

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public string msg_time;

        /// <summary>
        /// 发送消息的客户端id
        /// </summary>
        public string client_id;

        /// <summary>
        /// 消息结束标识，解决多侦的问题，对于同一个消息id的消息如果消息没有传输完成，那么则为1，消息传输完成则为0
        /// </summary>
        public int msg_end;
    }
}
