using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class Simcard : ISimcard
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
        public int ChatCount { get; set; }
        public Guid DivarCityForChat { get; set; }
        public Guid SheypoorCityForChat { get; set; }
        public Guid DivarChatCat1 { get; set; }
        public Guid DivarChatCat2 { get; set; }
        public Guid DivarChatCat3 { get; set; }
        public Guid SheypoorChatCat1 { get; set; }
        public Guid SheypoorChatCat2 { get; set; }
        public bool isSendSecondChat { get; set; }
        public bool isSendPostToTelegram { get; set; }
        [MaxLength(100)]
        public string ChannelForSendPost { get; set; }
        public int? PostCount { get; set; }
        public Guid? CityForGetPost { get; set; }
        public Guid? DivarPostCat1 { get; set; }
        public Guid? DivarPostCat2 { get; set; }
        public Guid? DivarPostCat3 { get; set; }
        public string DescriptionForPost { get; set; }
        public string FirstChatPassage { get; set; }
        public string SecondChatPassage { get; set; }
    }
}
