using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PacketParser.Entities.Interfaces.Advertise;

namespace AccSqlServerPersistence.Entities.Advertise
{
    [Table("Adv_SimCard")]
    public class SimCard : ISimCard
    {
        public DateTime SheypoorModified { get; set; }
        public DateTime NiazModified { get; set; }
        public DateTime NiazmandyHaModified { get; set; }
        public DateTime NextUseDivar { get; set; }
        public DateTime NextUseSheypoor { get; set; }
        public DateTime NextUseNiazKade { get; set; }
        public DateTime NextUseNiazmandyHa { get; set; }
        public bool isSheypoorBlocked { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Number { get; set; }

        [Required]
        public Guid OwnerGuid { get; set; }

        [DefaultValue(0)]
        public int UseCount { get; set; }

        [DefaultValue("true")]
        public bool Status { get; set; }// استقاده جهت حذف کردن

        [Key]
        public Guid Guid { get; set; }

        [DefaultValue("getdate()")]
        public DateTime Modified { get; set; }

        public DateTime DivarModified { get; set; }

        [Required]
        [MaxLength(7)]
        public string Enable_Status { get; set; }//بررسی وضعیت 

        public string Operator { get; set; }
        public string UserName { get; set; }
        public short CountDivarAdvInDay { get; set; }
        public short CountDivarAdvInMonth { get; set; }
        public short CountSheypoorAdvInDay { get; set; }
        public short CountSheypoorAdvInMonth { get; set; }
        public short CountNiazAdvInDay { get; set; }
        public short CountNiazAdvInMonth { get; set; }
        public short CountNiazmandyHaAdvInDay { get; set; }
        public short CountNiazmandyHaAdvInMonth { get; set; }
        public string SheypoorToken { get; set; }
        public string DivarToken { get; set; }
        public string NiazToken { get; set; }
    }
}
