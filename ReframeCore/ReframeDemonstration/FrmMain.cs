using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeDemonstration
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            GUIManager.MainPropertyGrid = propertyGrid;
        }

        private void constructionPartExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConstructionPartExample form = new FrmConstructionPartExample();
            form.MdiParent = this;
            form.Show();
        }
    }
}
