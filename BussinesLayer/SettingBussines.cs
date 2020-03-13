using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class SettingBussines : ISetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public int CountAdvInDay { get; set; }
        public int CountAdvInMounth { get; set; }
        public int CountAdvInIP { get; set; }
        public int DayCountForUpdateState { get; set; }
        public int MaxImgCount { get; set; }
        public string Address { get; set; }
        public Guid? PanelGuid { get; set; }
        public Guid? LineNumberGuid { get; set; }
        public int DayCountForDelete { get; set; }

        public static List<SettingBussines> GetAll()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Settings.GetAll();
                return Mappings.Default.Map<List<SettingBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<Setting>(this);
                    var res = _context.Settings.Save(a);
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
