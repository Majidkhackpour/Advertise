using System;

namespace DataLayer.Interface.Entities
{
   public interface IPanelLineNumbers:IHasGuid
    {
        Guid PanelGuid { get; set; }
        long LineNumber { get; set; }
    }
}
