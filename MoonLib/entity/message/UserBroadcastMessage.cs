using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity.message
{
    /// <summary>
    /// 用户广播消息
    /// </summary>
    public class UserBroadcastMessage
    {
        /// <summary>
        /// 发起方客户端id
        /// </summary>
        public string from_client_id;

        /// <summary>
        /// 接收方的客户端id列表
        /// </summary>
        public List<string> to_client_ids;

        /// <summary>
        /// 群id
        /// </summary>
        public string group_id;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string content;
    }
}
