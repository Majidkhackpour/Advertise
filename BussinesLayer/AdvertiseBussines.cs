using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using Nito.AsyncEx;

namespace BussinesLayer
{
    public class AdvertiseBussines : IAdvertise
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string AdvName { get; set; }
        public string Content { get; set; }
        public string Price { get; set; }
        public Guid DivarCatGuid1 { get; set; }
        public Guid DivarCatGuid2 { get; set; }
        public Guid DivarCatGuid3 { get; set; }
        public Guid SheypoorCatGuid1 { get; set; }
        public Guid SheypoorCatGuid2 { get; set; }
        public Guid GroupGuid { get; set; }
        public List<AdvTitlesBussines> Titles => AsyncContext.Run(() => AdvTitlesBussines.GetAllAsync(Guid));
        public List<AdvPicturesBussines> Images => AsyncContext.Run(() => AdvPicturesBussines.GetAllAsync(Guid));
        public static async Task<AdvertiseBussines> GetAsync(string city)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Advertise.GetAsync(city);
                return Mappings.Default.Map<AdvertiseBussines>(a);
            }
        }
        public async Task SaveAsync(List<AdvTitlesBussines> lstTitles, List<AdvPicturesBussines> lstImg)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var allTitles = await AdvTitlesBussines.GetAllAsync(Guid);
                    if (!AdvTitlesBussines.RemoveAll(allTitles)) return;
                    var allImg = await AdvPicturesBussines.GetAllAsync(Guid);
                    if (!AdvPicturesBussines.RemoveAll(allImg)) return;

                    if (lstTitles.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<AdvTitles>>(lstTitles);
                        foreach (var item in a1)
                        {
                            var res1 = _context.AdvTitles.Save(item);
                            _context.Set_Save();
                        }
                    }

                    if (lstImg.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<AdvPictures>>(lstImg);
                        foreach (var item in a1)
                        {
                            var res1 = _context.AdvPictures.Save(item);
                            _context.Set_Save();
                        }
                    }

                    var a = Mappings.Default.Map<Advertise>(this);
                    var res = _context.Advertise.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var allTitles = await AdvTitlesBussines.GetAllAsync(Guid);
                    if (!AdvTitlesBussines.RemoveAll(allTitles)) return;
                    var allImg = await AdvPicturesBussines.GetAllAsync(Guid);
                    if (!AdvPicturesBussines.RemoveAll(allImg)) return;
                    
                    var a = Mappings.Default.Map<Advertise>(this);
                    var res = _context.Advertise.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public static async Task<List<AdvertiseBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Advertise.GetAll();
                return Mappings.Default.Map<List<AdvertiseBussines>>(a);
            }
        }
        public static async Task<AdvertiseBussines> GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Advertise.Get(guid);
                return Mappings.Default.Map<AdvertiseBussines>(a);
            }
        }
        public static AdvertiseBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Advertise.Change_Status(accGuid, status);
                return Mappings.Default.Map<AdvertiseBussines>(a);
            }
        }
        public static bool Check_Name(string name, Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.Advertise.Check_Name(name, guid);
        }
    }
}
