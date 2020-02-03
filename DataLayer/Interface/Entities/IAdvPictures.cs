using System;

namespace DataLayer.Interface.Entities
{
   public interface IAdvPictures:IHasGuid
    {
        string PathGuid { get; set; }
        Guid AdvGuid { get; set; }
    }
}
