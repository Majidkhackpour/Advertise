using System;

namespace DataLayer.Interface.Entities
{
    public interface ISimcard : IHasGuid
    {
        DateTime NextUse { get; set; }
        long Number { get; set; }
        bool Status { get; set; }
        bool IsEnableChat { get; set; }
        bool IsEnableNumber { get; set; }
        string Operator { get; set; }
        string UserName { get; set; }
        string OwnerName { get; set; }
        bool IsSendAdv { get; set; }
        bool IsSendChat { get; set; }
        int ChatCount { get; set; }
        Guid DivarCityForChat { get; set; }
        Guid SheypoorCityForChat { get; set; }
        Guid DivarChatCat1 { get; set; }
        Guid DivarChatCat2 { get; set; }
        Guid DivarChatCat3 { get; set; }
        Guid SheypoorChatCat1 { get; set; }
        Guid SheypoorChatCat2 { get; set; }
        bool isSendSecondChat { get; set; }
        bool isSendPostToTelegram { get; set; }
        string ChannelForSendPost { get; set; }
        int? PostCount { get; set; }
        Guid? CityForGetPost { get; set; }
        Guid? DivarPostCat1 { get; set; }
        Guid? DivarPostCat2 { get; set; }
        Guid? DivarPostCat3 { get; set; }
        string DescriptionForPost { get; set; }
        string FirstChatPassage { get; set; }
        string SecondChatPassage { get; set; }
    }
}
