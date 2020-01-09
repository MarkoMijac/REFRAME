using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ReframeCore.Factories;
using ReframeAnalyzer.Graph;

namespace ReframeAnalyzer
{
    public static class Analyzer
    {
        public static string GetRegisteredGraphs()
        {
            string xml = "";

            IReadOnlyList<IReactor> registeredReactors = ReactorRegistry.Instance.GetReactors();
            xml = XmlExporter.ExportGraphs(registeredReactors);

            return xml;
        }

        public static string GetGraphNodes(IDependencyGraph graph)
        {
            string xml = "";

            xml = XmlExporter.ExportNodes(graph.Nodes);

            return xml;
        }

        public static IAnalysisGraph GetClassMemberGraph(IDependencyGraph graph)
        {
            AnalysisGraph analysisGraph = new AnalysisGraph();

            List<ClassAnalysisNode> classNodes = new List<ClassAnalysisNode>();

            foreach (var node in graph.Nodes)
            {
                Type t = node.OwnerObject.GetType();
                uint classIdentifier = (uint)t.GUID.GetHashCode();

                if (classNodes.Exists(c => c.Identifier == classIdentifier) == false)
                {
                    var classNode = new ClassAnalysisNode()
                    {
                        Identifier = classIdentifier,
                        Name = t.Name,
                        FullName = t.FullName,
                        Namespace = t.Namespace,
                        Assembly = t.Assembly.ManifestModule.ToString()
                    };

                    classNodes.Add(classNode);
                }

                if (analysisGraph.ContainsNode(classIdentifier) == false)
                {
                    var classNode = new ClassAnalysisNode()
                    {
                        Identifier = classIdentifier,
                        Name = t.Name,
                        FullName = t.FullName,
                        Namespace = t.Namespace,
                        Assembly = t.Assembly.ManifestModule.ToString()
                    };

                    var classMemberNode = new ClassMemberAnalysisNode()
                    {
                        ClassNode = classNode
                    };

                    analysisGraph.AddNode(classMemberNode);
                }
            }

            return analysisGraph;
        }

        public static string GetClassNodes(IDependencyGraph graph)
        {
            string xml = "";

            List<ClassAnalysisNode> classGraph = new List<ClassAnalysisNode>();

            foreach (var node in graph.Nodes)
            {
                Type t = node.OwnerObject.GetType();
                uint identifier = (uint)t.GUID.GetHashCode();

                if (classGraph.Exists(n => n.Identifier == identifier) == false)
                {
                    var classNode = new ClassAnalysisNode()
                    {
                        Identifier = identifier,
                        Name = t.Name,
                        FullName = t.FullName,
                        Namespace = t.Namespace,
                        Assembly = t.Assembly.ManifestModule.ToString()
                    };

                    classGraph.Add(classNode);
                }
            }

            foreach (var node in graph.Nodes)
            {
                Type t = node.OwnerObject.GetType();
                int identifier = t.GUID.GetHashCode();

                ClassAnalysisNode predecessor = classGraph.FirstOrDefault(n => n.Identifier == identifier);
                foreach (var s in node.Successors)
                {
                    int sTypeIdentifier = s.OwnerObject.GetType().GUID.GetHashCode();
                    ClassAnalysisNode successor = classGraph.FirstOrDefault(n => n.Identifier == sTypeIdentifier);

                    predecessor.AddSuccesor(successor);
                    successor.AddPredecessor(predecessor);
                }
            }

            xml = XmlExporter.ExportClassNodes(classGraph);

            return xml;
        }

    }
}
