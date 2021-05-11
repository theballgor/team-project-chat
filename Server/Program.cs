using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary.DbClasses;
using ClientServerLibrary;
using Server.Database;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServerClass server = new ServerClass(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            //server.Connect();

     
            DbManager dbManager = new DbManager();
            dbManager.CreateUser("Roman", "roman1273232@gmail.com", "1","description","+30675621670");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.ReadLine();
        }
    }
}
