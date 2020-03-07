using System;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
    public class PanelLineNumberBussines : IPanelLineNumbers
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid PanelGuid { get; set; }
        public int LineNumber { get; set; }
    }
}
