﻿using EnvDTE;
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
    public class VisualizationController
    {
        protected FrmAnalysisView View { get; set; }

        public VisualizationController(FrmAnalysisView view)
        {
            View = view;
        }

        protected void CreateAnalysisGraph(string reactorIdentifier, AnalysisLevel analysisLevel)
        {
        }

        protected virtual void ShowGraph(IVisualGraph visualGraph, string analysisDescription = "")
        {
            var dgmlGraph = visualGraph.GetDGML();

            string fileName = new Random().Next().ToString() + "_" + View.ReactorIdentifier;
            ProjectItem p = SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
        }

        public void Visualize(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> analysisNodes)
        {
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
