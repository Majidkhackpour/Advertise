using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Enums
{
    public static partial class PersianNameAttribute
    {
        public class PersianName : Attribute
        {
            public readonly string Text;

            public PersianName(string text)
            {
                Text = text;
            }
        }
    }
}
