using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
    public interface IAdvertiseLog : IHasGuid
    {
        string Title { get; set; }
        string Content { get; set; }
        long SimCardNumber { get; set; }
        string State { get; set; }
        string City { get; set; }
        string Region { get; set; }
        decimal Price { get; set; }
        string Category { get; set; }
        string SubCategory1 { get; set; }
        string SubCategory2 { get; set; }
        string URL { get; set; }
        string IP { get; set; }
        int VisitCount { get; set; }
        DateTime DateM { get; set; }
        StatusCode StatusCode { get; set; }
        AdvertiseType AdvType { get; set; }
        string ImagePath { get; set; }
        string Adv { get; set; }
        string AdvStatus { get; set; }
    }
}
