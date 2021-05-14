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
    class DbManager
    {
        private GenericUnitOfWork work;

        public DbManager()
        {
            work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
        }
        public bool CreateUser(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Add(user);
                Console.WriteLine("user created");
                return true;
            }
            catch (Exception)
            {
                return false;
                Console.WriteLine("Failed to create user");
            }
        }
        public bool CheckLogin(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.FindAll(User => User.Username == user.Username && User.Password == user.Username).First();
                Console.WriteLine("user logined");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to login");
                return false;
            }
        }
    }
}