namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Simcards", "DivarChatCat1", c => c.Guid(nullable: false));
            AddColumn("dbo.Simcards", "DivarChatCat2", c => c.Guid(nullable: false));
            AddColumn("dbo.Simcards", "DivarChatCat3", c => c.Guid(nullable: false));
            AddColumn("dbo.Simcards", "SheypoorChatCat1", c => c.Guid(nullable: false));
            AddColumn("dbo.Simcards", "SheypoorChatCat2", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Simcards", "SheypoorChatCat2");
            DropColumn("dbo.Simcards", "SheypoorChatCat1");
            DropColumn("dbo.Simcards", "DivarChatCat3");
            DropColumn("dbo.Simcards", "DivarChatCat2");
            DropColumn("dbo.Simcards", "DivarChatCat1");
        }
    }
}
