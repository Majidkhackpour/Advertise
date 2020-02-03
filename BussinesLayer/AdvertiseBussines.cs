using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class AdvertiseBussines:IAdvertise
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string AdvName { get; set; }
        public string Content { get; set; }
        public string Price { get; set; }
        public Guid DivarCatGuid1 { get; set; }
        public Guid DivarCatGuid2 { get; set; }
        public Guid DivarCatGuid3 { get; set; }
        public Guid SheypoorCatGuid1 { get; set; }
        public Guid SheypoorCatGuid2 { get; set; }
    }
}
