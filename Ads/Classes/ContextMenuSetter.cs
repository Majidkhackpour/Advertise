using System.Drawing;
using System.Windows.Forms;

namespace Ads.Classes
{
   public class ContextMenuSetter:ProfessionalColorTable
    {
        Color colour = ColorTranslator.FromHtml("#17212b");
        public override Color ToolStripDropDownBackground
        {
            get { return colour; }
        }
        public override Color MenuBorder
        {
            get { return colour; }
        }
        public override Color MenuItemBorder
        {
            get { return Color.White; }
        }
        public override Color MenuItemSelected
        {
            get { return colour; }
        }
    }
}
