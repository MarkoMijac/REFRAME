using ReframeAnalyzer.Nodes;
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
        private IVisualGraphFactory _visualGraphFactory;
        private IGraphFileCreator _fileCreator;

        public VisualizationController(string reactorIdentifier, IVisualGraphFactory factory, IGraphFileCreator fileCreator)
        {
            _visualGraphFactory = factory;
            _reactorIdentifier = reactorIdentifier;
            _fileCreator = fileCreator;
        }

        private void ShowGraph(IVisualGraph visualGraph)
        {
            _fileCreator.CreateNewFile(visualGraph);
        }

        public void Visualize(List<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null || analysisNodes.Count() == 0)
            {
                MessageBox.Show($"No nodes available for visualization!");
                return;
            }

            try
            {
                var visualGraph = _visualGraphFactory.CreateGraph(_reactorIdentifier, analysisNodes);

                var formOptions = new FrmVisualizationOptions(visualGraph);
                formOptions.ShowDialog();

                ShowGraph(visualGraph);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }
    }
}
