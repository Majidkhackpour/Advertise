using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class StateBussiness : IState
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<States>(this);
                    var res = _context.State.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public static bool RemoveAll(List<StateBussiness> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<States>>(list);
                    var a = _context.State.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static async Task<List<StateBussiness>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.State.GetAll();
                return Mappings.Default.Map<List<StateBussiness>>(a);
            }
        }
        public static StateBussiness Get(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.State.Get(guid);
                return Mappings.Default.Map<StateBussiness>(a);
            }
        }
    }
}
