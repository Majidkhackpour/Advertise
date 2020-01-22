using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_Visitor")]
    public class Visitor : IVisitor
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }

        public DateTime Modified { get; set; }


        [Required]
        public short MasterCount { get; set; }

        [Required]
        public short SlaveCount { get; set; }

        [Required]
        public bool IsAgent { get; set; }


        [MaxLength(200)]
        [Required]
        public string AdvReplacement { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string FullName { get; set; }

        [Required]
        public bool Status { get; set; }//استفاده جهت Delete

        [Required]
        [MaxLength(7)]
        public string Enable_Status { get; set; }//بررسی وضعیت 

        public string Phone { get; set; }
    }
}
