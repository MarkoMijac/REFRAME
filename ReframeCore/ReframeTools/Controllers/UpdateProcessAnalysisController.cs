using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class UpdateProcessAnalysisController
    {
        protected FrmUpdateFilter FilterView { get; set; }
        protected FrmUpdateProcessInfo View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }

        public UpdateProcessAnalysisController(FrmUpdateProcessInfo form, FrmUpdateFilter frmFilter = null)
        {
            View = form;
            FilterView = frmFilter;
            CreateAnalysisGraph(View.ReactorIdentifier);
        }

        private void CreateAnalysisGraph(string reactorIdentifier)
        {
            var pipeClient = new ReframePipeClient();
            string xmlUpdateInfo = pipeClient.GetUpdateInfo(reactorIdentifier);
            string xmlSource = pipeClient.GetReactor(reactorIdentifier);

            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(xmlSource);
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            AnalysisGraph = factory.CreateGraph(xmlUpdateInfo);
            AnalysisNodes = AnalysisGraph.Nodes;
        }

        private void ShowGraph()
        {
            ShowBasicGraphInformation();
            ShowUpdateGraphInformation();
            ShowUpdatedNodes();
            PaintInitialNode();
            PaintValueDifferences();
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(List<IAnalysisNode> originalNodes)
        {
            IEnumerable<IAnalysisNode> filteredNodes;
            if (FilterView != null)
            {
                FilterView.OriginalNodes = originalNodes;
                FilterView.ShowDialog();

                var filter = FilterView.Filter;
                filteredNodes = filter.Apply();
            }
            else
            {
                filteredNodes = originalNodes;
            }

            return filteredNodes;
        }

        public void ShowUpdateAnalysis()
        {
            try
            {
                var originalNodes = AnalysisGraph.Nodes;
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph();
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        protected void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }

        private void ShowBasicGraphInformation()
        {
            View.txtGraphIdentifier.Text = AnalysisGraph.Identifier;
            View.txtUpdatedNodesCount.Text = AnalysisGraph.Nodes.Count.ToString();
            View.txtDisplayedNodesCount.Text = AnalysisNodes.Count().ToString();
        }

        private void ShowUpdateGraphInformation()
        {
            IUpdateGraph updateGraph = AnalysisGraph as IUpdateGraph;
            View.txtGraphTotalNodeCount.Text = updateGraph.TotalNodeCount.ToString();
            View.txtUpdateSuccessful.Text = updateGraph.UpdateSuccessful.ToString();
            View.txtUpdateStartedAt.Text = updateGraph.UpdateStartedAt.ToString();
            View.txtUpdateEndedAt.Text = updateGraph.UpdateEndedAt.ToString();
            View.txtUpdateDuration.Text = updateGraph.UpdateDuration.ToString();

            View.txtUpdateCause.Text = updateGraph.CauseMessage;
            View.txtInitialNodeIdentifier.Text = updateGraph.InitialNodeIdentifier.ToString();
            View.txtInitialNodeMemberName.Text = updateGraph.InitialNodeName;
            View.txtInitialNodeOwnerObject.Text = updateGraph.InitialNodeOwner;
            View.txtInitialNodeCurrentValue.Text = updateGraph.InitialNodeCurrentValue;
            View.txtInitialNodePreviousValue.Text = updateGraph.InitialNodePreviousValue;
        }

        private void ShowUpdatedNodes()
        {
            View.dgvUpdateInfo.Rows.Clear();

            try
            {
                if (AnalysisGraph != null)
                {
                    foreach (IUpdateNode node in AnalysisNodes)
                    {
                        View.dgvUpdateInfo.Rows.Add(new string[]
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

        private void PaintValueDifferences()
        {
            for (int i = 0; i < View.dgvUpdateInfo.Rows.Count; i++)
            {
                if (View.dgvUpdateInfo.Rows[i].Cells["colCurrentValue"].Value.ToString() != View.dgvUpdateInfo.Rows[i].Cells["colPreviousValue"].Value.ToString())
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
            View.dgvUpdateInfo.Rows[i].Cells["colCurrentValue"].Style.BackColor = color;
            View.dgvUpdateInfo.Rows[i].Cells["colPreviousValue"].Style.BackColor = color;
        }

        private void PaintInitialNode()
        {
            IAnalysisNode initialNode = AnalysisNodes.FirstOrDefault(n => (n as IUpdateNode).IsInitialNode == true);
            if (initialNode != null)
            {
                for (int i = 0; i < View.dgvUpdateInfo.Rows.Count; i++)
                {
                    if (View.dgvUpdateInfo.Rows[i].Cells["colIdentifier"].Value.ToString() == initialNode.Identifier.ToString())
                    {
                        View.dgvUpdateInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                        break;
                    }
                }
            }

            View.dgvUpdateInfo.ClearSelection();
        }
    }
}
