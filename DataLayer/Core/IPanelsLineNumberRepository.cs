using System;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IPanelsLineNumberRepository : IRepository<PanelLineNumber>
    {
        PanelLineNumber Change_Status(Guid accGuid, bool status);
    }
}
