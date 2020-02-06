using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Xml.Linq;
using ReframeAnalyzer.Graph;

namespace ReframeVisualizer
{
    public abstract class VisualGraph : IVisualGraph
    {
        protected IEnumerable<IAnalysisNode> _analysisNodes;

        public VisualizationOptions VisualizationOptions { get; protected set; } = new VisualizationOptions();

        public VisualGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            _analysisNodes = analysisNodes;
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void AddCustomProperties(Graph dgmlGraph)
        {
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Degree", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("InDegree", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OutDegree", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Tag", System.Type.GetType("System.String"));
        }

        protected abstract void AddNodesToGraph(Graph dgmlGraph);
        protected abstract void AddDependenciesToGraph(Graph dgmlGraph);

        private void PaintSelectedNode(Graph dgmlGraph)
        {
            GraphNode initialNode = dgmlGraph.Nodes.FirstOrDefault(n => n.GetValue("Tag")!=null && n.GetValue("Tag").ToString() == "SelectedNode");
            if (initialNode != null)
            {
                var painter = new GraphPainter();
                painter.Paint(dgmlGraph, initialNode, "#FF339933");
            }
        }

        protected virtual void PaintGraph(Graph dgmlGraph)
        {
            PaintSelectedNode(dgmlGraph);
        }

        public Graph GetDGML()
        {
            Graph dgmlGraph = new Graph();

            AddCustomProperties(dgmlGraph);
            AddNodesToGraph(dgmlGraph);
            AddDependenciesToGraph(dgmlGraph);
            PaintGraph(dgmlGraph);

            return dgmlGraph;
        }
    }
}
