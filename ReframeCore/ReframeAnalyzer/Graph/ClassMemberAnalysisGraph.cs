using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisGraph : AnalysisGraph
    {
        public ClassMemberAnalysisGraph(ObjectMemberAnalysisGraph objectMemberGraph)
        {
            AnalysisLevel = AnalysisLevel.ClassMemberLevel;

            if (objectMemberGraph != null)
            {
                InitializeGraphBasicData(objectMemberGraph);
                if (objectMemberGraph.Nodes.Count > 0)
                {
                    InitializeGraphNodes(objectMemberGraph.Nodes);
                    InitializeGraphDependencies(objectMemberGraph.Nodes);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> nodes)
        {
            foreach (ObjectMemberAnalysisNode objectMemberNode in nodes)
            {
                uint identifier = ClassMemberAnalysisNode.GenerateIdentifier(objectMemberNode.Name, objectMemberNode.OwnerObject.OwnerClass.Identifier);

                var classMemberNode = GetNode(identifier);

                foreach (ObjectMemberAnalysisNode objectMemberNodeSuccessor in objectMemberNode.Successors)
                {
                    uint successorIdentifier = ClassMemberAnalysisNode.GenerateIdentifier(objectMemberNodeSuccessor.Name, objectMemberNodeSuccessor.OwnerObject.OwnerClass.Identifier);
                    var successorClassMemberNode = GetNode(successorIdentifier);
                    if (successorClassMemberNode != null && classMemberNode != successorClassMemberNode)
                    {
                        classMemberNode.AddSuccesor(successorClassMemberNode);
                    }
                }
            }
        }

        private void InitializeGraphNodes(List<IAnalysisNode> nodes)
        {
            foreach (ObjectMemberAnalysisNode objectMemberNode in nodes)
            {
                uint identifier = ClassMemberAnalysisNode.GenerateIdentifier(objectMemberNode.Name, objectMemberNode.OwnerObject.OwnerClass.Identifier);
               
                if (ContainsNode(identifier) == false)
                {
                    var classMemberNode = new ClassMemberAnalysisNode(objectMemberNode);
                    AddNode(classMemberNode);
                }
            }
        }

        private void InitializeGraphBasicData(ObjectMemberAnalysisGraph objectMemberGraph)
        {
            Identifier = objectMemberGraph.Identifier;
        }
    }
}
