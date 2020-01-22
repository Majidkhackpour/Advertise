using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
   public class DivarCityBussines:IDivarCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public bool Is_Checked { get; set; }

        public static async Task<List<DivarCityBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.GetAll();
                return Mappings.Default.Map<List<DivarCityBussines>>(a);
            }
        }
        public static DivarCityBussines GetAsync(string city)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.GetAsync(city);
                return Mappings.Default.Map<DivarCityBussines>(a);
            }
        }
        public static DivarCityBussines GetAsync(Guid cityGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.Get(cityGuid);
                return Mappings.Default.Map<DivarCityBussines>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<DivarCity>(this);
                    var res = _context.City.Save(a);
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
