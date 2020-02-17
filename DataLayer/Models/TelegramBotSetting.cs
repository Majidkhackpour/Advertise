using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class TelegramBotSetting : ITelegramBotSetting
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(150)]
        public string Token { get; set; }
        [MaxLength(50)]
        public string ChanelForAds { get; set; }
    }
}
