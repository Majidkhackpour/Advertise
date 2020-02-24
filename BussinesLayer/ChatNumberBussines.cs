using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class ChatNumberBussines : IChatNumbers
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Number { get; set; }
        public AdvertiseType Type { get; set; }
        public static List<ChatNumberBussines> GetAll(AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.ChatNumbers.GetAll(type);
                return Mappings.Default.Map<List<ChatNumberBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<ChatNumbers>(this);
                    var res = _context.ChatNumbers.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
