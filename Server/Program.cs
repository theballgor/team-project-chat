using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerClass server = new ServerClass(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000));
            server.Connect();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.ReadLine();
        }
    }
}
