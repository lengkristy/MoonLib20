using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity
{
    /// <summary>
    /// 消息体
    /// </summary>
    [Serializable]
    public class MessageBody
    {
        /// <summary>
        /// 消息内容，消息内容以字符串进行传输
        /// </summary>
        public string Content 
        { 
            get; 
            set;
        }
    }
}
