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
using System.Data.Entity.Validation;

namespace Server
{
    class DbManager
    {
        public DbManager()
        {
            Repositories.InitializeGenericRepositories();
        }
        public User[] GetAllUsersByUserName(string username)
        {
            try
            {
                return Repositories.RUsers.FindAll(User => User.Username.Contains(username)).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
           
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
        public MessageFile[] GetAllMessageFiles (Message message)
        {
            try
            {
                return Repositories.RMessageFiles.FindAll(item => item.Message.Id == message.Id).ToArray();
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
        public bool CreateFile(MessageFile dbFile)
        {
            try
            {
                Repositories.RMessageFiles.Add(dbFile);
                Console.WriteLine("file created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create file");
                return false;
            }
        }
        public bool CreateFile(UserImage dbFile)
        {
            try
            {
                Repositories.RUserImages.Add(dbFile);
                Console.WriteLine("file created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create file");
                return false;
            }
        }
        public bool CreateFile(ConversationImage dbFile)
        {
            try
            {
                Repositories.RConversationImages.Add(dbFile);
                Console.WriteLine("file created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create file");
                return false;
            }
        }
        public bool CreateConversation(Conversation conversation)
        {
            try
            {
                Repositories.RConversations.Add(conversation);
                Console.WriteLine("conversation created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create conversation");
                return false;
            }
        }
        public bool CreateConversationConnection(ConversationConnection conversationConnection)
        {
            try
            {
                Repositories.RConversationConnections.Add(conversationConnection);
                Console.WriteLine("conversationConnection created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create conversationConnection");
                return false;
            }
        }
        public bool CreateFriendship(Friendship friendship)
        {
            try
            {
                Repositories.RFriendShips.Add(friendship);
                Console.WriteLine("friendship created");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create friendship");
                return false;
            }
        }
  
        public bool CreateMessage(Message message)
        {
            try
            {
                Repositories.RMessages.Add(message);
                Console.WriteLine("message created");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create message");
                return false;
            }
        }
        // update
        public bool UpdateFriendship(Friendship friendship)
        {
            try
            {
                Repositories.RFriendShips.Update(friendship);
                Console.WriteLine("friendship updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update friendship");
                return false;
            }
        }
        public bool UpdateUser(User user)
        {
            try
            {
                Repositories.RUsers.Update(user);
                Console.WriteLine("user updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update user");
                return false;
            }
        }
        public bool UpdateConversation(Conversation conversation)
        {
            try
            {
                Repositories.RConversations.Update(conversation);
                Console.WriteLine("conversation updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update conversation");
                return false;
            }
        }
        public bool UpdateConverвsation(Conversation conversation)
        {
            try
            {
                //Repositories.R.Update(conversation);
                Console.WriteLine("conversation updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update conversation");
                return false;
            }
        }
    }
}