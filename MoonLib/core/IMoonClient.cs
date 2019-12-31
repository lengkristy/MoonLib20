using System;
using System.Collections.Generic;
using System.Text;
using MoonLib.core.cmm;

namespace MoonLib.core
{
    /// <summary>
    /// moon客户端接口
    /// </summary>
    public interface IMoonClient
    {

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        void ConnectServer(string clientId,string ip, int port);

        /// <summary>
        /// 获取通信器
        /// </summary>
        /// <returns></returns>
        ICommunicator GetCommunicator();

        /// <summary>
        /// 关闭通信
        /// </summary>
        void Close();
    }
}
