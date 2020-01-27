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
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
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
                AnalysisNodes = AnalysisGraph.Nodes;
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                AnalysisNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                AnalysisNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                AnalysisNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                AnalysisNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                AnalysisNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                    AnalysisNodes = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                    AnalysisNodes = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
                    AnalysisNodes = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    ShowGraph(AnalysisGraph, AnalysisNodes, description);
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
