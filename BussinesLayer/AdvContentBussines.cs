using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
  public  class AdvContentBussines:IAdvContent
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Content { get; set; }
        public Guid AdvGuid { get; set; }
        public static async Task<List<AdvContentBussines>> GetAllAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                if (guid == Guid.Empty) return null;
                var a = _context.AdvContents.GetAllAsync(guid);
                return Mappings.Default.Map<List<AdvContentBussines>>(a);
            }
        }
        public static bool RemoveAll(List<AdvContentBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<AdvContent>>(list);
                    var a = _context.AdvContents.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return false;
            }
        }
    }
}
