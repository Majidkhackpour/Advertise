using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
   public class Setting:ISetting
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public int CountAdvInDay { get; set; }
        public int CountAdvInMounth { get; set; }
        public int CountAdvInIP { get; set; }
        public int DayCountForUpdateState { get; set; }
        public int MaxImgCount { get; set; }
        public string Address { get; set; }
        public Guid? PanelGuid { get; set; }
        public Guid? LineNumberGuid { get; set; }
    }
}
