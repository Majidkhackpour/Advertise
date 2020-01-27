using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface.Entities
{
  public  interface ISetting:IHasGuid
    {
        int CountAdvInDayDivar { get; set; }
        int CountAdvInDaySheypoor { get; set; }
        int CountAdvInMounthDivar { get; set; }
        int CountAdvInMounthSheypoor { get; set; }
        int CountAdvInIPDivar { get; set; }
        int CountAdvInIPSheypoor { get; set; }
        string DivarCat1 { get; set; }
        string DivarCat2 { get; set; }
        string DivarCat3 { get; set; }
        string SheypoorCat1 { get; set; }
        string SheypoorCat2 { get; set; }
        string SheypoorCat3 { get; set; }
        int DivarDayCountForUpdateState { get; set; }
        int SheypoorDayCountForUpdateState { get; set; }
        int DivarMaxImgCount { get; set; }
        int SheypoorMaxImgCount { get; set; }
        string AdsAddress { get; set; }
        int DivarDayCountForDelete { get; set; }
    }
}
