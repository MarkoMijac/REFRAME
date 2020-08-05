using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
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
        private FrmAnalysisFilter FilterForm { get; set; }
        private FrmAnalysisView View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        private Analyzer Analyzer { get; set; } = new Analyzer();
        private AnalysisGraphFactory GraphFactory { get; set; }

        public AnalysisController(FrmAnalysisView form, AnalysisGraphFactory graphFactory, FrmAnalysisFilter frmFilter = null)
        {
            GraphFactory = graphFactory;
            View = form;
            FilterForm = frmFilter;

            CreateAnalysisGraph(View.ReactorIdentifier); 
        }

        protected void CreateAnalysisGraph(string reactorIdentifier)
        {
            var pipeClient = new ReframePipeClient();
            string xmlSource = pipeClient.GetReactor(reactorIdentifier);
            AnalysisGraph = GraphFactory.CreateGraph(xmlSource);
        }

        protected virtual void ShowGraph(IEnumerable<IAnalysisNode> nodes)
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

        private IEnumerable<IAnalysisNode> GetFilteredNodes(IEnumerable<IAnalysisNode> originalNodes)
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

        public virtual void ShowEntireGraph()
        {
            try
            {
                var originalNodes = AnalysisGraph.Nodes;
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public virtual void ShowSourceNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSinkNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowLeafNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowOrphanNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediaryNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowPredecessorNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes;

                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetNodeAndItsPredecessors(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetNodeAndItsPredecessors(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
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
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes;
                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetNodeAndItsSuccessors(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetNodeAndItsSuccessors(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowNeighbourNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes;
                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetNodeAndItsNeighbours(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetNodeAndItsNeighbours(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSourceNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetSourceNodes(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSinkNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetSinkNodes(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowLeafNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetLeafNodes(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediaryPredecessors(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetIntermediaryPredecessors(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediarySuccessors(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetIntermediarySuccessors(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediaryNodes(uint nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != 0)
                {
                    IEnumerable<IAnalysisNode> originalNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
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
    }
}
