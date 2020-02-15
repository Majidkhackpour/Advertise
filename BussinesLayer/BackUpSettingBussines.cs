using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class BackUpSettingBussines : IBackUpSetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string BackUpAddress { get; set; }
        public bool AutoBackUp { get; set; }
        public bool IsSendInTelegram { get; set; }
        public bool IsSendInEmail { get; set; }
        public int? AutoTime { get; set; }
        public string LastBackUpDate { get; set; }
        public string LastBackUpTime { get; set; }
        public string EmailAddress { get; set; }

        public static List<BackUpSettingBussines> GetAll()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.BackUpSetting.GetAll();
                return Mappings.Default.Map<List<BackUpSettingBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<BackUpSetting>(this);
                    var res = _context.BackUpSetting.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
    }
}
