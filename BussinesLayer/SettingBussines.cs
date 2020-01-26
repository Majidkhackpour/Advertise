using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
   public class SettingBussines:ISetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public int CountAdvInDayDivar { get; set; }
        public int CountAdvInDaySheypoor { get; set; }
        public int CountAdvInMounthDivar { get; set; }
        public int CountAdvInMounthSheypoor { get; set; }
        public int CountAdvInIPDivar { get; set; }
        public int CountAdvInIPSheypoor { get; set; }
        public string DivarCat1 { get; set; }
        public string DivarCat2 { get; set; }
        public string DivarCat3 { get; set; }
        public string SheypoorCat1 { get; set; }
        public string SheypoorCat2 { get; set; }
        public string SheypoorCat3 { get; set; }
        public string DivarPicPath { get; set; }
        public string SheypoorPicPath { get; set; }
        public int DivarDayCountForUpdateState { get; set; }
        public int SheypoorDayCountForUpdateState { get; set; }
        public int DivarMaxImgCount { get; set; }
        public int SheypoorMaxImgCount { get; set; }
        public string AdsAddress { get; set; }

        public static async Task<List<SettingBussines>> GetAllAsync()
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
            }
        }
    }
}
