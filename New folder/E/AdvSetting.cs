using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PacketParser.Entities;
using PacketParser.Entities.Interfaces.Advertise;
using PacketParser.Settings.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_Settings")]
    public class AdvSetting : Serializable<IAdvSetting>, IAdvSetting
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
        [JsonIgnore]
        [NotMapped]
        public DivarSettingInformations DivarSetting { get; set; }
        [JsonIgnore]
        [NotMapped]
        public SheypoorSettingInformations SheypoorSetting { get; set; }
        [JsonIgnore]
        [NotMapped]
        public NiazKadeSettingInformation NiazSetting { get; set; }
        [JsonIgnore]
        [NotMapped]
        public NiazmandyHaSettingInformations NiazmandyHaSetting { get; set; }
        public string NiazData { get; set; }
        public string DivarData { get; set; }
        public string SheypoorData { get; set; }
        public string NiazmandyHaData { get; set; }
    }
}
