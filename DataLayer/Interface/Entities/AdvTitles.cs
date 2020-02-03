using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Interface.Entities
{
   public class AdvTitles:IAdvTitles
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        public Guid AdvGuid { get; set; }
    }
}
