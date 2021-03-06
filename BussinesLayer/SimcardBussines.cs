﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class SimcardBussines : ISimcard
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public DateTime NextUse { get; set; }
        public long Number { get; set; }
        public bool Status { get; set; }
        public bool IsEnableChat { get; set; }
        public bool IsEnableNumber { get; set; }
        public string Operator { get; set; }
        public string UserName { get; set; }
        public string OwnerName { get; set; }
        public bool IsSendAdv { get; set; }
        public bool IsSendChat { get; set; }
        public int ChatCount { get; set; }
        public Guid DivarCityForChat { get; set; }
        public Guid SheypoorCityForChat { get; set; }
        public Guid DivarChatCat1 { get; set; }
        public Guid DivarChatCat2 { get; set; }
        public Guid DivarChatCat3 { get; set; }
        public Guid SheypoorChatCat1 { get; set; }
        public Guid SheypoorChatCat2 { get; set; }
        public bool isSendSecondChat { get; set; }
        public bool isSendPostToTelegram { get; set; }
        public string ChannelForSendPost { get; set; }
        public int? PostCount { get; set; }
        public Guid? CityForGetPost { get; set; }
        public Guid? DivarPostCat1 { get; set; }
        public Guid? DivarPostCat2 { get; set; }
        public Guid? DivarPostCat3 { get; set; }
        public string DescriptionForPost { get; set; }
        public string FirstChatPassage { get; set; }
        public string SecondChatPassage { get; set; }
        public string FirstChatPassage2 { get; set; }
        public string SecondChatPassage2 { get; set; }
        public string FirstChatPassage3 { get; set; }
        public string SecondChatPassage3 { get; set; }
        public string FirstChatPassage4 { get; set; }
        public string SecondChatPassage4 { get; set; }
        public bool IsSendAdvSheypoor { get; set; }
        public bool IsSendChatSheypoor { get; set; }
        public string SMS_Description { get; set; }

        public bool DivarToken
        {
            get
            {
                var tok = AdvTokensBussines.GetToken(Number, AdvertiseType.Divar)?.Token;
                return !string.IsNullOrEmpty(tok);
            }
        }
        public bool SheypoorToken
        {
            get
            {
                var tok = AdvTokensBussines.GetToken(Number, AdvertiseType.Sheypoor)?.Token;
                return !string.IsNullOrEmpty(tok);
            }
        }
        public bool DivarChetToken
        {
            get
            {
                var tok = AdvTokensBussines.GetToken(Number, AdvertiseType.DivarChat)?.Token;
                return !string.IsNullOrEmpty(tok);
            }
        }

        public static SimcardBussines GetAsync(AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                switch (type)
                {
                    case AdvertiseType.Divar:
                        var a = _context.Simcard.GetAsync(type);
                        return Mappings.Default.Map<SimcardBussines>(a);
                }

                return null;
            }
        }
        public static SimcardBussines GetAsync(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.Get(guid);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }
        public async static Task<SimcardBussines> GetAsync(long number)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.GetAsync(number);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<Simcard>(this);
                    var res = _context.Simcard.Save(a);
                    _context.Set_Save();
                    _context.Dispose();
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public async Task SaveAsync(List<DivarSimCityBussines>lstCity,List<SimcardAdsBussines>lstAds, List<SheypoorSimCityBussines> lstCitysh)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var allCity = await DivarSimCityBussines.GetAllAsync(Guid);
                    if (!DivarSimCityBussines.RemoveAll(allCity)) return;
                    var allCitysh = await SheypoorSimCityBussines.GetAllAsync(Guid);
                    if (!SheypoorSimCityBussines.RemoveAll(allCitysh)) return;
                    var allAds = await SimcardAdsBussines.GetAllAsync(Guid);
                    if (!SimcardAdsBussines.RemoveAll(allAds)) return;

                    if (lstCity.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<DivarSimCity>>(lstCity);
                        foreach (var item in a1)
                        {
                            var res1 = _context.DivarSimCity.Save(item);
                            _context.Set_Save();
                        }
                    }

                    if (lstCitysh.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<SheypoorSimCity>>(lstCitysh);
                        foreach (var item in a1)
                        {
                            var res1 = _context.SheypoorSimCity.Save(item);
                            _context.Set_Save();
                        }
                    }

                    if (lstAds.Count > 0)
                    {
                        var a1 = Mappings.Default.Map<List<SimcardAds>>(lstAds);
                        foreach (var item in a1)
                        {
                            var res1 = _context.SimcardAds.Save(item);
                            _context.Set_Save();
                        }
                    }

                    var a = Mappings.Default.Map<Simcard>(this);
                    var res = _context.Simcard.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        public async static Task<long> GetNextSimCardNumberAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                return await _context.Simcard.GetNextSimCardNumberAsync();
            }
        }

        public static async Task<List<SimcardBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.GetAll();
                return Mappings.Default.Map<List<SimcardBussines>>(a);
            }
        }
        public static async Task<List<SimcardBussines>> GetAllAsync(string search)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = await GetAllAsync();
                if (search == null) return a;
                var res = a.Where(q =>
                        q.OwnerName.Contains(search) || q.Number.ToString().Contains(search) ||
                        q.Operator.Contains(search))
                    .ToList();
                return res;
            }
        }
        public static bool Check_Number(long number, Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.Simcard.Check_Number(number, guid);
        }
        public static SimcardBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Simcard.Change_Status(accGuid, status);
                return Mappings.Default.Map<SimcardBussines>(a);
            }
        }
    }
}
