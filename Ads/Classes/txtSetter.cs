using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Ads.Classes
{
    public static class txtSetter
    {
        public static void Focus(DevComponents.DotNetBar.Controls.TextBoxX txt = null, TextBox txt2 = null)
        {
            if (txt == null)
            {
                txt2.BackColor = Color.White;
                txt2.ForeColor = Color.Black;
                return;
            }
            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;
        }

        public static void Follow(DevComponents.DotNetBar.Controls.TextBoxX txt = null, TextBox txt2 = null)
        {
            Color colour = ColorTranslator.FromHtml("#17212b");
            if (txt == null)
            {
                txt2.BackColor = colour;
                txt2.ForeColor = Color.White;
                return;
            }
            txt.BackColor = colour;
            txt.ForeColor = Color.White;
        }

        public static void KeyPress_Whit_Dot(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) <= 57 & Convert.ToInt32(e.KeyChar) >= 45 || Convert.ToInt32(e.KeyChar) <= 1785 & Convert.ToInt32(e.KeyChar) >= 1776 || Convert.ToInt32(e.KeyChar) == 8)
            {
            }
            else
                e.KeyChar = Convert.ToChar(Keys.None);
        }
        public static void KeyPress_Whitout_Dot(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) <= 57 & Convert.ToInt32(e.KeyChar) >= 48 || Convert.ToInt32(e.KeyChar) <= 1785 & Convert.ToInt32(e.KeyChar) >= 1776 || Convert.ToInt32(e.KeyChar) == 8)
            {
            }
            else
                e.KeyChar = Convert.ToChar(Keys.None);
        }

        public static void KeyDown(object sender, KeyEventArgs e, DevComponents.DotNetBar.ButtonX btn)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.F5)
                btn.PerformClick();
        }

        //public static void Three_Ziro(TextBox txt)
        //{
        //    txt.Text = txt.Text.Replace(".", "000");
        //    txt.Text = txt.Text.Replace("+", "00");
        //    txt.Text = txt.Text.ParseToDecimal().ThreeSeparator();
        //    txt.SelectionStart = txt.Text.Length;
        //}
        public static void Switch_Language_To_English()
        {
            CultureInfo LAN = new CultureInfo("en-us");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(LAN);
        }
        public static void Switch_Language_To_Persian()
        {
            CultureInfo LAN = new CultureInfo("fa-ir");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(LAN);
        }
    }
}
