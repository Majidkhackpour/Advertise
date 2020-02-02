using System;

namespace DataLayer.Interface.Entities
{
   public interface ISimcard:IHasGuid
    {
        DateTime NextUse { get; set; }
        long Number { get; set; }
        bool Status { get; set; }
        string Operator { get; set; }
        string UserName { get; set; }
        string OwnerName { get; set; }
        bool IsSendAdv { get; set; }
        bool IsSendChat { get; set; }
        Guid? DivarCatGuid1 { get; set; }
        Guid? DivarCatGuid2 { get; set; }
        Guid? DivarCatGuid3 { get; set; }
        Guid? SheypoorCatGuid1 { get; set; }
        Guid? SheypoorCatGuid2 { get; set; }
    }
}
