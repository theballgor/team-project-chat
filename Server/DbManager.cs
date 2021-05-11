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
        public static void CreateUser(string userName,string email,string avatar,int status, string password)
        {
            try
            {
                GenericUnitOfWork work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Add(new User() { Username = userName, Email = email, Avatar = avatar, Status = status, Password = password });
            }
            catch (Exception)
            {
                Console.WriteLine("Failed");
                throw;
            }
        }
        public static void UpdateUser(string avatar, string status, string password)
        {

        }
        public static void CreateConversation()
        {

        }
    }
}
