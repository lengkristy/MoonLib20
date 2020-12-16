using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity
{
    /// <summary>
    /// 消息
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 消息头
        /// </summary>
        public MessageHead Head
        {
            get;
            set;
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public MessageBody Body
        {
            get;
            set;
        }
    }
}
