namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Advertises", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertises", "Content", c => c.String());
        }
    }
}
