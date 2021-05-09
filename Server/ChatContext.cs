using System;
using System.Data.Entity;
using System.Linq;
using ClientServerLibrary.DbClasses;

namespace Server
{
    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("name=ChatContext")
        {
            Database.SetInitializer<ChatContext>(new CreateDatabaseIfNotExists<ChatContext>());
        }

        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<ConversationModel> Conversations { get; set; }
        public virtual DbSet<ConversationConnection> ConversationConnections { get; set; }
        public virtual DbSet<MessageModel> Messages { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Conversation>()
        //        .HasOptional<Conversation>(s => s.)
        //        .WithMany()
        //        .WillCascadeOnDelete(false);
        //}
    }
}