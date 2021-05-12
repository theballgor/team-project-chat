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

        public void CreateUser(string userName,string email, string password, string description, string phoneNumber, string avatar)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Add(new User() { Username = userName, Email = email, Password = password, Description = description, PhoneNumber = phoneNumber, Status = UserStatus.Ofline, Avatar = avatar });
            }
            catch (Exception)
            {
                Console.WriteLine("Failed");
            }
        }
        public void CreateUser(string userName, string email, string password, string description, string phoneNumber)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Add(new User() { Username = userName, Email = email, Password=password, Description= description,PhoneNumber= phoneNumber,Status=UserStatus.Ofline, Avatar = null });
            }
            catch (Exception)
            {
                Console.WriteLine("Failed");
            }
        }
        public void CreateUser(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Add(user);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed");
            }
        }
        public void CreateConversation()
        {

        }

        public void CreateMessage()
        {

        }
    }
}