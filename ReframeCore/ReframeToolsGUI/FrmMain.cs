using ReframeToolsGUI.Controllers;
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

        Form currentForm = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void displayReactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            formReactors = new FrmReactors();
            DisplayForm(formReactors);
        }

        private void classlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowClassLevelAnalysisForm();
        }

        private void ShowClassLevelAnalysisForm()
        {
            string reactorIdentifier = formReactors.GetSelectedReactorIdentifier();

            FrmClassAnalysis form = new FrmClassAnalysis(reactorIdentifier);
            DisplayForm(form);
        }

        private void showSourceNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowSourceNodes();
        }

        private void DisplayForm(Form form)
        {
            form.MdiParent = this;
            currentForm = form;
            ManageMenuVisibility();
            form.Show();
        }

        private void ManageMenuVisibility()
        {
            analysesToolStripMenuItem.Visible = currentForm is FrmClassAnalysis;
        }

        private void showEntireGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowEntireGraph();
        }

        private void showSinkNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowSinkNodes();
        }

        private void showLeafNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowLeafNodes();
        }

        private void showOrphanNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowOrphanNodes();
        }

        private void showIntermediaryNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAnalysisController controller = new ClassAnalysisController(currentForm as FrmClassAnalysis);
            controller.ShowIntermediaryNodes();
        }
    }
}
