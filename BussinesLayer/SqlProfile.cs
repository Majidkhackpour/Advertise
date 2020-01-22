using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
