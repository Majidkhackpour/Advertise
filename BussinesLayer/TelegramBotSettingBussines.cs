using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
    public class TelegramBotSettingBussines : ITelegramBotSetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
        public string ChanelForAds { get; set; }
    }
}
