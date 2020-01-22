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
    [Table("Adv_State")]
    public class State : IState
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public DateTime Modified { get; set; }=DateTime.Now;
        [Required]
        public string StateName { get; set; }
    }
}
