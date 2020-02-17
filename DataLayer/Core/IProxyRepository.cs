using System;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IProxyRepository : IRepository<Proxy>
    {
        Proxy Change_Status(Guid accGuid, bool status);
    }
}
