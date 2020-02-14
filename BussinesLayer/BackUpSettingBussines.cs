using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
    public class BackUpSettingBussines : IBackUpSetting
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string BackUpAddress { get; set; }
        public bool AutoBackUp { get; set; }
        public bool IsSendInTelegram { get; set; }
        public int? AutoTime { get; set; }
    }
}
