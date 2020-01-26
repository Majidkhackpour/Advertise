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
        [MaxLength(100)]
        public string DivarCat1 { get; set; }
        [MaxLength(100)]
        public string DivarCat2 { get; set; }
        [MaxLength(100)]
        public string DivarCat3 { get; set; }
        [MaxLength(100)]
        public string SheypoorCat1 { get; set; }
        [MaxLength(100)]
        public string SheypoorCat2 { get; set; }
        [MaxLength(100)]
        public string SheypoorCat3 { get; set; }
        public string DivarPicPath { get; set; }
        public string SheypoorPicPath { get; set; }
        public int DivarDayCountForUpdateState { get; set; }
        public int SheypoorDayCountForUpdateState { get; set; }
        public int DivarMaxImgCount { get; set; }
        public int SheypoorMaxImgCount { get; set; }
        public string AdsAddress { get; set; }
    }
}
