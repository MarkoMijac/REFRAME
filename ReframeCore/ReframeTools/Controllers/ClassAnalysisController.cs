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
    public class ClassAnalysisController
    {
        protected FrmAnalysis _form;

        private ClassLevelAnalyzer Analyzer { get; set; } = new ClassLevelAnalyzer();

        private IAnalysisGraph AnalysisGraph { get; set; }

        public ClassAnalysisController(FrmAnalysis form)
        {
            _form = form;
            string xmlSource = ClientQueries.GetReactor(_form.ReactorIdentifier);
            var graphFactory = new AnalysisGraphFactory();
            AnalysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
        }

        protected virtual void ShowGraph(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> nodes, string analysisDescription = "")
        {
            _form.ShowXMLSource(analysisGraph.Source);
            _form.ShowTable(nodes);
            _form.ShowDescription(analysisDescription);
        }

        protected void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }

        public void ShowEntireGraph()
        {
            try
            {
                string analysisDescription = $"Showing entire class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, AnalysisGraph.Nodes, analysisDescription);
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

                string analysisDescription = $"Showing only source nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, sourceNodes, analysisDescription);
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

                string analysisDescription = $"Showing only sink nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, sinkNodes, analysisDescription);
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

                string analysisDescription = $"Showing only leaf nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, leafNodes, analysisDescription);
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

                string analysisDescription = $"Showing only orphan nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, orphanNodes, analysisDescription);
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
                var intermediary = Analyzer.GetIntermediaryNodes(AnalysisGraph);

                string analysisDescription = $"Showing only intermediary nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(AnalysisGraph, intermediary, analysisDescription);
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

                    string analysisDescription = $"Showing only predecessor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(AnalysisGraph, predecessors, analysisDescription);
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

                    string analysisDescription = $"Showing only successor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(AnalysisGraph, successors, analysisDescription);
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

                    string analysisDescription = $"Showing only neighbour nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(AnalysisGraph, neighbours, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }
    }
}
