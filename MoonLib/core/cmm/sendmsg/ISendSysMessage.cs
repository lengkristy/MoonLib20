using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core.cmm.sendmsg
{
    /// <summary>
    /// 发送系统消息接口
    /// </summary>
    public interface ISendSysMessage
    {
        /// <summary>
        /// 获取服务器客户端列表
        /// </summary>
        void GetServerClientInfoList();
    }
}
