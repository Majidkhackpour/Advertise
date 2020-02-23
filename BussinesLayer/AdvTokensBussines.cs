using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class AdvTokensBussines : IAdvTokens
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public long Number { get; set; }
        public AdvertiseType Type { get; set; }
        public string Token { get; set; }

        public static List<AdvTokensBussines> GetAllAsync(AdvertiseType type, long number)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvTokens.GetAll(type, number);
                return Mappings.Default.Map<List<AdvTokensBussines>>(a);
            }
        }
        public static AdvTokensBussines GetToken(long number, AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvTokens.GetToken(number, type);
                return Mappings.Default.Map<AdvTokensBussines>(a);
            }
        }
        public static bool RemoveAll(List<AdvTokensBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<AdvTokens>>(list);
                    var a = _context.AdvTokens.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task SaveAsync(AdvertiseType type, long number)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var all = GetAllAsync(type, number);
                    if (all.Count > 0)
                    {
                        if (!RemoveAll(all)) return;
                    }
                    var a = Mappings.Default.Map<AdvTokens>(this);
                    var res = _context.AdvTokens.Save(a);
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
