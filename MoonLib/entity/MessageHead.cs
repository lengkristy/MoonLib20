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
        /// 消息id，在整个网络中流转的消息唯一标识
        /// </summary>
        public string msg_id;

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
    }
}
