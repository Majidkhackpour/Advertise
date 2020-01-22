using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_Tokens")]
    public class AdvTokens:IAdvTokens
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
        public long Number { get; set; }
        public AdvertiseType Type { get; set; }
        public string Token { get; set; }
    }
}
