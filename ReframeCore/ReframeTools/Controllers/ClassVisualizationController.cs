using EnvDTE;
using IPCClient;
using Microsoft.VisualStudio.GraphModel;
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
    public class ClassVisualizationController : GUIController
    {
        public ClassVisualizationController(FrmAnalysis form)
            :base(form)
        {
           
        }

        protected override void ShowGraph(string xmlGraph, string analysisDescription = "")
        {
            ClassVisualGraph visualGraph = new ClassVisualGraph(xmlGraph);
            Visualizer visualizer = new Visualizer(visualGraph);
            Graph dgmlGraph = visualizer.GenerateDGMLGraph();

            string fileName = new Random().Next().ToString() + "_" + _form.ReactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
            MessageBox.Show(analysisDescription);
        }

        public override void ShowEntireGraph()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraph(_form.ReactorIdentifier);
                string analysisDescription = $"Showing entire class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSourceNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphSourceNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only source nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSinkNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphSinkNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only sink nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowLeafNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphLeafNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only leaf nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowOrphanNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphOrphanNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only orphan nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowIntermediaryNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphIntermediaryNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only intermediary nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowPredecessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphPredecessorNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only predecessor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSuccessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphSuccessorNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only successor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowNeighbourNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphNeighbourNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only neighbour nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        internal void GeneratePredecessorsGraph(string reactorIdentifier, string nodeIdentifier)
        {
            string xmlGraph = ClientQueries.GetClassAnalysisGraphPredecessorNodes(reactorIdentifier, nodeIdentifier);
            ClassVisualGraph visualGraph = new ClassVisualGraph(xmlGraph);
            Visualizer visualizer = new Visualizer(visualGraph);
            Graph dgmlGraph = visualizer.GenerateDGMLGraph();
            ProjectItem p = SolutionServices.CreateNewDgmlFile(nodeIdentifier, dgmlGraph);
        }
    }
}
