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

namespace ReframeAnalyzer
{
    public static class Analyzer
    {
        public static string GetRegisteredGraphs()
        {
            string xml = "";

            List<IDependencyGraph> registeredGraphs = GraphFactory.GetRegisteredGraphs();
            xml = XmlExporter.ExportGraphs(registeredGraphs);

            return xml;
        }

        public static string GetGraphNodes(IDependencyGraph graph)
        {
            string xml = "";

            xml = XmlExporter.ExportNodes(graph.Nodes);

            return xml;
        }

        public static string GetClassNodes(IDependencyGraph graph)
        {
            string xml = "";

            List<StaticNode> classGraph = new List<StaticNode>();

            foreach (var node in graph.Nodes)
            {
                Type t = node.OwnerObject.GetType();
                int identifier = t.GUID.GetHashCode();

                if (classGraph.Exists(n => n.Identifier == identifier) == false)
                {
                    var classNode = new StaticNode()
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

                StaticNode predecessor = classGraph.FirstOrDefault(n => n.Identifier == identifier);
                foreach (var s in node.Successors)
                {
                    int sTypeIdentifier = s.OwnerObject.GetType().GUID.GetHashCode();
                    StaticNode successor = classGraph.FirstOrDefault(n => n.Identifier == sTypeIdentifier);

                    if (predecessor.Successors.Contains(successor) == false)
                    {
                        predecessor.Successors.Add(successor);
                    }
                }
            }

            xml = XmlExporter.ExportClassNodes(classGraph);

            return xml;
        }

    }
}
