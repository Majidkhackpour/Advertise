using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class SimcardBussines : ISimcard
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public DateTime NextUse { get; set; }
        public long Number { get; set; }
        public bool Status { get; set; }
        public string Operator { get; set; }
        public string UserName { get; set; }
        public string OwnerName { get; set; }
        public bool IsSendAdv { get; set; }
        public bool IsSendChat { get; set; }
        public Guid? DivarCatGuid1 { get; set; }
        public Guid? DivarCatGuid2 { get; set; }
        public Guid? DivarCatGuid3 { get; set; }
        public Guid? SheypoorCatGuid1 { get; set; }
        public Guid? SheypoorCatGuid2 { get; set; }

        public static SimcardBussines GetAsync(AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                switch (type)
                {
                    case AdvertiseType.Divar:
                        var a = _context.Simcard.GetAsync(type);
                        return Mappings.Default.Map<SimcardBussines>(a);
                        break;
                }

                return null;
            }
        }
        public static SimcardBussines GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.Get(guid);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }
        public async static Task<SimcardBussines> GetAsync(long number)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.GetAsync(number);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<Simcard>(this);
                    var res = _context.Simcard.Save(a);
                    _context.Set_Save();
                    _context.Dispose();
                }
            }
            catch (Exception e)
            {
            }
        }
        public async Task SaveAsync(List<DivarSimCityBussines>lstCity,List<SimcardAdsBussines>lstAds)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var allCity = await DivarSimCityBussines.GetAllAsync(Guid);
                    if (!DivarSimCityBussines.RemoveAll(allCity)) return;
                    var allAds = await SimcardAdsBussines.GetAllAsync(Guid);
                    if (!SimcardAdsBussines.RemoveAll(allAds)) return;

                    if (lstCity.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<DivarSimCity>>(lstCity);
                        foreach (var item in a1)
                        {
                            var res1 = _context.DivarSimCity.Save(item);
                            _context.Set_Save();
                        }
                    }

                    if (lstAds.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<SimcardAds>>(lstAds);
                        foreach (var item in a1)
                        {
                            var res1 = _context.SimcardAds.Save(item);
                            _context.Set_Save();
                        }
                    }

                    var a = Mappings.Default.Map<Simcard>(this);
                    var res = _context.Simcard.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }

        public async static Task<long> GetNextSimCardNumberAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                return await _context.Simcard.GetNextSimCardNumberAsync();
            }
        }

        public static async Task<List<SimcardBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.GetAll();
                return Mappings.Default.Map<List<SimcardBussines>>(a);
            }
        }
        public static async Task<List<SimcardBussines>> GetAllAsync(string search)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = await GetAllAsync();
                if (search == null) return a;
                var res = a.Where(q =>
                        q.OwnerName.Contains(search) || q.Number.ToString().Contains(search) ||
                        q.Operator.Contains(search))
                    .ToList();
                return res;
            }
        }
        public static bool Check_Number(long number, Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.Simcard.Check_Number(number, guid);
        }
        public static SimcardBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.Change_Status(accGuid, status);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }
    }
}
