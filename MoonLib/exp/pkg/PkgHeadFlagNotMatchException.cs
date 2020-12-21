using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.exp.pkg
{
    /// <summary>
    /// 包头标识不匹配异常
    /// </summary>
    public class PkgHeadFlagNotMatchException : Exception
    {
        public PkgHeadFlagNotMatchException(string message):base(message)
        {
            
        }
    }
}
