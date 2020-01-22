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
        public DateTime NextUseDivar { get; set; }
        public DateTime NextUseSheypoor { get; set; }
        public DateTime NextUseDivarChat { get; set; }
        public long Number { get; set; }
        public bool Status { get; set; }
        [MaxLength(150)]
        public string Operator { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string OwnerName { get; set; }
    }
}
