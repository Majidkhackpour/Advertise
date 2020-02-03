﻿using System;
using DataLayer.Context;
using DataLayer.Core;

namespace DataLayer.Persitence
{
    public class UnitOfWorkLid : IDisposable
    {
        private readonly dbContext db = new dbContext();
        private IStateRepository _stateRepository;
        private IDivarCityRepository _divarCityRepository;
        private IRegionRepository _regionRepository;
        private IAdvertiseLogRepository _advertiseLogRepository;
        private IAdvTokensRepository _advTokensRepository;
        private ISimcardRepository _simcardRepository;
        private ISimcardAdsRepository _simcardAdsRepository;
        private ISheypoorSimCityRepository _sheypoorsimCityRepository;
        private ISheypoorCityRepository _sheypoorCityRepository;
        private ISettingRepository _settingRepository;
        private IDivarSimCityRepository _divarSimCityRepository;
        private IAdvCategoryRepository _advCategoryRepository;
        private IAdvGroupRepositpry _advGroupRepository;
        private IAdvertiseRepository _advertiseRepository;
        private IAdvPicturesRepository _advPicturesRepository;
        private IAdvTitlesRepository _advTitlesRepository;

        public void Dispose()
        {
            db?.Dispose();
        }
        public void Set_Save()
        {
            db.SaveChanges();
        }
        public IStateRepository State => _stateRepository ?? (_stateRepository = new StateOersistenceRepository(db));
        public IRegionRepository Region => _regionRepository ?? (_regionRepository = new RegionPersistenceRepository(db));
        public IDivarCityRepository City => _divarCityRepository ?? (_divarCityRepository = new DivarCityPersistenceRepository(db));
        public IAdvertiseLogRepository AdvertiseLog => _advertiseLogRepository ?? (_advertiseLogRepository = new AdvertiseLogPersistenceRepository(db));
        public IAdvTokensRepository AdvTokens => _advTokensRepository ?? (_advTokensRepository = new AdvTokensPersistenceRepository(db));
        public ISimcardRepository Simcard => _simcardRepository ?? (_simcardRepository = new SimcardPersistendeRepository(db));
        public ISimcardAdsRepository SimcardAds => _simcardAdsRepository ?? (_simcardAdsRepository = new SimcardAdsPersistenceRepository(db));

        public ISheypoorSimCityRepository SheypoorSimCity =>
            _sheypoorsimCityRepository ?? (_sheypoorsimCityRepository = new SheypoorSimCityPersistenceRepository(db));
        public ISheypoorCityRepository SheypoorCity =>
            _sheypoorCityRepository ?? (_sheypoorCityRepository = new SheypoorCityPersistenceRepository(db));
        public ISettingRepository Settings =>
            _settingRepository ?? (_settingRepository = new SettingPersistenceRepository(db));
        public IDivarSimCityRepository DivarSimCity =>
            _divarSimCityRepository ?? (_divarSimCityRepository = new DivarSimCityPersistenceRepository(db));
        public IAdvCategoryRepository AdvCategory =>
            _advCategoryRepository ?? (_advCategoryRepository = new AdvCategoryPersistenceRepository(db));
        public IAdvGroupRepositpry AdvGroup => _advGroupRepository ?? (_advGroupRepository = new AdvGroupPersistenceRepository(db));
        public IAdvertiseRepository Advertise => _advertiseRepository ?? (_advertiseRepository = new AdvertisePersistenceRepository(db));
        public IAdvPicturesRepository AdvPictures => _advPicturesRepository ?? (_advPicturesRepository = new AdvPicturesPersistenceRepository(db));
        public IAdvTitlesRepository AdvTitles => _advTitlesRepository ?? (_advTitlesRepository = new AdvTitlesPersistenceRepository(db));
    }
}
