using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary.DbClasses;

namespace Server.Database
{
    public class ChatDBContext : DbContext
    {
        public ChatDBContext() { }
        public ChatDBContext(string conStr) : base(conStr) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ConversationConnection> ConversationConnections { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> FriendShips { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<DbFile> DbFiles { get; set; }
    }
}
