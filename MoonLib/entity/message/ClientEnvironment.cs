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
        public string client_sdk_version;

        /// <summary>
        /// 客户端平台信息，client platform info,windows/linux/android/ios
        /// </summary>
        public string client_platform;

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string opra_system_version;

        /// <summary>
        /// sdk的令牌
        /// </summary>
        public string connect_sdk_token;
    }
}
