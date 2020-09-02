using EnvDTE;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using ReframeVisualizer.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class VisualizationController
    {
        private string _reactorIdentifier;
        private IVisualGraphFactory _visualGraphFactory;

        public VisualizationController(string reactorIdentifier, IVisualGraphFactory factory)
        {
            _visualGraphFactory = factory;
            _reactorIdentifier = reactorIdentifier;
        }

        protected virtual void ShowGraph(IVisualGraph visualGraph, string analysisDescription = "")
        {
            var serializedGraph = visualGraph.SerializeGraph();

            string fileName = new Random().Next().ToString() + "_" + _reactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, serializedGraph);
        }

        public void Visualize(IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null || analysisNodes.Count() == 0)
            {
                MessageBox.Show($"No nodes available for visualization!");
                return;
            }

            try
            {
                var visualGraph = _visualGraphFactory.CreateGraph(analysisNodes);

                var formOptions = new FrmVisualizationOptions(visualGraph);
                formOptions.ShowDialog();

                ShowGraph(visualGraph, "");
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
