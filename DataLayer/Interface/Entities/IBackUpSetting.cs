namespace DataLayer.Interface.Entities
{
   public interface IBackUpSetting:IHasGuid
    {
        string BackUpAddress { get; set; }
        bool AutoBackUp { get; set; }
        bool IsSendInTelegram { get; set; }
        bool IsSendInEmail { get; set; }
        int? AutoTime { get; set; }
        string LastBackUpDate { get; set; }
        string LastBackUpTime { get; set; }
        string EmailAddress { get; set; }
    }
}
