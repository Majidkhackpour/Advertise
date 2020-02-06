using System;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IAdvGroupRepositpry : IRepository<AdvGroup>
    {
        bool Check_Name(string Name, Guid guid);
        AdvGroup Change_Status(Guid accGuid, bool state);
        int ChildCounter(Guid guid);
    }
}
