using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.exp.pkg
{
    /// <summary>
    /// 包长度不匹配异常
    /// </summary>
    public class PkgLengthNotMatchException : Exception
    {
        public PkgLengthNotMatchException(string message)
            : base(message)
        {
            
        }
    }
}
