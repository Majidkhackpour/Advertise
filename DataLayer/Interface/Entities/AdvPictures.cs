using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Interface.Entities
{
   public class AdvPictures:IAdvPictures
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string PathGuid { get; set; }
        public Guid AdvGuid { get; set; }
    }
}
