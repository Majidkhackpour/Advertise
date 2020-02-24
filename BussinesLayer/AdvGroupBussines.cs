using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
   public class AdvGroupBussines:IAdvGroup
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid ParentGuid { get; set; }

        public static async Task<List<AdvGroupBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvGroup.GetAll();
                return Mappings.Default.Map<List<AdvGroupBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {

                    var a = Mappings.Default.Map<AdvGroup>(this);
                    var res = _context.AdvGroup.Save(a);
                    _context.Set_Save();

                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<AdvGroupBussines> GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvGroup.Get(guid);
                return Mappings.Default.Map<AdvGroupBussines>(a);
            }
        }
        public static AdvGroupBussines Get(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvGroup.Get(guid);
                return Mappings.Default.Map<AdvGroupBussines>(a);
            }
        }
        public static AdvGroupBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvGroup.Change_Status(accGuid, status);
                return Mappings.Default.Map<AdvGroupBussines>(a);
            }
        }
        public static bool Check_Name(string name, Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.AdvGroup.Check_Name(name, guid);
        }

        public static int ChildCounter(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.AdvGroup.ChildCounter(guid);
        }

    }
}
