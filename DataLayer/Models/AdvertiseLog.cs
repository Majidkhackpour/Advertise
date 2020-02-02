using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class AdvertiseLog : IAdvertiseLog
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long SimCardNumber { get; set; }
        [MaxLength(150)]
        public string State { get; set; }
        [MaxLength(150)]
        public string City { get; set; }
        [MaxLength(150)]
        public string Region { get; set; }
        public decimal Price { get; set; }
        [MaxLength(150)]
        public string Category { get; set; }
        [MaxLength(150)]
        public string SubCategory1 { get; set; }
        [MaxLength(150)]
        public string SubCategory2 { get; set; }
        [MaxLength(100)]
        public string URL { get; set; }
        [MaxLength(20)]
        public string IP { get; set; }
        public int VisitCount { get; set; }
        public DateTime DateM { get; set; }
        public StatusCode StatusCode { get; set; }
        public AdvertiseType AdvType { get; set; }
        public string ImagePath { get; set; }
        public string Adv { get; set; }
        [MaxLength(50)]
        public string AdvStatus { get; set; }
    }
}
