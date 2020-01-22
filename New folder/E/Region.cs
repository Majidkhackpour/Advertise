using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_Region")]
    public class Region:IRegion
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
        public Guid CityGuid { get; set; }
        public string Name { get; set; }
        public AdvertiseType Type { get; set; }
    }
}
