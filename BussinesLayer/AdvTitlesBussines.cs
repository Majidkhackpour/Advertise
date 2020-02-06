using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
   public class AdvTitlesBussines:IAdvTitles
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public Guid AdvGuid { get; set; }
        public static async Task<List<AdvTitlesBussines>> GetAllAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                if (guid == Guid.Empty) return null;
                var a = _context.AdvTitles.GetAllAsync(guid);
                return Mappings.Default.Map<List<AdvTitlesBussines>>(a);
            }
        }
        public static bool RemoveAll(List<AdvTitlesBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<AdvTitles>>(list);
                    var a = _context.AdvTitles.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
