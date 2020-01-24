using EnvDTE;
using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public abstract class VisualizationController
    {
        protected FrmAnalysisView View { get; set; }
        protected IAnalysisGraph AnalysisGraph { get; set; }
        protected VisualGraphFactory VisualGraphFactory { get; set; } = new VisualGraphFactory();
        protected Analyzer Analyzer { get; set; } = new Analyzer();

        public VisualizationController(FrmAnalysisView view)
        {
            View = view;
        }

        protected void CreateAnalysisGraph(string reactorIdentifier, AnalysisLevel analysisLevel)
        {
            string xmlSource = ClientQueries.GetReactor(reactorIdentifier);
            var analysisGraphFactory = new AnalysisGraphFactory();
            AnalysisGraph = analysisGraphFactory.CreateGraph(xmlSource, analysisLevel);
        }

        protected virtual void ShowGraph(IVisualGraph visualGraph, string analysisDescription = "")
        {
            var dgmlGraph = visualGraph.GetDGML();

            string fileName = new Random().Next().ToString() + "_" + View.ReactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
        }

        public void DisplayEntireGraph(string description = "")
        {
            try
            {
                var visualGraph = VisualGraphFactory.CreateGraph(AnalysisGraph.Nodes, AnalysisGraph.AnalysisLevel);
                var dgmlGraph = visualGraph.GetDGML();

                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplaySourceNodes(string description = "")
        {
            try
            {
                var sourceNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(sourceNodes, AnalysisGraph.AnalysisLevel);
                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplaySinkNodes(string description = "")
        {
            try
            {
                var sinkNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(sinkNodes, AnalysisGraph.AnalysisLevel);
                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplayLeafNodes(string description = "")
        {
            try
            {
                var leafNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(leafNodes, AnalysisGraph.AnalysisLevel);
                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplayOrphanNodes(string description = "")
        {
            try
            {
                var orphanNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(orphanNodes, AnalysisGraph.AnalysisLevel);
                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplayIntermediaryNodes(string description = "")
        {
            try
            {
                var intermediaryNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(intermediaryNodes, AnalysisGraph.AnalysisLevel);
                ShowGraph(visualGraph, description);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplayPredecessorNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var predecessors = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(predecessors, AnalysisGraph.AnalysisLevel);

                    ShowGraph(visualGraph, description);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplaySuccessorNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var successors = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(successors, AnalysisGraph.AnalysisLevel);

                    ShowGraph(visualGraph, description);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void DisplayNeighbourNodes(string nodeIdentifier, string description = "")
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    var neighbours = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(neighbours, AnalysisGraph.AnalysisLevel);

                    ShowGraph(visualGraph, description);
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
