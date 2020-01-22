using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevComponents.DotNetBar;

namespace AdvertiseApp.Classes
{
    public class Setting
    {
        public static Color TXTColorLosetFocuse
        {
            get
            {
                return System.Drawing.Color.White;
            }
        }
        public static Color TXTBackColorFocused
        {
            get
            {
                return System.Drawing.Color.Gainsboro;
            }
            set
            {
            }
        }

        public bool CheckMobileNumber(string[] number)
        {
            string phone_regexp = "[0-9]{4}[0-9]{3}[0-9]{4}";
            string phone_regexp2 = "[0-9]{3}[0-9]{3}[0-9]{4}";
            bool Res = false;
            foreach (string phone in number)
            {
                Match m = Regex.Match(phone, phone_regexp);
                Match m2 = Regex.Match(phone, phone_regexp2);
                if (m.Success || m2.Success)
                {
                    Res = true;
                }
                else
                {
                    Res = false;
                    return false;
                }
            }

            return Res;
        }

        public static Color GroupBoxGotFocuse
        {
            get
            {
                return ColorTranslator.FromHtml("#5e04d9");
            }
        }
        public static Color GroupBoxlostFocuse
        {
            get
            {
                return ColorTranslator.FromHtml("#14002f");
            }
        }

        public static void SetSelectedGroupBoxBackColor(PanelEx pnl)
        {
            pnl.Style.BackColor1.Color = Color.Silver;
            pnl.Style.BackColor2.Color = Color.Silver;
        }
        public static void SetUnSelectedGroupBoxBackColor(PanelEx pnl)
        {
            pnl.Style.BackColor1.Color = Color.White;
            pnl.Style.BackColor2.Color = Color.White;
        }
        public static void SetSelectedGroupBoxForColor(PanelEx pnl)
        {
            pnl.Style.ForeColor.Color = Color.Red;
        }
        public static void SetUnSelectedGroupBoxForColor(PanelEx pnl)
        {
            pnl.Style.ForeColor.Color = Color.White;
        }
    }
}
