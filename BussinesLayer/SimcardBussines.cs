using System;
using System.Collections.Generic;
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
        public DateTime NextUseDivar { get; set; }
        public DateTime NextUseSheypoor { get; set; }
        public DateTime NextUseDivarChat { get; set; }
        public long Number { get; set; }
        public bool Status { get; set; }
        public string Operator { get; set; }
        public string UserName { get; set; }
        public string OwnerName { get; set; }
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
            catch (Exception exception)
            {
            }
        }

        public async static Task<long> GetNextSimCardNumberAsync(AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                return await _context.Simcard.GetNextSimCardNumberAsync(type);
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
    }
}
