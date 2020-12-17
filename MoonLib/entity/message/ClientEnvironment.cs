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
        /// 客户端id，该客户端id是用于在该客户端的整个网络生命周期内的标识，用于接收发送消息，应该具有唯一性，客户端id的定义由 消息路由节点名称+消息服务节点名称+uuid构成，如：
        /// * rout2:moonserver1:709482f5aeaa4cbc96cf1bb15a72bdb3
	    /// 这样的目的是为了解决消息路由节点做消息转发的时候能够快速定位发送到哪一个消息路由节点对应的服务节点
        /// </summary>
        public string ClientId { get; set; }
    }
}
