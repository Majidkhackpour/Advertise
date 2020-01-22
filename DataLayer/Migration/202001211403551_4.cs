namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "DivarMaxImgCount", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "SheypoorMaxImgCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "SheypoorMaxImgCount");
            DropColumn("dbo.Settings", "DivarMaxImgCount");
        }
    }
}
