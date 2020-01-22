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
        [PersianNameAttribute.PersianName("چت دیوار")] DivarChat = 2
    }
    public enum StatusCode
    {
        InPublishQueue = 1, //  در صف انتشار,
        Published = 2,//  منتشر شده,
        EditNeeded = 3,// "نیاز به اصلاح"
        WaitForPayment = 4,//  "منتظر پرداخت"
        Failed = 5,//  "رد شده"
        Deleted = 6,//  "حذف شده"
        Expired = 7,//  "منقضی شده"
        InsertError = 10,//  "خطای درج"
        Unknown = 0//  "نامشخص"
    }
}
