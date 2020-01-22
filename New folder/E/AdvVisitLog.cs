using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities;
using PacketParser.Entities.Interfaces.Advertise;
namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_VisitLog")]
    public class AdvVisitLog : IAdvVisitLog
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
        public Guid AdvGuid { get; set; }
        public DateTime DateM { get; set; }
        public int VisitCount { get; set; }
        public string AdvStatus { get; set; }
        public short StatusCode { get; set; }
        public AdvertiseType Type { get; set; }
    }
}
