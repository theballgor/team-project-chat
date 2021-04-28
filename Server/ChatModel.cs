using ClientServerLibrary.DbClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Server
{
    public class ChatModel : DbContext
    {
        public ChatModel()
            : base("name=ChatModel")
        {
            Database.SetInitializer<ChatModel>(new CreateDatabaseIfNotExists<ChatModel>());
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ConversationConnection> ConversationConnections { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}