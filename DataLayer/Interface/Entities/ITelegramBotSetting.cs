namespace DataLayer.Interface.Entities
{
   public interface ITelegramBotSetting:IHasGuid
    {
        string Token { get; set; }
        string ChanelForAds { get; set; }
    }
}
