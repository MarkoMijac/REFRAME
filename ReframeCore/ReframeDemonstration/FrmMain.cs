using ReframeDemonstration.BusinessLogic;
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
            GUIManager.MdiForm = this;
        }

        private void constructionPartExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIManager.ShowForm(new FrmConstructionPartExample());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Repository.LoadData();
            DependencyManager.DefaultGraph.Initialize();
            DependencyManager.DefaultGraph.PerformUpdate();
        }
    }
}
