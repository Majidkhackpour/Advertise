using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_City")]
    public class City : ICity
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public DateTime Modified { get; set; }
        [Required]
        [MaxLength(200)]
        public string CityName { get; set; }
        public Guid StateGuid { get; set; }
        public int Weight { get; set; }

        public bool isDivarCity { get; set; }

        public bool isSheypoorCity { get; set; }

        public bool isNiazKadeCity { get; set; }

        public bool isNiazmandyHaCity { get; set; }
        //public int StartPoint { get; set; }
        //public int EndPoint { get; set; }
    }
}
