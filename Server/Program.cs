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

            /*GenericUnitOfWork work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            IGenericRepository<User> rUsers = work.Repository<User>();
            Console.WriteLine(rUsers.GetAll().First().Email);*/

            ServerClass server = new ServerClass(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            server.Connect();
            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.ReadLine();
        }
    }
}
