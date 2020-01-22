namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvertiseLogs",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        SimCardNumber = c.Long(nullable: false),
                        State = c.String(maxLength: 150),
                        City = c.String(maxLength: 150),
                        Region = c.String(maxLength: 150),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(maxLength: 150),
                        SubCategory1 = c.String(maxLength: 150),
                        SubCategory2 = c.String(maxLength: 150),
                        URL = c.String(maxLength: 100),
                        IP = c.String(maxLength: 20),
                        VisitCount = c.Int(nullable: false),
                        DateM = c.DateTime(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        AdvType = c.Int(nullable: false),
                        ImagePath = c.String(),
                        Adv = c.String(),
                        AdvStatus = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.AdvTokens",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Number = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.DivarCities",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        CountAdvInDayDivar = c.Int(nullable: false),
                        CountAdvInDaySheypoor = c.Int(nullable: false),
                        CountAdvInMounthDivar = c.Int(nullable: false),
                        CountAdvInMounthSheypoor = c.Int(nullable: false),
                        CountAdvInIPDivar = c.Int(nullable: false),
                        CountAdvInIPSheypoor = c.Int(nullable: false),
                        DivarCat1 = c.String(maxLength: 100),
                        DivarCat2 = c.String(maxLength: 100),
                        DivarCat3 = c.String(maxLength: 100),
                        SheypoorCat1 = c.String(maxLength: 100),
                        SheypoorCat2 = c.String(maxLength: 100),
                        SheypoorCat3 = c.String(maxLength: 100),
                        DivarPicPath = c.String(),
                        SheypoorPicPath = c.String(),
                        DivarDayCountForUpdateState = c.Int(nullable: false),
                        SheypoorDayCountForUpdateState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.SheypoorCities",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 150),
                        StateGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.Simcards",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        NextUseDivar = c.DateTime(nullable: false),
                        NextUseSheypoor = c.DateTime(nullable: false),
                        Number = c.Long(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Operator = c.String(maxLength: 150),
                        UserName = c.String(maxLength: 100),
                        OwnerName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.SimcardAds",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DateSabt = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        SimcardGuid = c.Guid(nullable: false),
                        AdsName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.SimcardCities",
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
            
            AddColumn("dbo.Regions", "Type", c => c.Int(nullable: false));
            DropTable("dbo.Cities");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.Regions", "Type");
            DropTable("dbo.SimcardCities");
            DropTable("dbo.SimcardAds");
            DropTable("dbo.Simcards");
            DropTable("dbo.SheypoorCities");
            DropTable("dbo.Settings");
            DropTable("dbo.DivarCities");
            DropTable("dbo.AdvTokens");
            DropTable("dbo.AdvertiseLogs");
        }
    }
}
