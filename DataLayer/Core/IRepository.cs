using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface;

namespace DataLayer.Core
{
    public interface IRepository<T> where T : class, IHasGuid, new()
    {
        T Get(Guid guid);
        bool Remove(T item, string transActionName = null);
        List<T> GetAll();
        bool Update(T entity);
        bool Save(T item);
    }
}
