using System;

namespace DataLayer.Interface.Entities
{
   public interface IAdvGroup:IHasGuid
    {
        string Name { get; set; }
        Guid ParentGuid { get; set; }
    }
}
