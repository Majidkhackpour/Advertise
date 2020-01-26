using System.Drawing;
using System.Windows.Forms;

namespace Ads.Classes
{
    public static class lblSetter
    {
        public static void LostFocose(Label lbl)
        {
            lbl.ForeColor = Color.Silver;
        }
        public static void GotFocose(Label lbl)
        {
            lbl.ForeColor = Color.Red;
        }
    }
}
