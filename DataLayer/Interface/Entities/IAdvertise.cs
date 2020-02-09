using System;

namespace DataLayer.Interface.Entities
{
   public interface IAdvertise:IHasGuid
    {
        string AdvName { get; set; }
        string Price { get; set; }
        Guid DivarCatGuid1 { get; set; }
        Guid DivarCatGuid2 { get; set; }
        Guid DivarCatGuid3 { get; set; }
        Guid SheypoorCatGuid1 { get; set; }
        Guid SheypoorCatGuid2 { get; set; }
        Guid GroupGuid { get; set; }
    }
}
