using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class BackUpSetting : IBackUpSetting
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string BackUpAddress { get; set; }
        public bool AutoBackUp { get; set; }
        public bool IsSendInTelegram { get; set; }
        public bool IsSendInEmail { get; set; }
        public int? AutoTime { get; set; }
        public string LastBackUpDate { get; set; }
        public string LastBackUpTime { get; set; }
        public string EmailAddress { get; set; }
    }
}
