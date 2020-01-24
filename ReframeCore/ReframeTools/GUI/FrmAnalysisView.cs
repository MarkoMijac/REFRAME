using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.GUI
{
    public partial class FrmAnalysisView : Form
    {
        public FrmAnalysisView()
        {
            InitializeComponent();
        }

        public string ReactorIdentifier { get; set; }
        protected AnalysisController AnalysisController { get; set; }
        protected VisualizationController VisualizationController { get; set; }
        public FrmAnalysisView(string reactorIdentifier) : this()
        {
            ReactorIdentifier = reactorIdentifier;
            AddColumns();
        }

        protected virtual void SetFormTitle()
        {
            
        }

        private void FrmObjectMemberAnalysis_Load(object sender, EventArgs e)
        {
            SetFormTitle();
        }

        public virtual void ShowAnalysis(IEnumerable<IAnalysisNode> nodes)
        {
            dgvNodes.Rows.Clear();
        }

        protected virtual void AddColumns()
        {
            
        }

        protected virtual void showEntireGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowEntireGraph();
        }

        protected virtual void showSourceNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSourceNodes();
        }

        protected virtual void showSinkNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSinkNodes();
        }

        protected virtual void showLeafNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowLeafNodes();
        }

        protected virtual void showOrphanNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowOrphanNodes();
        }

        protected virtual void showIntermediaryNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowIntermediaryNodes();
        }

        private string GetSelectedNodeIdentifier()
        {
            string identifier = "";

            if (dgvNodes.Rows.Count > 0)
            {
                identifier = dgvNodes.CurrentRow.Cells[0].Value.ToString();
            }

            return identifier;
        }

        private void showNodePredecessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowPredecessorNodes(GetSelectedNodeIdentifier());
        }

        private void showSuccessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSuccessorNodes(GetSelectedNodeIdentifier());
        }

        private void showNodesNeighboursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowNeighbourNodes(GetSelectedNodeIdentifier());
        }

        private void displayEntireGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayEntireGraph();
        }

        private void displaySourceNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplaySourceNodes();
        }

        private void displaySinkNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplaySinkNodes();
        }

        private void displayLeafNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayLeafNodes();
        }

        private void displayOrphanNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayOrphanNodes();
        }

        private void displayIntermediaryNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayIntermediaryNodes();
        }

        private void displayNodesPredecessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayPredecessorNodes(GetSelectedNodeIdentifier());
        }

        private void displayNodesSuccessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplaySuccessorNodes(GetSelectedNodeIdentifier());
        }

        private void displayNodesNeighboursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizationController.DisplayNeighbourNodes(GetSelectedNodeIdentifier());
        }
    }
}
