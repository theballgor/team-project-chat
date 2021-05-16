﻿using System;
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
                Console.WriteLine("Failed to create user");
                return false;   
            }
        }
        public User CheckLogin(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                User dbUser= userRepo.FindAll(User => User.Username == user.Username && User.Password == user.Username).First();
                Console.WriteLine("user logined");
                return dbUser;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to login");
                return null;
            }
        }
    }
}