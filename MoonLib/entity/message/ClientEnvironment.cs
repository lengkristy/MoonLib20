using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity.message
{
    /// <summary>
    /// 客户端环境
    /// </summary>
    [Serializable]
    public class ClientEnvironment
    {

        /// <summary>
        /// sdk版本
        /// </summary>
        public string ClientSDKVersion { get; set; }

        /// <summary>
        /// 客户端平台信息，client platform info,windows/linux/android/ios
        /// </summary>
        public string ClientPlatform { get; set; }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OpraSystemVersion { get; set; }

        /// <summary>
        /// sdk的令牌
        /// </summary>
        public string ConnectSDKToken { get; set; }

        /// <summary>
        /// 客户端id，该客户端id是用于在该客户端的整个网络生命周期内的标识，用于接收发送消息，应该具有唯一性
        /// </summary>
        public string ClientId { get; set; }
    }
}
