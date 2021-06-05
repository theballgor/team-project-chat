using System.Configuration;
using ClientServerLibrary.DbClasses;

namespace Server.Database
{
    public static class Repositories
    {
        public static GenericUnitOfWork Work { get; set; }
        public static IGenericRepository<User> RUsers;
        public static IGenericRepository<ConversationConnection> RConversationConnections { get; set; }
        public static IGenericRepository<Message> RMessages { get; set; }
        public static IGenericRepository<Friendship> RFriendShips { get; set; }
        public static IGenericRepository<Conversation> RConversations { get; set; }
        public static IGenericRepository<MessageFile> RMessageFiles { get; set; }
        public static IGenericRepository<UserImage> RUserImages { get; set; }
        public static IGenericRepository<ConversationImage> RConversationImages { get; set; }

        public static void InitializeGenericRepositories()
        {
            Work = new GenericUnitOfWork(new ChatDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            RUsers = Work.Repository<User>();
            RConversationConnections = Work.Repository<ConversationConnection>();
            RMessages = Work.Repository<Message>();
            RFriendShips = Work.Repository<Friendship>();
            RConversations = Work.Repository<Conversation>();
            RMessageFiles = Work.Repository<MessageFile>();
            RUserImages = Work.Repository<UserImage>();
            RConversationImages = Work.Repository<ConversationImage>();


            RUsers.GetAll();
            RConversationConnections.GetAll();
            RMessages.GetAll();
            RFriendShips.GetAll();
            RConversations.GetAll();
            RMessageFiles.GetAll();
            RUserImages.GetAll();
            RConversationImages.GetAll();
        }
    }
}
