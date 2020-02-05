using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;

namespace ReframeTools.GUI
{
    public partial class FrmUpdateProcessInfo : Form
    {
        public string ReactorIdentifier { get; set; }

        public FrmUpdateProcessInfo(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
            AddColumns();
        }

        public void ShowAnalysis(IAnalysisGraph analysisGraph)
        {
            ShowBasicInformation(analysisGraph as UpdateAnalysisGraph);
            ShowUpdatedNodes(analysisGraph as UpdateAnalysisGraph);
        }

        private void ShowBasicInformation(UpdateAnalysisGraph analysisGraph)
        {
            txtGraphIdentifier.Text = analysisGraph.Identifier;
            txtGraphTotalNodeCount.Text = analysisGraph.TotalNodeCount.ToString();
            txtUpdatedNodesCount.Text = analysisGraph.Nodes.Count.ToString();
            txtUpdateSuccessful.Text = analysisGraph.UpdateSuccessful.ToString();
            txtUpdateStartedAt.Text = analysisGraph.UpdateStartedAt.ToString();
            txtUpdateEndedAt.Text = analysisGraph.UpdateEndedAt.ToString();
            txtUpdateDuration.Text = analysisGraph.UpdateDuration.ToString();

            txtUpdateCause.Text = analysisGraph.CauseMessage;
            txtInitialNodeIdentifier.Text = analysisGraph.InitialNodeIdentifier.ToString();
            txtInitialNodeMemberName.Text = analysisGraph.InitialNodeName;
            txtInitialNodeOwnerObject.Text = analysisGraph.InitialNodeOwner;
        }

        private void ShowUpdatedNodes(UpdateAnalysisGraph analysisGraph)
        {
            dgvUpdateInfo.Rows.Clear();

            try
            {
                if (analysisGraph != null)
                {
                    foreach (UpdateAnalysisNode node in analysisGraph.Nodes)
                    {
                        dgvUpdateInfo.Rows.Add(new string[]
                            {
                                node.Identifier.ToString(),
                                node.Name,
                                node.NodeType.ToString(),
                                node.CurrentValue,
                                node.UpdateOrder.ToString(),
                                node.UpdateLayer.ToString(),
                                node.UpdateStartedAt,
                                node.UpdateCompletedAt,
                                node.UpdateDuration,
                                node.Degree.ToString(),
                                node.InDegree.ToString(),
                                node.OutDegree.ToString()
                            });
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddColumns()
        {
            if (dgvUpdateInfo.Columns.Count == 0)
            {
                dgvUpdateInfo.Columns.Add("colIdentifier", "Identifier");
                dgvUpdateInfo.Columns.Add("colName", "Name");
                dgvUpdateInfo.Columns.Add("colNodeType", "Node Type");
                dgvUpdateInfo.Columns.Add("colCurrentValue", "Current Value");
                dgvUpdateInfo.Columns.Add("colUpdateOrder", "Update Order");
                dgvUpdateInfo.Columns.Add("colUpdateLayer", "Update Layer");
                dgvUpdateInfo.Columns.Add("colStartedAt", "Started At");
                dgvUpdateInfo.Columns.Add("colCompletedAt", "Completed At");
                dgvUpdateInfo.Columns.Add("colDuration", "Duration");
                dgvUpdateInfo.Columns.Add("colDegree", "Degree");
                dgvUpdateInfo.Columns.Add("colInDegree", "In Degree");
                dgvUpdateInfo.Columns.Add("colOutDegree", "Out Degree");
            }
        }

        private void FrmUpdateProcessInfo_Load(object sender, EventArgs e)
        {
            UpdateProcessAnalysisController controller = new UpdateProcessAnalysisController(this);
            controller.ShowGraph();
        }
    }
}
