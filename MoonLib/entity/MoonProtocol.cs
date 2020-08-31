using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.entity
{
    /// <summary>
    /// 定义消息协议
    /// </summary>
    public class MoonProtocol
    {
        /// <summary>
        /// 初始化协议
        /// </summary>
        public static class InitProtocol
        {
            /*********************************************************************************************************************************************************/
            /// <summary>
            /// 客户端连接初始化主协议号，客户端连接服务器成功后的第一条消息，将会把客户端的运行环境信息传递过来
            /// </summary>
            public static int MN_PROTOCOL_MAIN_CONNECT_INIT = 10001;

            /// <summary>
            /// server connection sub-Protocol(used to cluster)，集群使用
            /// </summary>
            public static int MN_PROTOCOL_SUB_SERVER_CON = 1000101;

            /// <summary>
            /// client connection sub-Protocol，客户端连接的子协议
            /// </summary>
            public static int MN_PROTOCOL_SUB_CLIENT_CON = 1000102;

            /// <summary>
            /// 服务端同意接受连接的子协议
            /// </summary>
            public static int MN_PROTOCOL_SUB_SERVER_ACCEPT = 1000109;
        }
        

        /*********************************************************************************************************************************************************/


        /// <summary>
        /// 数据包定义
        /// </summary>
        public static class Packge
        {
            /// <summary>
            /// 数据实体字节最大长度
            /// </summary>
            public static int DATA_BYTE_MAX_LENGTH = 9000;

            /// <summary>
            /// 包字节最大长度
            /// </summary>
            public static int PKG_BYTE_MAX_LENGTH = 9999;

            /// <summary>
            /// 包头标识
            /// </summary>
            public static string PKG_HEAD_FLAG = "\n\rMNPH\n\r";

            /// <summary>
            /// 包头长度
            /// </summary>
            public static int PKG_HEAD_LENGTH = 12;

            /// <summary>
            /// 包尾标识
            /// </summary>
            public static string PKG_TAIL_FLAG = "\n\rMNPT\n\r";

            /// <summary>
            /// 包尾长度
            /// </summary>
            public static int PKG_TAIL_LENGTH = 8;

        }

        /// <summary>
        /// 本地协议，用于通知第三方，不和服务器交互
        /// </summary>
        public static class LocalProtocol
        {
            /// <summary>
            /// 服务端没有响应客户端消息
            /// </summary>
            public static int MN_PROTOCOL_MAIN_SERVER_NOT_REPLY = 90001;

            /// <summary>
            /// 
            /// </summary>
            public static int MN_PROTOCOL_MAIN_CLIENT_SEND_SERVER_MSG = 90002;
        }

        /// <summary>
        /// 服务端回复消息协议
        /// </summary>
        public static class ServerReply
        {
            public static int MN_PROTOCOL_MAIN_REPLY = 10003;//服务端回复主协议代码

            public static int MN_PROTOCOL_SUB_REPLY_OK = 1000301;//服务端收到消息成功的子协议代码

            public static int MN_PROTOCOL_SUB_REPLY_FAILD = 1000302;//服务端收到消息失败的子协议代码
        }

        /// <summary>
        /// 通信异常协议
        /// </summary>
        public static class CommunicationException
        {
            public static int MN_PROTOCOL_MAIN_MSG = 20003; //消息通信异常主代码

            public static int MN_PROTOCOL_SUB_OUT_CONNECT = 2000301;//链接丢失
        }

        /// <summary>
        /// 获取服务器客户端信息协议
        /// </summary>
        public static class ServeClientInfo
        {
            public static int MN_PROTOCOL_MAIN_SCI = 10004;//获取服务器客户端信息主协议代码
            public static int MN_PROTOCOL_MAIN_ALL_CLIENT_LIST = 1000401;//请求获取服务器中所有客户端列表
            public static int MN_PROTOCAL_MAIN_ALL_CLIENBT_LIST_OK = 1000402;//服务器成功返回客户端列表
        }

        /// <summary>
        /// 点对点消息协议
        /// </summary>
        public static class PointToPointMsg
        {
            public static int MN_PROTOCOL_MAIN_MSG_POINT_TO_POINT = 10002; //点对点的主消息
            public static int MN_PROTOCOL_SUB_MSG_PTP_TEXT   =  1000201; //点对点的文本消息
            public static int MN_PROTOCOL_SUB_MSG_PTP_EMOTICON = 1000202; //点对点的表情消息
            public static int MN_PROTOCOL_SUB_MSG_PTP_IMG  =  1000203; //点对点的图片消息
            public static int MN_PROTOCOL_SUB_MSG_PTP_VIDE = 1000204; //点对点的短视频消息
            public static int MN_PROTOCOL_SUB_MSG_PTP_FILE	= 1000205; //点对点的文件传输消息
        }
    }
}
