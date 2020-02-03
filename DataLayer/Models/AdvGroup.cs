using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class AdvGroup : IAdvGroup
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid ParentGuid { get; set; }
    }
}
