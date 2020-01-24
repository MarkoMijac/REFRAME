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
        protected FrmOptions Options { get; set; }
        protected FrmAnalysisView View { get; set; }
        protected IAnalysisGraph AnalysisGraph { get; set; }
        protected Analyzer Analyzer { get; set; } = new Analyzer();
        protected AnalysisGraphFactory GraphFactory { get; set; } = new AnalysisGraphFactory();

        public AnalysisController(FrmAnalysisView form, FrmOptions frmOptions = null)
        {
            View = form;
            Options = frmOptions;
        }

        protected void CreateAnalysisGraph(string reactorIdentifier, AnalysisLevel analysisLevel)
        {
            string xmlSource = ClientQueries.GetReactor(reactorIdentifier);
            AnalysisGraph = GraphFactory.CreateGraph(xmlSource, analysisLevel);
        }

        protected virtual void ShowGraph(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> nodes, string analysisDescription)
        {
            if (View != null)
            {
                View.ShowAnalysis(nodes);
            }
        }

        public virtual void ShowEntireGraph(string description = "")
        {
            try
            {
                ShowGraph(AnalysisGraph, AnalysisGraph.Nodes, description);
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
                var sourceNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, sourceNodes, description);
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
                var sinkNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, sinkNodes, description);
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
                var leafNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, leafNodes, description);
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
                var orphanNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, orphanNodes, description);
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
                var intermediary = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, intermediary, description);
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
                    var predecessors = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, predecessors, description);
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
                    var successors = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, successors, description);
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
                    var neighbours = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, neighbours, description);
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
