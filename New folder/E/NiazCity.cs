﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_NiazKadeCities")]
    public class NiazCity:INiazCities
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
        [MaxLength(200)]
        public string CityName { get; set; }
    }
}
