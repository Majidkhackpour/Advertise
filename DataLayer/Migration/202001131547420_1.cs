namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 150),
                        Weight = c.Int(nullable: false),
                        StateGuid = c.Guid(nullable: false),
                        isDivar = c.Boolean(nullable: false),
                        isSheypoor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 150),
                        CityGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.States");
            DropTable("dbo.Regions");
            DropTable("dbo.Cities");
        }
    }
}
