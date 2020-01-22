using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_VisitorCity")]
    public class VisitorCity : IVisitorCity
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public DateTime Modified { get; set; }=DateTime.Now;
        [Required]
        public Guid VisitorGuid { get; set; }
        [Required]
        public Guid CityGuid { get; set; }
    }
}
