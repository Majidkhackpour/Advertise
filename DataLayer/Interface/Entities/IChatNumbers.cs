using System;
using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
    public interface IChatNumbers : IHasGuid
    {
        string Number { get; set; }
        AdvertiseType Type { get; set; }
        DateTime DateM { get; set; }
        bool isSendSms { get; set; }
        string City { get; set; }
        string Cat { get; set; }
    }
}
