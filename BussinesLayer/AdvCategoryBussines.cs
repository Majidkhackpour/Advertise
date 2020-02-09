using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class AdvCategoryBussines : IAdvCategory
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid ParentGuid { get; set; }
        public AdvertiseType Type { get; set; }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<AdvCategory>(this);
                    var res = _context.AdvCategory.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public static async Task<List<AdvCategoryBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvCategory.GetAll();
                return Mappings.Default.Map<List<AdvCategoryBussines>>(a);
            }
        }
        public static async Task<List<AdvCategoryBussines>> GetAllAsync(Guid guid, AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvCategory.GetAllAsync(guid, type);
                return Mappings.Default.Map<List<AdvCategoryBussines>>(a);
            }
        }
        public static bool RemoveAllAsync(List<AdvCategoryBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<AdvCategory>>(list);
                    var a = _context.AdvCategory.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static AdvCategoryBussines Get(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                if (guid == System.Guid.Empty) return null;
                var a = _context.AdvCategory.Get(guid);
                return Mappings.Default.Map<AdvCategoryBussines>(a);
            }
        }
    }
}
