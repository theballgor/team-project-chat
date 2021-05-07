using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericUnitOfWork work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));

            IGenericRepository<User> userRepo = work.Repository<User>();

            userRepo.Add(new User() { Username = "Akio", Email = "akio.emal@i.com", Avatar = "test", Status = 0,  Password = "qwerty123"});

        }
    }
}
