﻿using System;
using System.Collections.Generic;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class NaqzBussines : INaqz
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public static List<NaqzBussines> GetAll()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Naqz.GetAll();
                return Mappings.Default.Map<List<NaqzBussines>>(a);
            }
        }
        public static void Save(List<NaqzBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var lst = GetAll();
                    if (lst.Count > 0) return;
                    foreach (var item in list)
                    {
                        var a = Mappings.Default.Map<Naqz>(item);
                        var res = _context.Naqz.Save(a);
                    }
                    _context.Set_Save();
                    _context.Dispose();
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
