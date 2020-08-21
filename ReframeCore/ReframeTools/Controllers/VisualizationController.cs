using EnvDTE;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class VisualizationController
    {
        private string _reactorIdentifier;

        public VisualizationController(string reactorIdentifier)
        {
            _reactorIdentifier = reactorIdentifier;
        }

        protected virtual void ShowGraph(IVisualGraph visualGraph, string analysisDescription = "")
        {
            var dgmlGraph = visualGraph.GetDGML();

            string fileName = new Random().Next().ToString() + "_" + _reactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
        }

        public void Visualize(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null || analysisNodes.Count() == 0)
            {
                MessageBox.Show($"No nodes available for visualization!");
                return;
            }

            try
            {
                var factory = new VisualGraphFactory();
                var visualGraph = factory.CreateGraph(analysisNodes, analysisGraph.AnalysisLevel);

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
