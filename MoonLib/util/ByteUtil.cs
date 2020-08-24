using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.util
{
    public static class ByteUtil
    {
        // 获取字节长度
        public static int ByteDataLength(byte[] bytes)
        {
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0)
                    {
                        return (i + 1);
                    }
                }
            }
            return 0;
        }
    }


}
