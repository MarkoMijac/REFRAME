﻿using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerDGML.Graphs
{
    public class AssemblyVisualGraphDGML : VisualGraphDGML
    {
        public AssemblyVisualGraphDGML(string reactorIdentifier, List<IAnalysisNode> analysisNodes) : base(reactorIdentifier, analysisNodes)
        {

        }

        protected override void AddNodesToGraph(Graph graph)
        {
            AddNodes(graph);
        }

        private void AddNodes(Graph graph)
        {
            foreach (var node in AnalysisNodes)
            {
                GraphNode g = graph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);
            }
        }
    }
}
