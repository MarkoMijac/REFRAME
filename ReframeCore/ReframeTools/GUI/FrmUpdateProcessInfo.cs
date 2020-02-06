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
        private UpdateAnalysisGraph _analysisGraph;
        public FrmUpdateProcessInfo(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
            AddColumns();
        }

        public void ShowAnalysis(IAnalysisGraph analysisGraph)
        {
            _analysisGraph = analysisGraph as UpdateAnalysisGraph;
            ShowBasicInformation();
            ShowUpdatedNodes();
            PaintInitialNode(analysisGraph);
        }

        private void PaintInitialNode(IAnalysisGraph analysisGraph)
        {
            IAnalysisNode initialNode = analysisGraph.Nodes.FirstOrDefault(n => (n as UpdateAnalysisNode).IsInitialNode == true);
            if (initialNode != null)
            {
                for (int i = 0; i < dgvUpdateInfo.Rows.Count; i++)
                {
                    if (dgvUpdateInfo.Rows[i].Cells["colIdentifier"].Value.ToString() == initialNode.Identifier.ToString())
                    {
                        dgvUpdateInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                        break;
                    }
                }
            }

            dgvUpdateInfo.ClearSelection();
        }

        private void ShowBasicInformation()
        {
            txtGraphIdentifier.Text = _analysisGraph.Identifier;
            txtGraphTotalNodeCount.Text = _analysisGraph.TotalNodeCount.ToString();
            txtUpdatedNodesCount.Text = _analysisGraph.Nodes.Count.ToString();
            txtUpdateSuccessful.Text = _analysisGraph.UpdateSuccessful.ToString();
            txtUpdateStartedAt.Text = _analysisGraph.UpdateStartedAt.ToString();
            txtUpdateEndedAt.Text = _analysisGraph.UpdateEndedAt.ToString();
            txtUpdateDuration.Text = _analysisGraph.UpdateDuration.ToString();

            txtUpdateCause.Text = _analysisGraph.CauseMessage;
            txtInitialNodeIdentifier.Text = _analysisGraph.InitialNodeIdentifier.ToString();
            txtInitialNodeMemberName.Text = _analysisGraph.InitialNodeName;
            txtInitialNodeOwnerObject.Text = _analysisGraph.InitialNodeOwner;
            txtInitialNodeCurrentValue.Text = _analysisGraph.InitialNodeCurrentValue;
            txtInitialNodePreviousValue.Text = _analysisGraph.InitialNodePreviousValue;
        }

        private void ShowUpdatedNodes()
        {
            dgvUpdateInfo.Rows.Clear();

            try
            {
                if (_analysisGraph != null)
                {
                    foreach (UpdateAnalysisNode node in _analysisGraph.Nodes)
                    {
                        dgvUpdateInfo.Rows.Add(new string[]
                            {
                                node.Identifier.ToString(),
                                node.Name,
                                node.NodeType.ToString(),
                                node.CurrentValue,
                                node.PreviousValue,
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
                dgvUpdateInfo.Columns.Add("colPreviousValue", "Previous Value");
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

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            VisualizationController visualizationController = new VisualizationController(ReactorIdentifier);
            visualizationController.Visualize(_analysisGraph, _analysisGraph.Nodes);
        }
    }
}
