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

        public DbManager()
        {
            Repositories.InitializeGenericRepositories();
        }
        public User CheckLoginByEmail(User user)
        {
            try
            {            
                User dbUser= Repositories.RUsers.FindAll(User => User.Email == user.Email && User.Password == user.Password).First();
                Console.WriteLine("user logined");
                return dbUser;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to login");
                return null;
            }
        }
        public User CheckLoginByUsername(User user)
        {
            try
            {
                User dbUser = Repositories.RUsers.FindAll(User => User.Username == user.Username && User.Password == user.Password).First();
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
        public User[] GetAllUsers()
        {
            try
            {
                return Repositories.RUsers.GetAll().ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Conversation GetConversationById(int conversationId)
        {
            try
            {
                return Repositories.RConversations.FindById(conversationId);
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
                return Repositories.RUsers.FindById(userId);
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
                return Repositories.RConversationConnections.FindAll(item => item.User.Id == userId).ToArray();
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
                ConversationConnection[] userConversationConnections = Repositories.RConversationConnections.FindAll(item => item.User.Id == userId).ToArray();
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
                ConversationConnection[] userConversationConnections = Repositories.RConversationConnections.FindAll(item => item.Conversation.Id == conversation.Id).ToArray();
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
                return Repositories.RFriendShips.FindAll(item => item.Inviter.Id == user.Id || item.Requester.Id == user.Id).ToArray();

            }
            catch (Exception)
            {
                return null;
            }
      
        }
        public Message[] GetAllConversationMessages(Conversation conversation)
        {
            try
            {
                return Repositories.RMessages.FindAll(item => item.Conversation.Id == conversation.Id).ToArray();
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
                Repositories.RUsers.Add(user);
                Console.WriteLine("user created");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create user");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool CreateFile(DbFile dbFile)
        {
            try
            {
                Repositories.RDbFiles.Add(dbFile);
                Console.WriteLine("file created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create file");
                return false;
            }
        }
        public Conversation CreateConversation(Conversation conversation)
        {
            try
            {
                Repositories.RConversations.Add(conversation);
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
                Repositories.RConversationConnections.Add(conversationConnection);
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
                Repositories.RFriendShips.Add(friendship);
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
                Repositories.RMessages.Add(message);
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
                Repositories.RFriendShips.Update(friendship);
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
                Repositories.RUsers.Update(user);
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
                Repositories.RConversations.Update(conversation);
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