using DataLayer.Migrations;
using DataLayer.Models;

namespace DataLayer.Context
{
    using System.Data.Entity;

    public class dbContext : DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<dbContext, Configuration>());
        }
        public virtual DbSet<States> State { get; set; }
        public virtual DbSet<DivarCity> DivarCity { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<AdvertiseLog> AdvertiseLog { get; set; }
        public virtual DbSet<AdvTokens> AdvTokens { get; set; }
        public virtual DbSet<Simcard> Simcard { get; set; }
        public virtual DbSet<SimcardAds> SimcardAds { get; set; }
        public virtual DbSet<SheypoorSimCity> SheypoorSimCity { get; set; }
        public virtual DbSet<SheypoorCity> SheypoorCity { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<DivarSimCity> DivarSimCity { get; set; }
        public virtual DbSet<AdvCategory> AdvCategory { get; set; }
        public virtual DbSet<AdvGroup> AdvGroups { get; set; }
        public virtual DbSet<Advertise> Advertise { get; set; }
        public virtual DbSet<AdvPictures> AdvPictures { get; set; }
        public virtual DbSet<AdvTitles> AdvTitles { get; set; }
        public virtual DbSet<AdvContent> AdvContents { get; set; }
        public virtual DbSet<ChatNumbers> ChatNumbers { get; set; }
        public virtual DbSet<BackUpSetting> BackUpSetting { get; set; }
        public virtual DbSet<Proxy> Proxy { get; set; }
        public virtual DbSet<TelegramBotSetting> TelegramBotSetting { get; set; }
        public virtual DbSet<Naqz> Naqz { get; set; }
        public virtual DbSet<Panels> Panel { get; set; }
        public virtual DbSet<PanelLineNumber> PanelLineNumber { get; set; }
    }

}