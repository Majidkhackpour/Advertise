using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class AdvGroupBussines:IAdvGroup
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid ParentGuid { get; set; }
    }
}
