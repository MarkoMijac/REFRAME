using EnvDTE;
using IPCClient;
using Microsoft.VisualStudio.GraphModel;
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
    public class ClassVisualizationController
    {
        private IAnalysisGraph AnalysisGraph { get; set; }
        private VisualGraphFactory VisualGraphFactory { get; set; } = new VisualGraphFactory();
        public Analyzer Analyzer { get; set; } = new Analyzer();

        protected FrmAnalysis _form;

        public ClassVisualizationController(FrmAnalysis form)
        {
            _form = form;
            string xmlSource = ClientQueries.GetReactor(_form.ReactorIdentifier);
            var analysisGraphFactory = new AnalysisGraphFactory();
            AnalysisGraph = analysisGraphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
        }

        protected void ShowGraph(IVisualGraph visualGraph, string analysisDescription = "")
        {
            var dgmlGraph = visualGraph.GetDGML();

            string fileName = new Random().Next().ToString() + "_" + _form.ReactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
            MessageBox.Show(analysisDescription);
        }

        public void ShowEntireGraph()
        {
            try
            {
                var visualGraph = VisualGraphFactory.CreateGraph(AnalysisGraph.Nodes, VisualizationLevel.ClassLevel);
                var dgmlGraph = visualGraph.GetDGML();

                string analysisDescription = $"Showing entire class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSourceNodes()
        {
            try
            {
                var sourceNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(sourceNodes, VisualizationLevel.ClassLevel);
                string analysisDescription = $"Showing only source nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
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
                var sinkNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(sinkNodes, VisualizationLevel.ClassLevel);
                string analysisDescription = $"Showing only sink nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
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
                var leafNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(leafNodes, VisualizationLevel.ClassLevel);
                string analysisDescription = $"Showing only leaf nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
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
                var orphanNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(orphanNodes, VisualizationLevel.ClassLevel);
                string analysisDescription = $"Showing only orphan nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
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
                var intermediaryNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                var visualGraph = VisualGraphFactory.CreateGraph(intermediaryNodes, VisualizationLevel.ClassLevel);
                string analysisDescription = $"Showing only intermediary nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(visualGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowPredecessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    var predecessors = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(predecessors, VisualizationLevel.ClassLevel);

                    string analysisDescription = $"Showing only predecessor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(visualGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSuccessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    var successors = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(successors, VisualizationLevel.ClassLevel);

                    string analysisDescription = $"Showing only successors nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(visualGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowNeighbourNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    var neighbours = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    var visualGraph = VisualGraphFactory.CreateGraph(neighbours, VisualizationLevel.ClassLevel);

                    string analysisDescription = $"Showing only neighbour nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(visualGraph, analysisDescription);
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
