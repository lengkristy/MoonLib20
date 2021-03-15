using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity.message
{
    /// <summary>
    /// 点对点消息
    /// </summary>
    public class PTPMessage
    {
        /// <summary>
        /// 发起方客户端id
        /// </summary>
        public string FromClientId { get; set; }

        /// <summary>
        /// 接收方客户端id
        /// </summary>
        public string ToClientId { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Context { get; set; }
    }
}
