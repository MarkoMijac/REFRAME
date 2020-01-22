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
        protected FrmAnalysis Form { get; set; }
        protected IAnalysisGraph AnalysisGraph { get; set; }
        protected VisualGraphFactory VisualGraphFactory { get; set; } = new VisualGraphFactory();
        protected Analyzer Analyzer { get; set; } = new Analyzer();

        public VisualizationController(FrmAnalysis form)
        {
            Form = form;
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

            string fileName = new Random().Next().ToString() + "_" + Form.ReactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
            MessageBox.Show(analysisDescription);
        }

        public void ShowEntireGraph(string description = "")
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

        public void ShowSourceNodes(string description = "")
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

        public void ShowSinkNodes(string description = "")
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

        public void ShowLeafNodes(string description = "")
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

        public void ShowOrphanNodes(string description = "")
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

        public void ShowIntermediaryNodes(string description = "")
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

        public void ShowPredecessorNodes(string description = "")
        {
            try
            {
                string nodeIdentifier = Form.GetSelectedNodeIdentifier();
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

        public void ShowSuccessorNodes(string description = "")
        {
            try
            {
                string nodeIdentifier = Form.GetSelectedNodeIdentifier();
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

        public void ShowNeighbourNodes(string description = "")
        {
            try
            {
                string nodeIdentifier = Form.GetSelectedNodeIdentifier();
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
