using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class ChatNumbers : IChatNumbers
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(15)]
        public string Number { get; set; }
        public AdvertiseType Type { get; set; }
    }
}
