
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
        int DivarDayCountForUpdateState { get; set; }
        int SheypoorDayCountForUpdateState { get; set; }
        int DivarMaxImgCount { get; set; }
        int SheypoorMaxImgCount { get; set; }
        string AdsAddress { get; set; }
        int DayCountForDelete { get; set; }
    }
}
