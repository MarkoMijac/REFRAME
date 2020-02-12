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
        private IAnalysisGraph _analysisGraph;
        private IEnumerable<IAnalysisNode> _analysisNodes;
        public FrmUpdateProcessInfo(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
            AddColumns();
            rbtnAllNodes.Checked = true;
        }

        public void ShowAnalysis(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> analysisNodes)
        {
            _analysisGraph = analysisGraph;
            _analysisNodes = analysisNodes;
            ShowBasicGraphInformation();
            ShowUpdateGraphInformation();
            ShowUpdatedNodes();
            PaintInitialNode();
            PaintValueDifferences();
        }

        private void PaintValueDifferences()
        {
            for (int i = 0; i < dgvUpdateInfo.Rows.Count; i++)
            {
                if (dgvUpdateInfo.Rows[i].Cells["colCurrentValue"].Value.ToString() != dgvUpdateInfo.Rows[i].Cells["colPreviousValue"].Value.ToString())
                {
                    PaintValueCells(i, Color.DarkRed);
                }
                else
                {
                    PaintValueCells(i, Color.DarkGreen);
                }
            }
        }

        private void PaintValueCells(int i, Color color)
        {
            dgvUpdateInfo.Rows[i].Cells["colCurrentValue"].Style.BackColor = color;
            dgvUpdateInfo.Rows[i].Cells["colPreviousValue"].Style.BackColor = color;
        }

        private void PaintInitialNode()
        {
            IAnalysisNode initialNode = _analysisNodes.FirstOrDefault(n => (n as IUpdateNode).IsInitialNode == true);
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

        private void ShowBasicGraphInformation()
        {
            txtGraphIdentifier.Text = _analysisGraph.Identifier;
            txtUpdatedNodesCount.Text = _analysisGraph.Nodes.Count.ToString();
            txtDisplayedNodesCount.Text = _analysisNodes.Count().ToString();
        }

        private void ShowUpdateGraphInformation()
        {
            IUpdateGraph updateGraph = _analysisGraph as IUpdateGraph;
            txtGraphTotalNodeCount.Text = updateGraph.TotalNodeCount.ToString();
            txtUpdateSuccessful.Text = updateGraph.UpdateSuccessful.ToString();
            txtUpdateStartedAt.Text = updateGraph.UpdateStartedAt.ToString();
            txtUpdateEndedAt.Text = updateGraph.UpdateEndedAt.ToString();
            txtUpdateDuration.Text = updateGraph.UpdateDuration.ToString();

            txtUpdateCause.Text = updateGraph.CauseMessage;
            txtInitialNodeIdentifier.Text = updateGraph.InitialNodeIdentifier.ToString();
            txtInitialNodeMemberName.Text = updateGraph.InitialNodeName;
            txtInitialNodeOwnerObject.Text = updateGraph.InitialNodeOwner;
            txtInitialNodeCurrentValue.Text = updateGraph.InitialNodeCurrentValue;
            txtInitialNodePreviousValue.Text = updateGraph.InitialNodePreviousValue;
        }

        private void ShowUpdatedNodes()
        {
            dgvUpdateInfo.Rows.Clear();

            try
            {
                if (_analysisGraph != null)
                {
                    foreach (IUpdateNode node in _analysisNodes)
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
            UpdateProcessAnalysisController controller = new UpdateProcessAnalysisController(this, new FrmFilterUpdateAnalysis());
            controller.ShowUpdateAnalysis();
        }

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            VisualizationController visualizationController = new VisualizationController(ReactorIdentifier);
            visualizationController.Visualize(_analysisGraph, _analysisNodes);
        }

        private void ShowOnlyRowsWithNoDifferences()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                if (ValueChanged(row) == false)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void ShowOnlyRowsWithDifferences()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                if (ValueChanged(row) == true)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void ShowAllRows()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                row.Visible = true;
            }
        }

        private bool ValueChanged(DataGridViewRow row)
        {
            bool changed = true;

            if (row.Cells["colCurrentValue"].Value.ToString() == row.Cells["colPreviousValue"].Value.ToString())
            {
                changed = false;
            }

            return changed;
        }

        private void rbtnAllNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAllNodes.Checked == true)
            {
                ShowAllRows();
            }
        }

        private void rbtnOnlyNodesWithDifferences_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOnlyNodesWithDifferences.Checked == true)
            {
                ShowOnlyRowsWithDifferences();
            }
        }

        private void rbtnOnlyNodesWithNoDifferences_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOnlyNodesWithNoDifferences.Checked == true)
            {
                ShowOnlyRowsWithNoDifferences();
            }
        }
    }
}
