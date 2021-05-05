using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientServer;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerClass server = new ServerClass(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            server.Connect();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.ReadLine();
        }
    }
}
