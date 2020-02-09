using System;

namespace DataLayer.Interface.Entities
{
   public interface IAdvContent:IHasGuid
    {
        string Content { get; set; }
        Guid AdvGuid { get; set; }
    }
}
