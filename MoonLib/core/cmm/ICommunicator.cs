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
        /// 用户id，这里登陆不需要用户密码，只需要用户id，用户密码在第三方系统中做控制
        /// </summary>
        /// <param name="id"></param>
        void Login(string id);
    }
}
