using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
using ReframeTools.Commands;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class AnalysisController
    {
        internal List<GuiCommand> GeneralAnalyses { get; set; } = new List<GuiCommand>();
        internal List<GuiCommand> CustomAnalyses { get; set; } = new List<GuiCommand>();

        private FrmAnalysisFilter FilterForm { get; set; }
        private FrmAnalysisView View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        private Analyzer Analyzer { get; set; } = new Analyzer();

        public AnalysisController(FrmAnalysisView form, AnalysisGraphFactory graphFactory, FrmAnalysisFilter frmFilter = null)
        {
            View = form;
            FilterForm = frmFilter;

            CreateGeneralCommands();
            CreateAnalysisGraph(View.ReactorIdentifier, graphFactory); 
        }

        private void CreateAnalysisGraph(string reactorIdentifier, AnalysisGraphFactory graphFactory)
        {
            var pipeClient = new ReframePipeClient();
            string xmlSource = pipeClient.GetReactor(reactorIdentifier);
            AnalysisGraph = graphFactory.CreateGraph(xmlSource);
        }

        private void DisplayGraph(List<IAnalysisNode> originalNodes)
        {
            try
            {
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void ShowGraph(IEnumerable<IAnalysisNode> nodes)
        {
            if (View != null)
            {
                View.ShowAnalysis(nodes);

                SetGraphDetails();
                View.DisplayDetails();
            }
        }

        private void SetGraphDetails()
        {
            View.GraphIdentifier = AnalysisGraph.Identifier;
            View.GraphTotalNodeCount = GraphMetrics.GetNumberOfNodes(AnalysisGraph.Nodes).ToString();
            View.NumberOfAnalyzedNodes = GraphMetrics.GetNumberOfNodes(AnalysisNodes).ToString();
            View.NumberOfDependencies = GraphMetrics.GetNumberOfEdges(AnalysisNodes).ToString();
            View.MaxNumberOfDependencies = GraphMetrics.GetMaximumNumberOfEdges(AnalysisNodes).ToString();
            View.GraphDensity = GraphMetrics.GetGraphDensity(AnalysisNodes).ToString("N4");
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(List<IAnalysisNode> originalNodes)
        {
            IEnumerable<IAnalysisNode> filteredNodes;
            if (FilterForm != null)
            {
                FilterForm.OriginalNodes = originalNodes;
                FilterForm.ShowDialog();

                var filter = FilterForm.Filter;
                filteredNodes = filter.Apply();
            }
            else
            {
                filteredNodes = originalNodes;
            }

            return filteredNodes;
        }

        private void CreateGeneralCommands()
        {
            var showGraphCommand = new GuiCommand("showGraphCommand", "Show graph...", this, nameof(ShowEntireGraph));

            var showSourceNodesCommand = new GuiCommand("showSourceNodesCommand", "Show source nodes...", this, nameof(ShowSourceNodes));

            var showSinkNodesCommand = new GuiCommand("showSinkNodesCommand", "Show sink nodes...", this, nameof(ShowSinkNodes));

            var showLeafNodesCommand = new GuiCommand("showLeafNodesCommand", "Show leaf nodes...", this, nameof(ShowLeafNodes));

            var showOrphanNodesCommand = new GuiCommand("showOrphanNodesCommand", "Show orphan nodes...", this, nameof(ShowOrphanNodes));

            var showIntermediaryNodesCommand = new GuiCommand("showIntermediaryNodesCommand", "Show intermediary nodes...", this, nameof(ShowIntermediaryNodes));

            GeneralAnalyses.Add(showGraphCommand);
            GeneralAnalyses.Add(showSourceNodesCommand);
            GeneralAnalyses.Add(showSinkNodesCommand);
            GeneralAnalyses.Add(showLeafNodesCommand);
            GeneralAnalyses.Add(showOrphanNodesCommand);
            GeneralAnalyses.Add(showIntermediaryNodesCommand);
        }

        private void ShowEntireGraph()
        {
            DisplayGraph(AnalysisGraph.Nodes);
        }

        private void ShowSourceNodes()
        {
            DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph));
        }

        private void ShowSinkNodes()
        {
            DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph));
        }

        private void ShowLeafNodes()
        {
            DisplayGraph(Analyzer.GetLeafNodes(AnalysisGraph));
        }

        private void ShowOrphanNodes()
        {
            DisplayGraph(Analyzer.GetOrphanNodes(AnalysisGraph));
        }

        private void ShowIntermediaryNodes()
        {
            DisplayGraph(Analyzer.GetIntermediaryNodes(AnalysisGraph));
        }

        public void ShowPredecessorNodes(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> predecessorNodes = Analyzer.GetNodeAndItsPredecessors(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(predecessorNodes);
            }
        }

        private int GetMaxDepth()
        {
            var depthForm = new FrmMaxDepthLevel();
            depthForm.ShowDialog();

            return depthForm.MaxDepthLevel;
        }

        public void ShowSuccessorNodes(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> successorNodes = Analyzer.GetNodeAndItsSuccessors(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(successorNodes);
            }
        }

        public void ShowNeighbourNodes(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> neighbourNodes = Analyzer.GetNodeAndItsNeighbours(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(neighbourNodes);
            }
        }

        public void ShowSourceNodesForNode(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowSinkNodesForNode(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowLeafNodesForNode(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetLeafNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowIntermediaryPredecessors(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediaryPredecessors(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowIntermediarySuccessors(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediarySuccessors(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowIntermediaryNodesForNode(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediaryNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        private void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }
    }
}
