using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class PanelLineNumber : IPanelLineNumbers
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid PanelGuid { get; set; }
        public long LineNumber { get; set; }
    }
}
