using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class AdvPicturesBussines:IAdvPictures
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string PathGuid { get; set; }
        public Guid AdvGuid { get; set; }
    }
}
