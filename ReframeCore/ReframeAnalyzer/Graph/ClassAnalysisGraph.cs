using ReframeCore;
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
            DependencyGraph = graph;
            InitializeNodes();
            InitializeDependencies();
        }

        private void InitializeNodes()
        {
            if (DependencyGraph == null || DependencyGraph.Nodes.Count == 0)
            {
                return;
            }

            foreach (var node in DependencyGraph.Nodes)
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
                        Assembly = t.Assembly.ManifestModule.ToString()
                    };

                    AddNode(classNode);
                }
            }
        }

        private void InitializeDependencies()
        {
            if (DependencyGraph == null || DependencyGraph.Nodes.Count == 0)
            {
                return;
            }

            foreach (var node in DependencyGraph.Nodes)
            {
                Type t = node.OwnerObject.GetType();
                uint identifier = (uint)t.GUID.GetHashCode();

                var predecessor = GetNode(identifier);
                foreach (var s in node.Successors)
                {
                    uint sTypeIdentifier = (uint)s.OwnerObject.GetType().GUID.GetHashCode();
                    var successor = GetNode(sTypeIdentifier);

                    predecessor.AddSuccesor(successor);
                    successor.AddPredecessor(predecessor);
                }
            }
        }
    }
}
