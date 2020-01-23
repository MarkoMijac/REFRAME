using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            foreach (ObjectAnalysisNode objectNode in objectNodes)
            {
                var classNode = GetNode(objectNode.OwnerClass.Identifier);

                foreach (ObjectAnalysisNode objectNodeSuccessor in objectNode.Successors)
                {
                    var successorClassNode = GetNode(objectNodeSuccessor.OwnerClass.Identifier);
                    if (successorClassNode != null && classNode != successorClassNode)
                    {
                        classNode.AddSuccesor(successorClassNode);
                    }
                }
            }
        }

        private void InitializeGraphNodes(List<IAnalysisNode> objectNodes)
        {
            foreach (ObjectAnalysisNode objectNode in objectNodes)
            {
                if (ContainsNode(objectNode.OwnerClass.Identifier) == false)
                {
                    AddNode(objectNode.OwnerClass);
                }
            }
        }

        private void InitializeGraphBasicData(ObjectAnalysisGraph objectAnalysisGraph)
        {
            Identifier = objectAnalysisGraph.Identifier;
        }
    }
}
