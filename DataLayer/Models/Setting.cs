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
        public int CountAdvInDayDivar { get; set; }
        public int CountAdvInDaySheypoor { get; set; }
        public int CountAdvInMounthDivar { get; set; }
        public int CountAdvInMounthSheypoor { get; set; }
        public int CountAdvInIPDivar { get; set; }
        public int CountAdvInIPSheypoor { get; set; }
        public int DivarDayCountForUpdateState { get; set; }
        public int SheypoorDayCountForUpdateState { get; set; }
        public int DivarMaxImgCount { get; set; }
        public int SheypoorMaxImgCount { get; set; }
        public int DayCountForDelete { get; set; }
    }
}
