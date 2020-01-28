using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public abstract class AnalysisController
    {
        protected FrmFilterAnalysis FilterView { get; set; }
        protected FrmAnalysisView View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        protected Analyzer Analyzer { get; set; } = new Analyzer();
        protected AnalysisGraphFactory GraphFactory { get; set; } = new AnalysisGraphFactory();

        public AnalysisController(FrmAnalysisView form, FrmFilterAnalysis frmFilter = null)
        {
            View = form;
            FilterView = frmFilter;
        }

        protected void CreateAnalysisGraph(string reactorIdentifier, AnalysisLevel analysisLevel)
        {
            string xmlSource = ClientQueries.GetReactor(reactorIdentifier);
            AnalysisGraph = GraphFactory.CreateGraph(xmlSource, analysisLevel);
        }

        protected virtual void ShowGraph(IEnumerable<IAnalysisNode> nodes, string analysisDescription)
        {
            if (View != null)
            {
                View.ShowAnalysis(nodes);
            }
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(IEnumerable<IAnalysisNode> originalNodes)
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

        public virtual void ShowEntireGraph(string description = "")
        {
            try
            {
                var originalNodes = AnalysisGraph.Nodes;
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public virtual void ShowSourceNodes(string description = "")
        {
            try
            {
                var originalNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSinkNodes(string description = "")
        {
            try
            {
                var originalNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowLeafNodes(string description = "")
        {
            try
            {
                var originalNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowOrphanNodes(string description = "")
        {
            try
            {
                var originalNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediaryNodes(string description = "")
        {
            try
            {
                var originalNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowPredecessorNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var originalNodes = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes, description);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSuccessorNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var originalNodes = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes, description);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowNeighbourNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var originalNodes = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes, description);
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
