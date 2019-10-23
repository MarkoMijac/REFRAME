using ReframeCore;
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
    public partial class FrmConstructionPartExample : Form
    {
        public FrmConstructionPartExample()
        {
            InitializeComponent();
        }

        private void FrmConstructionPartExample_Load(object sender, EventArgs e)
        {
            Repository.LoadData();
            DependencyManager.Initialize();
            ShowConstructionParts();
            DependencyManager.DefaultGraph.UpdateCompleted += DefaultGraph_UpdateCompleted;
        }

        private void DefaultGraph_UpdateCompleted(object sender, EventArgs e)
        {
            RefreshGui();
        }

        private void ShowConstructionParts()
        {
            dgvConstructionParts.DataSource = Repository.ConstructionParts;
        }

        private void dgvConstructionParts_SelectionChanged(object sender, EventArgs e)
        {
            ConstructionPart cPart = dgvConstructionParts.CurrentRow.DataBoundItem as ConstructionPart;
            GUIManager.ShowObject(cPart);
        }

        private void RefreshGui()
        {
            dgvConstructionParts.Refresh();
            GUIManager.MainPropertyGrid.Refresh();
        }

        private void btnAddConstructionPart_Click(object sender, EventArgs e)
        {
            (DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = true;
            ConstructionPart part = new ConstructionPart();
            Repository.ConstructionParts.Add(part);
            DependencyManager.CreateDependencies(part);
            (DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = false;

            DependencyManager.DefaultGraph.PerformUpdate();

            dgvConstructionParts.DataSource = null;
            dgvConstructionParts.DataSource = Repository.ConstructionParts;
        }
    }
}
