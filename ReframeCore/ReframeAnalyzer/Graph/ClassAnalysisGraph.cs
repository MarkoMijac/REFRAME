using System.Collections.Generic;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisGraph : AnalysisGraph
    {
        public ClassAnalysisGraph(ObjectAnalysisGraph objectAnalysisGraph)
        {
            AnalysisLevel = AnalysisLevel.ClassLevel;

            if (objectAnalysisGraph != null)
            {
                InitializeGraphBasicData(objectAnalysisGraph);
                if (objectAnalysisGraph.Nodes.Count > 0)
                {
                    InitializeGraphNodes(objectAnalysisGraph.Nodes);
                    InitializeGraphDependencies(objectAnalysisGraph.Nodes);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> objectNodes)
        {
            foreach (var objectNode in objectNodes)
            {
                var classNode = GetNode(objectNode.Parent.Identifier);

                foreach (var objectNodeSuccessor in objectNode.Successors)
                {
                    var successorClassNode = GetNode(objectNodeSuccessor.Parent.Identifier);
                    if (successorClassNode != null)
                    {
                        classNode.AddSuccesor(successorClassNode);
                    }
                }
            }
        }

        private void InitializeGraphNodes(List<IAnalysisNode> objectNodes)
        {
            foreach (var objectNode in objectNodes)
            {
                if (ContainsNode(objectNode.Parent.Identifier) == false)
                {
                    AddNode(objectNode.Parent);
                }
            }
        }

        private void InitializeGraphBasicData(ObjectAnalysisGraph objectAnalysisGraph)
        {
            Identifier = objectAnalysisGraph.Identifier;
        }
    }
}
