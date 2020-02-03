using System;

namespace DataLayer.Interface.Entities
{
   public interface IAdvTitles:IHasGuid
    {
        string Title { get; set; }
        Guid AdvGuid { get; set; }
    }
}
