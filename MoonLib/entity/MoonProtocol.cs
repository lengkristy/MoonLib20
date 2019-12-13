﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity
{
    /// <summary>
    /// 定义消息协议
    /// </summary>
    public class MoonProtocol
    {

        public static int SYS_MAIN_PROTOCOL_CONNECT_INIT = 10001; //客户端连接初始化主协议号，客户端连接服务器成功后的第一条消息，将会把客户端的运行环境信息传递过来

        public static int SYS_SUB_PROTOCOL_SERVER_CON = 1000101; //server connection sub-Protocol(used to cluster)

        public static int SYS_SUB_PROTOCOL_CLIENT_CON = 1000102; //client connection sub-Protocol


        /// <summary>
        /// 通信异常协议
        /// </summary>
        public static class CommunicationException
        {
            public static int SYS_MAIN_PROTOCOL_MSG = 10003; //消息通信异常主代码

            public static int SYS_SUB_PROTOCOL_OUT_CONNECT = 1000301;//链接丢失
        }


        /// <summary>
        /// 客户端登陆协议
        /// </summary>
        public static class ClientLogin
        {
            public static int SYS_MAIN_PROTOCOL_LOGIN = 10002; //client login main-protocol

            public static int SYS_SUB_PROTOCOL_LOGIN_FIRST = 1000201;//client first login sub-protocol

            public static int SYS_SUB_PROTOCOL_LOGIN_SUCCESS = 1000202;//client login successful sub-protocol

            public static int SYS_SUB_PROTOCOL_LOGIN_FAILD = 1000203;//client login failed sub-protocol

            public static int SYS_SUB_PROTOCOL_LOGIN_OUT = 1000204;//client login out sub-protocol
        }
    }
}
