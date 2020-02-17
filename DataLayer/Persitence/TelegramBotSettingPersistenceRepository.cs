using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    public class TelegramBotSettingPersistenceRepository : GenericRepository<TelegramBotSetting>, ITelegramBotSettingRepository
    {
        private dbContext db;

        public TelegramBotSettingPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }
    }
}
