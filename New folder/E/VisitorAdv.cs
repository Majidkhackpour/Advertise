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
    [Table("Adv_VisitorAdv")]
    public class VisitorAdv : IVisitorAdv
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public DateTime Modified { get; set; } = DateTime.Now;
        [Required]
        public Guid VisitorGuid { get; set; }
        [Required]
        public string AdvName { get; set; }
    }
}
