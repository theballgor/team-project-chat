using System.Configuration;
using ClientServerLibrary.DbClasses;

namespace Server.Database
{
    public static class Repositories
    {
        public static GenericUnitOfWork Work { get; set; }
        public static IGenericRepository<User> RUsers;
        public static IGenericRepository<UserConversation> RUserConversations { get; set; }
        public static IGenericRepository<Message> RMessages { get; set; }
        public static IGenericRepository<Friendship> RFriendShips { get; set; }
        public static IGenericRepository<Conversation> RConversations { get; set; }

        public static void InitializeGenericRepositories()
        {
            Work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            RUsers = Work.Repository<User>();
            RUserConversations = Work.Repository<UserConversation>();
            RMessages = Work.Repository<Message>();
            RFriendShips = Work.Repository<Friendship>();
            RConversations = Work.Repository<Conversation>();
        }
    }
}
