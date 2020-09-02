using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Xml.Linq;
using ReframeAnalyzer.Graph;
using VisualizerDGML.Utilities;
using ReframeVisualizer;

namespace VisualizerDGML.Graphs
{
    public class VisualGraphDGML : IVisualGraph
    {
        public VisualizationOptions Options { get; protected set; } = new VisualizationOptions();
        protected IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }

        public VisualGraphDGML(IEnumerable<IAnalysisNode> analysisNodes)
        {
            AnalysisNodes = analysisNodes;
        }

        protected virtual void AddCustomProperties(Graph graph)
        {
            graph.DocumentSchema.Properties.AddNewProperty("Name", Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Degree", Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("InDegree", Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("OutDegree", Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Tag", Type.GetType("System.String"));
        }

        protected virtual void AddNodesToGraph(Graph graph)
        {

        }

        protected virtual void AddDependenciesToGraph(Graph graph)
        {
            GraphNode predecessor;
            GraphNode successor;
            foreach (var analysisNode in AnalysisNodes)
            {
                predecessor = graph.Nodes.Get(analysisNode.Identifier.ToString());
                foreach (var analysisSuccessor in analysisNode.Successors)
                {
                    successor = graph.Nodes.Get(analysisSuccessor.Identifier.ToString());
                    if (successor != null)
                    {
                        GraphLink dependency = graph.Links.GetOrCreate(predecessor, successor);
                    }
                }
            }
        }

        private void PaintSelectedNode(Graph graph)
        {
            GraphNode initialNode = graph.Nodes.FirstOrDefault(n => n.GetValue("Tag") != null && n.GetValue("Tag").ToString() == "SelectedNode");
            if (initialNode != null)
            {
                var painter = new GraphPainter();
                painter.Paint(graph, initialNode, "#FF339933");
            }
        }

        protected virtual void PaintGraph(Graph graph)
        {
            PaintSelectedNode(graph);
        }

        public Graph GetGraph()
        {
            Graph graph = new Graph();

            AddCustomProperties(graph);
            AddNodesToGraph(graph);
            AddDependenciesToGraph(graph);
            PaintGraph(graph);

            return graph;
        }

        public string SerializeGraph()
        {
            return GetGraph().ToXml();
        }
    }
}
