using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.core.cmm.callback;

namespace MoonLib.core.cmm
{
    /// <summary>
    /// 通信接口
    /// </summary>
    public interface ICommunicator
    {

        /// <summary>
        /// 注册消息回调接口
        /// </summary>
        /// <param name="sysMessageCallback"></param>
        /// <param name="ptpMessageCallback"></param>
        /// <param name="broadcastMessageCallback"></param>
        void RegistCallback(SysMessageCallback sysMessageCallback,PTPMessageCallback ptpMessageCallback,BroadcastMessageCallback broadcastMessageCallback);

        /// <summary>
        /// 获取服务器客户端列表
        /// </summary>
        void GetServerClientInfoList();


        /// <summary>
        /// 向某个用户发送文本消息
        /// </summary>
        /// <param name="userId"></param>
        void SendTextMessageToUser(string userId,string strMsg);

        /// <summary>
        /// 发送群文本消息
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <param name="userIds">用户列表</param>
        /// <param name="content">内容</param>
        void SendTextMessageToGroup(string groupId,List<string> userIds,string content);
    }
}
