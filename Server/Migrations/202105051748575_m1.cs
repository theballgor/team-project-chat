namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Description = c.String(maxLength: 256),
                        Avatar = c.String(),
                        ConversationAccessibility = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId, cascadeDelete: false)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Avatar = c.String(),
                        Description = c.String(maxLength: 256),
                        PhoneNumber = c.String(),
                        UserStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendShips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequesterId = c.Int(nullable: false),
                        InviterId = c.Int(nullable: false),
                        InviteTime = c.DateTime(nullable: false),
                        FriendshipStatus = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.InviterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.RequesterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.RequesterId)
                .Index(t => t.InviterId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 4000),
                        SendTime = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ConversationId = c.Int(nullable: false),
                        MessageType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConversationConnections", "UserId", "dbo.Users");
            DropForeignKey("dbo.ConversationConnections", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.FriendShips", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "RequesterId", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "InviterId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.FriendShips", new[] { "User_Id" });
            DropIndex("dbo.FriendShips", new[] { "InviterId" });
            DropIndex("dbo.FriendShips", new[] { "RequesterId" });
            DropIndex("dbo.Conversations", new[] { "CreatorId" });
            DropIndex("dbo.ConversationConnections", new[] { "ConversationId" });
            DropIndex("dbo.ConversationConnections", new[] { "UserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.FriendShips");
            DropTable("dbo.Users");
            DropTable("dbo.Conversations");
            DropTable("dbo.ConversationConnections");
        }
    }
}
