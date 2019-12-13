using System;
using System.Collections.Generic;
using System.Text;

namespace MoonLib.core
{
    public class ClientFactory
    {

        public static IMoonClient GetDefaultClient()
        {
            return new MoonClient();
        }
    }
}
