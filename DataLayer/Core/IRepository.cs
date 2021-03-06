﻿using System;
using System.Collections.Generic;
using DataLayer.Interface;

namespace DataLayer.Core
{
    public interface IRepository<T> where T : class, IHasGuid, new()
    {
        T Get(Guid guid);
        bool Remove(T item);
        bool RemoveAll(List<T> list);
        List<T> GetAll();
        bool Update(T entity);
        bool Save(T item);
    }
}
