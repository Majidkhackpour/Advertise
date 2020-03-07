using System;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IPanelsRepository : IRepository<Panels>
    {
        Panels Change_Status(Guid accGuid, bool status);
    }
}
