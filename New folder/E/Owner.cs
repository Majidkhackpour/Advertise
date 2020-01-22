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
    [Table("Adv_Owner")]
    public class Owner:IOwner
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }

       public DateTime Modified { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Family { get; set; }
        
        public Guid VisitorGuid { get; set; }

        [Required]
        public bool Status { get; set; }

    }
}
