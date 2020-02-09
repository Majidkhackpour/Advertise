namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvContents",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Content = c.String(),
                        AdvGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            AddColumn("dbo.Settings", "FierstLevelChatAddress", c => c.String());
            AddColumn("dbo.Settings", "SecondLevelChatAddress", c => c.String());
            AddColumn("dbo.Simcards", "DivarCityForChat", c => c.Guid(nullable: false));
            AddColumn("dbo.Simcards", "SheypoorCityForChat", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Simcards", "SheypoorCityForChat");
            DropColumn("dbo.Simcards", "DivarCityForChat");
            DropColumn("dbo.Settings", "SecondLevelChatAddress");
            DropColumn("dbo.Settings", "FierstLevelChatAddress");
            DropTable("dbo.AdvContents");
        }
    }
}
