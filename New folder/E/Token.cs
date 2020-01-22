using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_Token")]
    public class Token : IToken
    {
        [Index(IsUnique = true)]
        [Required]
        public long Number { get; set; }

        [MaxLength(500)]
        public string DivarToken { get; set; }

        [Required]
        public bool Status { get; set; }
        [MaxLength(500)]
        public string SheypoorToken { get; set; }

        [Key]
        public Guid Guid { get; set; }
        public DateTime Modified { get; set; }
    }


}
