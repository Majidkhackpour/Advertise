using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;
using Nito.AsyncEx;

namespace BussinesLayer
{
    public class PanelLineNumberBussines : IPanelLineNumbers
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid PanelGuid { get; set; }
        public long LineNumber { get; set; }
        public bool IsDefault { get; set; }
        public string PanelName => AsyncContext.Run(() => PanelBussines.GetAsync(PanelGuid).Result.Name);
        public static async Task<List<PanelLineNumberBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.PanelLineNumber.GetAll();
                return Mappings.Default.Map<List<PanelLineNumberBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<PanelLineNumber>(this);
                    var res = _context.PanelLineNumber.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<PanelLineNumberBussines> GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.PanelLineNumber.Get(guid);
                return Mappings.Default.Map<PanelLineNumberBussines>(a);
            }
        }
        public static PanelLineNumberBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.PanelLineNumber.Change_Status(accGuid, status);
                return Mappings.Default.Map<PanelLineNumberBussines>(a);
            }
        }
    }
}
