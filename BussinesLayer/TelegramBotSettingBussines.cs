using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class TelegramBotSettingBussines : ITelegramBotSetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
        public string ChanelForAds { get; set; }
        public static List<TelegramBotSettingBussines> GetAll()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.TelegramBotSetting.GetAll();
                return Mappings.Default.Map<List<TelegramBotSettingBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<TelegramBotSetting>(this);
                    var res = _context.TelegramBotSetting.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
