using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface ISheypoorCityRepository : IRepository<SheypoorCity>
    {
        SheypoorCity GetAsync(string city);
        bool Check_Name(string name);
        List<SheypoorCity> GetAllAsync(string search);
    }
}
