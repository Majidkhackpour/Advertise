namespace DataLayer.Interface.Entities
{
   public interface IBackUpSetting:IHasGuid
    {
        string BackUpAddress { get; set; }
        bool AutoBackUp { get; set; }
        bool IsSendInTelegram { get; set; }
        int? AutoTime { get; set; }
    }
}
