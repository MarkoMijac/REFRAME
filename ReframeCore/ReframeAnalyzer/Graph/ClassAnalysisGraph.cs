using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisGraph : AnalysisGraph
    {
        public override void InitializeGraph(IDependencyGraph graph)
        {
            Identifier = graph.Identifier;
            InitializeNodes(graph.Nodes);
            InitializeDependencies(graph.Nodes);
        }

        private void InitializeNodes(IEnumerable<INode> nodes)
        {
            if (nodes.Count() == 0)
            {
                return;
            }

            foreach (var node in nodes)
            {
                Type t = node.OwnerObject.GetType();
                uint identifier = (uint)t.GUID.GetHashCode();

                if (ContainsNode(identifier) == false)
                {
                    var classNode = new ClassAnalysisNode()
                    {
                        Identifier = identifier,
                        Name = t.Name,
                        FullName = t.FullName,
                        Namespace = t.Namespace,
                        Assembly = t.Assembly.ManifestModule.ToString(),  
                    };

                    AddNode(classNode);
                }
            }
        }

        private void InitializeDependencies(IEnumerable<INode> nodes)
        {
            if (nodes.Count() == 0)
            {
                return;
            }

            foreach (var node in nodes)
            {
                Type t = node.OwnerObject.GetType();
                uint identifier = (uint)t.GUID.GetHashCode();

                var predecessor = GetNode(identifier);
                foreach (var s in node.Successors)
                {
                    uint sTypeIdentifier = (uint)s.OwnerObject.GetType().GUID.GetHashCode();
                    var successor = GetNode(sTypeIdentifier);
                    if (successor != null)
                    {
                        predecessor.AddSuccesor(successor);
                        successor.AddPredecessor(predecessor);
                    }
                }
            }
        }
    }
}
