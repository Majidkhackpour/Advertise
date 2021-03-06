﻿using System;

namespace DataLayer.Interface.Entities
{
  public  interface ISetting:IHasGuid
    {
        int CountAdvInDay { get; set; }
        int CountAdvInMounth { get; set; }
        int CountAdvInIP { get; set; }
        int DayCountForUpdateState { get; set; }
        int MaxImgCount { get; set; }
        string Address { get; set; }
        Guid? PanelGuid { get; set; }
        Guid? LineNumberGuid { get; set; }
        int DayCountForDelete { get; set; }
    }
}
