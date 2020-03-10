using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class PanelBussines : IPanels
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string API { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDefault { get; set; }
        public static async Task<List<PanelBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Panels.GetAll();
                return Mappings.Default.Map<List<PanelBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<Panels>(this);
                    var res = _context.Panels.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<PanelBussines> GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Panels.Get(guid);
                return Mappings.Default.Map<PanelBussines>(a);
            }
        }
        public static PanelBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Panels.Change_Status(accGuid, status);
                return Mappings.Default.Map<PanelBussines>(a);
            }
        }
    }
}
