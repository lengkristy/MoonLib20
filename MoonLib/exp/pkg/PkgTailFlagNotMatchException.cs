using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.exp.pkg
{
    /// <summary>
    /// 解析包的时候，包尾标识不匹配异常
    /// </summary>
    public class PkgTailFlagNotMatchException : Exception
    {

        public PkgTailFlagNotMatchException(string message)
            : base(message)
        {
            
        }
    }
}
