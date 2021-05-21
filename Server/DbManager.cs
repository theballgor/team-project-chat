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
        public User CheckLogin(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                User dbUser = userRepo.FindAll(User => User.Username == user.Username && User.Password == user.Username).First();
                Console.WriteLine("user logined");
                return dbUser;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to login");
                return null;
            }
        }
        // get
        public Conversation GetConversationById(int conversationId)
        {
            try
            {
                IGenericRepository<Conversation> conversationRepo = work.Repository<Conversation>();
                return conversationRepo.FindById(conversationId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User GetUserById(int userId)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                return userRepo.FindById(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ConversationConnection[] GetAllUserConversationConnections(int userId)
        {
            try
            {
                IGenericRepository<ConversationConnection> conversationConnectionRepo = work.Repository<ConversationConnection>();
                return conversationConnectionRepo.FindAll(item => item.User == GetUserById(userId)).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Conversation[] GetAllUserConversations(int userId)
        {
            try
            {
                IGenericRepository<ConversationConnection> conversationConnectionRepo = work.Repository<ConversationConnection>();
                ConversationConnection[] userConversationConnections = conversationConnectionRepo.FindAll(item => item.User == GetUserById(userId)).ToArray();
                List<Conversation> conversations = new List<Conversation>();
                foreach (var item in userConversationConnections)
                    conversations.Add(item.Conversation);
                return conversations.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User[] GetAllUsersFromConversation(Conversation conversation)
        {
            try
            {
                IGenericRepository<ConversationConnection> conversationConnectionRepo = work.Repository<ConversationConnection>();
                ConversationConnection[] userConversationConnections = conversationConnectionRepo.FindAll(item => item.Conversation == conversation).ToArray();
                List<User> users = new List<User>();
                foreach (var item in userConversationConnections)
                    users.Add(item.User);
                return users.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Friendship[] GetAllUserFriendShips(User user)
        {
            try
            {
                IGenericRepository<Friendship> friendshipRepo = work.Repository<Friendship>();
                return friendshipRepo.FindAll(item => item.Inviter == user || item.Requester == user).ToArray();
            }
            catch (Exception)
            {
                return null;
            }

        }
        // create
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
        public Conversation CreateConversation(Conversation conversation)
        {
            try
            {
                IGenericRepository<Conversation> conversationRepo = work.Repository<Conversation>();
                conversationRepo.Add(conversation);
                Console.WriteLine("conversation created");
                return conversation;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create conversation");
                return null;
            }
        }
        public ConversationConnection CreateConversationConnection(ConversationConnection conversationConnection)
        {
            try
            {
                IGenericRepository<ConversationConnection> conversationConnectionRepo = work.Repository<ConversationConnection>();
                conversationConnectionRepo.Add(conversationConnection);
                Console.WriteLine("conversationConnection created");
                return conversationConnection;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create conversationConnection");
                return null;
            }
        }
        public Friendship CreateFriendship(Friendship friendship)
        {
            try
            {
                IGenericRepository<Friendship> friendshipRepo = work.Repository<Friendship>();
                friendshipRepo.Add(friendship);
                Console.WriteLine("friendship created");
                return friendship;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create friendship");
                return null;
            }
        }
        public Message CreateMessage(Message message)
        {
            try
            {
                IGenericRepository<Message> messageRepo = work.Repository<Message>();
                messageRepo.Add(message);
                Console.WriteLine("message created");
                return message;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create message");
                return null;
            }
        }
        // update
        public Friendship ChangeFriendshipStatus(Friendship friendship)
        {
            try
            {
                IGenericRepository<Friendship> friendshipRepo = work.Repository<Friendship>();
                friendshipRepo.Update(friendship);
                Console.WriteLine("friendship updated");
                return friendship;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update friendship");
                return null;
            }
        }
        public User ChangeUserStatus(User user)
        {
            try
            {
                IGenericRepository<User> userRepo = work.Repository<User>();
                userRepo.Update(user);
                Console.WriteLine("user updated");
                return user;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update user");
                return null;
            }
        }
        public Conversation ChangeConversationStatus(Conversation conversation)
        {
            try
            {
                IGenericRepository<Conversation> conversationRepo = work.Repository<Conversation>();
                conversationRepo.Update(conversation);
                Console.WriteLine("conversation updated");
                return conversation;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update conversation");
                return null;
            }
        }
    }
}