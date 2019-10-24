using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeDemonstration
{
    static class GUIManager
    {
        public static Form MdiForm { get; set; }

        public static PropertyGrid MainPropertyGrid { get; set; }

        public static void ShowObject(object obj)
        {
            MainPropertyGrid.SelectedObject = obj;
            MainPropertyGrid.Refresh();
        }

        public static void ShowForm(Form form, bool modal = false)
        {
            if (modal == true)
            {
                form.ShowDialog();
            }
            else
            {
                form.MdiParent = MdiForm;
                form.Show();
            }
        }
    }
}
