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

        public void ShowEntireGraph()
        {
            DisplayGraph(AnalysisGraph.Nodes);
        }

        public void ShowSourceNodes()
        {
            DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph));
        }

        public void ShowSinkNodes()
        {
            DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph));
        }

        public void ShowLeafNodes()
        {
            DisplayGraph(Analyzer.GetLeafNodes(AnalysisGraph));
        }

        public void ShowOrphanNodes()
        {
            DisplayGraph(Analyzer.GetOrphanNodes(AnalysisGraph));
        }

        public void ShowIntermediaryNodes()
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

        public void ShowSourceNodes(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowSinkNodes(uint nodeIdentifier)
        {
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        public void ShowLeafNodes(uint nodeIdentifier)
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

        public void ShowIntermediaryNodes(uint nodeIdentifier)
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
