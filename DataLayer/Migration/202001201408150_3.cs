namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SimcardCities", newName: "DivarSimCities");
            CreateTable(
                "dbo.SheypoorSimCities",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        SimcardGuid = c.Guid(nullable: false),
                        StateGuid = c.Guid(nullable: false),
                        CityGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            DropColumn("dbo.DivarSimCities", "StateGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DivarSimCities", "StateGuid", c => c.Guid(nullable: false));
            DropTable("dbo.SheypoorSimCities");
            RenameTable(name: "dbo.DivarSimCities", newName: "SimcardCities");
        }
    }
}
