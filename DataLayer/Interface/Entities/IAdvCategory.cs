
using System;
using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
   public interface IAdvCategory:IHasGuid
    {
        string Name { get; set; }
        Guid ParentGuid { get; set; }
        AdvertiseType Type { get; set; }
    }
}
