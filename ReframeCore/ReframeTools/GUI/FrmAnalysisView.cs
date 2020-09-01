using ReframeAnalyzer.Graph;
using ReframeTools.Commands;
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

        public uint SelectedNodeId { get { return GetSelectedNodeId(); } }

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

            FillGeneralAnalyses();
            FillCustomAnalyses();

        }

        private void FillMenuItemOptions(ToolStripMenuItem menu, IEnumerable<GuiCommand> commands)
        {
            menu.DropDownItems.Clear();

            foreach (var command in commands)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(command.Text, null, command.Handler);
                menu.DropDownItems.Add(item);
            }
        }

        private void FillGeneralAnalyses()
        {
            FillMenuItemOptions(generalAnalysesToolStripMenuItem, AnalysisController.GetGeneralGraphAnalysesCommands());
            FillMenuItemOptions(generalNodeAnalysisToolStripMenuItem, AnalysisController.GetGeneralNodeAnalysesCommands());
        }

        private void FillCustomAnalyses()
        {
            var customAnalyses = AnalysisController.GetCustomAnalysesCommands();
            if (customAnalyses.Count > 0)
            {
                FillMenuItemOptions(customAnalysesToolStripMenuItem, customAnalyses);
                customAnalysesToolStripMenuItem.Visible = true;
            }
            else
            {
                customAnalysesToolStripMenuItem.Visible = false;
            }
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

        private uint GetSelectedNodeId()
        {
            uint id = 0;

            if (dgvNodes.Rows.Count > 0)
            {
                uint.TryParse(dgvNodes.CurrentRow.Cells[0].Value.ToString(), out id);
            }

            return id;
        }

        protected virtual void btnVisualize_Click(object sender, EventArgs e)
        {
            
        }
    }
}
