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
        public  AnalysisController AnalysisController { get; set; }
        protected VisualizationController VisualizationController { get; set; }

        public string GraphIdentifier { get; set; }
        public string GraphTotalNodeCount { get; set; }
        public string NumberOfAnalyzedNodes { get; set; }
        public string NumberOfDependencies { get; set; }
        public string MaxNumberOfDependencies { get; set; }
        public string GraphDensity { get; set; }

        public FrmAnalysisView(string reactorIdentifier) : this()
        {
            ReactorIdentifier = reactorIdentifier;
            AnalysisController = CreateAnalysisController();
            VisualizationController = new VisualizationController(ReactorIdentifier);
            AddColumns();
        }

        private void FrmObjectMemberAnalysis_Load(object sender, EventArgs e)
        {
            bool notEmtpy = dgvNodes.SelectedRows.Count > 0;
            generalNodeAnalysisToolStripMenuItem.Enabled = notEmtpy;
            btnVisualize.Enabled = notEmtpy;
        }

        public virtual void ShowAnalysis(IEnumerable<IAnalysisNode> nodes)
        {
            dgvNodes.Rows.Clear();
            bool notEmtpy = nodes.Count() > 0;
            generalNodeAnalysisToolStripMenuItem.Enabled = notEmtpy;
            btnVisualize.Enabled = notEmtpy;
        }

        public virtual void DisplayDetails()
        {
            txtGraphIdentifier.Text = GraphIdentifier;
            txtGraphTotalNodeCount.Text = GraphTotalNodeCount;
            txtNumberOfAnalyzedNodes.Text = NumberOfAnalyzedNodes;
            txtNumberOfDependencies.Text = NumberOfDependencies;
            txtMaxNumOfDependencies.Text = MaxNumberOfDependencies;
            txtGraphDensity.Text = GraphDensity;
        }

        protected virtual void AddColumns()
        {
            
        }

        protected virtual AnalysisController CreateAnalysisController()
        {
            return null;
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

        private uint GetSelectedNodeId()
        {
            uint id = 0;

            if (dgvNodes.Rows.Count > 0)
            {
                uint.TryParse(dgvNodes.CurrentRow.Cells[0].Value.ToString(), out id);
            }

            return id;
        }

        private void showNodePredecessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowPredecessorNodes(GetSelectedNodeId());
        }

        private void showSuccessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSuccessorNodes(GetSelectedNodeId());
        }

        private void showNodesNeighboursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowNeighbourNodes(GetSelectedNodeId());
        }

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            VisualizationController.Visualize(AnalysisController.AnalysisGraph, AnalysisController.AnalysisNodes);
        }

        private void showSinkNodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSinkNodes(GetSelectedNodeId());
        }

        private void showLeafNodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowLeafNodes(GetSelectedNodeId());
        }

        private void showSourceNodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowSourceNodes(GetSelectedNodeId());
        }

        private void showIntermediaryPredecessorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowIntermediaryPredecessors(GetSelectedNodeId());
        }

        private void showIntermediarySuccessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowIntermediarySuccessors(GetSelectedNodeId());
        }

        private void showIntermediaryNodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalysisController.ShowIntermediaryNodes(GetSelectedNodeId());
        }
    }
}
