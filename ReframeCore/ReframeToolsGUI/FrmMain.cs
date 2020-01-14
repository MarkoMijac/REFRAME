using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeToolsGUI
{
    public partial class FrmMain : Form
    {
        FrmReactors formReactors;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void displayReactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reactorIdentifier = formReactors.GetSelectedReactorIdentifier();

            FrmClassAnalysis form = new FrmClassAnalysis(reactorIdentifier);
            form.MdiParent = this;
            form.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            formReactors = new FrmReactors();
            formReactors.MdiParent = this;
            formReactors.Show();
            formReactors.WindowState = FormWindowState.Maximized;
        }
    }
}
