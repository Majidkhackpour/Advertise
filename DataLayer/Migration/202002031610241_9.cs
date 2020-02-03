namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertises",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        AdvName = c.String(maxLength: 150),
                        Content = c.String(),
                        Price = c.String(maxLength: 15),
                        DivarCatGuid1 = c.Guid(nullable: false),
                        DivarCatGuid2 = c.Guid(nullable: false),
                        DivarCatGuid3 = c.Guid(nullable: false),
                        SheypoorCatGuid1 = c.Guid(nullable: false),
                        SheypoorCatGuid2 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.AdvGroups",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 100),
                        ParentGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.AdvPictures",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        PathGuid = c.String(),
                        AdvGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.AdvTitles",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Title = c.String(maxLength: 200),
                        AdvGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdvTitles");
            DropTable("dbo.AdvPictures");
            DropTable("dbo.AdvGroups");
            DropTable("dbo.Advertises");
        }
    }
}
