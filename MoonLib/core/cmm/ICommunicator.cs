using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core.cmm
{
    /// <summary>
    /// 通信接口
    /// </summary>
    public interface ICommunicator
    {

        /// <summary>
        /// 注册服务端消息回调
        /// </summary>
        /// <param name="messageCallback"></param>
        void RegistServerMessageCallback(IMessageCallBack messageCallback);


        /// <summary>
        /// 获取服务器客户端列表
        /// </summary>
        void GetServerClientInfoList();


        /// <summary>
        /// 向某个用户发送消息
        /// </summary>
        /// <param name="userId"></param>
        void SendTextMessageToUser(string userId,string strMsg);
    }
}
