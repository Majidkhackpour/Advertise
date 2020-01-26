
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IDivarCityRepository:IRepository<DivarCity>
   {
       DivarCity GetAsync(string city);
       bool Check_Name(string name);
   }
}
