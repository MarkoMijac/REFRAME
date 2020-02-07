using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ObjectAnalysisGraph : AnalysisGraph
    {
        public ObjectAnalysisGraph(ObjectMemberAnalysisGraph objectMemberAnalysisGraph)
        {
            AnalysisLevel = AnalysisLevel.ObjectLevel;

            if (objectMemberAnalysisGraph != null)
            {
                InitializeGraphBasicData(objectMemberAnalysisGraph);
                if (objectMemberAnalysisGraph.Nodes.Count > 0)
                {
                    InitializeGraphNodes(objectMemberAnalysisGraph.Nodes);
                    InitializeGraphDependencies(objectMemberAnalysisGraph.Nodes);
                }
            }
        }

        private void InitializeGraphBasicData(ObjectMemberAnalysisGraph objectMemberAnalysisGraph)
        {
            Identifier = objectMemberAnalysisGraph.Identifier;
        }

        private void InitializeGraphNodes(List<IAnalysisNode> memberNodes)
        {
            foreach (ObjectMemberAnalysisNode memberNode in memberNodes)
            {
                if (ContainsNode(memberNode.OwnerObject.Identifier) == false)
                {
                    AddNode(memberNode.OwnerObject);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> memberNodes)
        {
            foreach (ObjectMemberAnalysisNode memberNode in memberNodes)
            {
                var objectNode = GetNode(memberNode.OwnerObject.Identifier);

                foreach (ObjectMemberAnalysisNode memberNodeSuccessor in memberNode.Successors)
                {
                    var successorMemberNode = GetNode(memberNodeSuccessor.OwnerObject.Identifier);
                    if (successorMemberNode != null)
                    {
                        objectNode.AddSuccesor(successorMemberNode);
                    }
                }
            }
        }
    }
}
