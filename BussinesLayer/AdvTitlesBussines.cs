using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class AdvTitlesBussines:IAdvTitles
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public Guid AdvGuid { get; set; }
    }
}
