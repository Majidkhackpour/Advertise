using AutoMapper;
using DataLayer.Models;

namespace BussinesLayer
{
   public class SqlProfile:Profile
    {
        public SqlProfile()
        {
            CreateMap<States, StateBussiness>();
            CreateMap<StateBussiness, States>();

            CreateMap<DivarCity, DivarCityBussines>();
            CreateMap<DivarCityBussines, DivarCity>();

            CreateMap<Region, RegionBussiness>();
            CreateMap<RegionBussiness, Region>();

            CreateMap<AdvertiseLog, AdvertiseLogBussines>();
            CreateMap<AdvertiseLogBussines, AdvertiseLog>();

            CreateMap<AdvTokens, AdvTokensBussines>();
            CreateMap<AdvTokensBussines, AdvTokens>();

            CreateMap<Simcard, SimcardBussines>();
            CreateMap<SimcardBussines, Simcard>();

            CreateMap<SimcardAds, SimcardAdsBussines>();
            CreateMap<SimcardAdsBussines, SimcardAds>();

            CreateMap<SheypoorSimCity, SheypoorSimCityBussines>();
            CreateMap<SheypoorSimCityBussines, SheypoorSimCity>();

            CreateMap<SheypoorCity, SheypoorCityBussines>();
            CreateMap<SheypoorCityBussines, SheypoorCity>();

            CreateMap<Setting, SettingBussines>();
            CreateMap<SettingBussines, Setting>();

            CreateMap<DivarSimCityBussines, DivarSimCity>();
            CreateMap<DivarSimCity, DivarSimCityBussines>();

            CreateMap<AdvCategory, AdvCategoryBussines>();
            CreateMap<AdvCategoryBussines, AdvCategory>();

            CreateMap<AdvGroup, AdvGroupBussines>();
            CreateMap<AdvGroupBussines, AdvGroup>();

            CreateMap<Advertise, AdvertiseBussines>();
            CreateMap<AdvertiseBussines, Advertise>();

            CreateMap<AdvPictures, AdvPicturesBussines>();
            CreateMap<AdvPicturesBussines, AdvPictures>();

            CreateMap<AdvTitles, AdvTitlesBussines>();
            CreateMap<AdvTitlesBussines, AdvTitles>();

            CreateMap<AdvContentBussines, AdvContent>();
            CreateMap<AdvContent, AdvContentBussines>();

            CreateMap<ChatNumbers, ChatNumberBussines>();
            CreateMap<ChatNumberBussines, ChatNumbers>();

            CreateMap<BackUpSetting, BackUpSettingBussines>();
            CreateMap<BackUpSettingBussines, BackUpSetting>();
        }
    }
}
