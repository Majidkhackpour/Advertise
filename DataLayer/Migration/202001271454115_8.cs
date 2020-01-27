namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Settings", "DivarPicPath");
            DropColumn("dbo.Settings", "SheypoorPicPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "SheypoorPicPath", c => c.String());
            AddColumn("dbo.Settings", "DivarPicPath", c => c.String());
        }
    }
}
