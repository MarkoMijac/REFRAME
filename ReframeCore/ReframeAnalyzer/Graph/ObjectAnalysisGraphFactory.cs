﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ObjectAnalysisGraphFactory : AnalysisGraphAbstractFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = factory.CreateGraph(XmlSource) as ObjectMemberAnalysisGraph;

            string identifier = objectMemberAnalysisGraph.Identifier;
            var graph = new ObjectAnalysisGraph(identifier, AnalysisLevel.ObjectLevel);

            InitializeGraph(graph, objectMemberAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(ObjectAnalysisGraph graph, ObjectMemberAnalysisGraph objectMemberAnalysisGraph)
        {
            if (graph != null && objectMemberAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, objectMemberAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, objectMemberAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(ObjectAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var memberNode in nodes)
            {
                if (graph.ContainsNode(memberNode.Parent.Identifier) == false)
                {
                    graph.AddNode(memberNode.Parent);
                }
            }
        }

        private void InitializeGraphDependencies(ObjectAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var memberNode in nodes)
            {
                var objectNode = graph.GetNode(memberNode.Parent.Identifier);

                foreach (var memberNodeSuccessor in memberNode.Successors)
                {
                    var successorMemberNode = graph.GetNode(memberNodeSuccessor.Parent.Identifier);
                    if (successorMemberNode != null)
                    {
                        objectNode.AddSuccesor(successorMemberNode);
                    }
                }
            }
        }
    }
}
