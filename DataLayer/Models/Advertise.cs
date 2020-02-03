using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
   public class Advertise:IAdvertise
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(150)]
        public string AdvName { get; set; }
        public string Content { get; set; }
        [MaxLength(15)]
        public string Price { get; set; }
        public Guid DivarCatGuid1 { get; set; }
        public Guid DivarCatGuid2 { get; set; }
        public Guid DivarCatGuid3 { get; set; }
        public Guid SheypoorCatGuid1 { get; set; }
        public Guid SheypoorCatGuid2 { get; set; }
    }
}
