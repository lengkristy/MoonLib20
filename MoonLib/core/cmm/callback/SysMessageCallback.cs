using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core.cmm.callback
{
    /// <summary>
    /// 系统消息回调
    /// </summary>
    public interface SysMessageCallback
    {
        /// <summary>
        /// 接收通信服务节点所有的在线客户端列表
        /// </summary>
        /// <param name="clientList"></param>
        void RecvServerNodeAllOnlineClientList(string clientList);
    }
}
