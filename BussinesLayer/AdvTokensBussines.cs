using System;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class AdvTokensBussines : IAdvTokens
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public long Number { get; set; }
        public AdvertiseType Type { get; set; }
        public string Token { get; set; }


        public static AdvTokensBussines GetToken(long number, AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvTokens.GetToken(number, type);
                return Mappings.Default.Map<AdvTokensBussines>(a);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<AdvTokens>(this);
                    var res = _context.AdvTokens.Save(a);
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
