namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DataLayer.Context.dbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migration";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataLayer.Context.dbContext context)
        {
        }
    }
}
