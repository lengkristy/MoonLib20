using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.util
{
    /// <summary>
    /// uuid工具类
    /// </summary>
    public class UUIDUtil
    {
        /// <summary>
        /// 生成32位uuid
        /// </summary>
        /// <returns></returns>
        public static string Generator32UUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
