﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class ChatNumberBussines : IChatNumbers
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; } = DateConvertor.M2SH(DateTime.Now);
        public bool Status { get; set; } = true;
        public string Number { get; set; }
        public AdvertiseType Type { get; set; }
        public DateTime DateM { get; set; } = DateTime.Now;
        public bool isSendSms { get; set; } = false;
        public bool isChecked { get; set; }
        public string TypeName => Type.GetDisplay();
        public static List<ChatNumberBussines> GetAll(AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.ChatNumbers.GetAll(type);
                return Mappings.Default.Map<List<ChatNumberBussines>>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var num = Number.ParseToInt();
                    if (num == 0) return;
                    var chn = Get(Number);
                    if (chn != null)
                    {
                        chn.DateM = DateM;
                        var a1 = Mappings.Default.Map<ChatNumbers>(chn);
                        var res1 = _context.ChatNumbers.Save(a1);
                        _context.Set_Save();
                        _context.Dispose();
                        return;
                    }

                    var a = Mappings.Default.Map<ChatNumbers>(this);
                    var res = _context.ChatNumbers.Save(a);
                    _context.Set_Save();
                    _context.Dispose();
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static List<ChatNumberBussines> GetAll()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.ChatNumbers.GetAll();
                return Mappings.Default.Map<List<ChatNumberBussines>>(a);
            }
        }

        private static ChatNumberBussines Get(string number)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.ChatNumbers.Get(number);
                return Mappings.Default.Map<ChatNumberBussines>(a);
            }
        }
    }
}
