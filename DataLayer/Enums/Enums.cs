using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Enums
{
    public enum AdvertiseType
    {
        [PersianNameAttribute.PersianName("دیوار")] Divar = 0,
        [PersianNameAttribute.PersianName("شیپور")] Sheypoor = 1,
        [PersianNameAttribute.PersianName("چت دیوار")] DivarChat = 2,
        [PersianNameAttribute.PersianName("همه")] All = 100,
    }
    public enum StatusCode
    {
        [PersianNameAttribute.PersianName("در صف انتشار")] InPublishQueue = 1,
        [PersianNameAttribute.PersianName("منتشر شده")] Published = 2,
        [PersianNameAttribute.PersianName("نیاز به اصلاح")] EditNeeded = 3,
        [PersianNameAttribute.PersianName("منتظر پرداخت")] WaitForPayment = 4,
        [PersianNameAttribute.PersianName("رد شده")] Failed = 5,
        [PersianNameAttribute.PersianName("حذف شده")] Deleted = 6,
        [PersianNameAttribute.PersianName("منقضی شده")] Expired = 7,
        [PersianNameAttribute.PersianName("خطای درج")] InsertError = 10,
        [PersianNameAttribute.PersianName("نامشخص")] Unknown = 0
    }
    public enum ProxyType
    {
        [PersianNameAttribute.PersianName("MtProto")] MtProto = 0,
        [PersianNameAttribute.PersianName("Socks5")] Socks5 = 1
    }
    public enum TelegramSendType
    {
        [PersianNameAttribute.PersianName("ارسال فایل پشتیبان")] SendBackUp = 0,
        [PersianNameAttribute.PersianName("ارسال پست تبلیغاتی")] SendPost = 1
    }
}
