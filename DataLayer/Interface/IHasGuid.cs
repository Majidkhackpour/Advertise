using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IHasGuid
    {
        Guid Guid { get; set; }
        string DateSabt { get; set; }
        bool Status { get; set; }
    }
}
