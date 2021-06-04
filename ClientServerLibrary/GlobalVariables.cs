using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    public static class GlobalVariables
    {
        public static readonly int ServerPort = 40001;
        public static readonly IPAddress LocalIP = IPAddress.Parse("127.0.0.1");
    }
}
