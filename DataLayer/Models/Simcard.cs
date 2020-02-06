using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
   public class Simcard:ISimcard
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public DateTime NextUse { get; set; }
        public long Number { get; set; }
        public bool Status { get; set; }
        public bool IsEnableChat { get; set; }
        public bool IsEnableNumber { get; set; }

        [MaxLength(150)]
        public string Operator { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string OwnerName { get; set; }
        public bool IsSendAdv { get; set; }
        public bool IsSendChat { get; set; }
        public Guid? DivarCatGuid1 { get; set; }
        public Guid? DivarCatGuid2 { get; set; }
        public Guid? DivarCatGuid3 { get; set; }
        public Guid? SheypoorCatGuid1 { get; set; }
        public Guid? SheypoorCatGuid2 { get; set; }
        public int ChatCount { get; set; }
    }
}
