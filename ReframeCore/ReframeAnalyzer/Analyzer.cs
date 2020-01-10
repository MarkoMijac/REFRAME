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
using ReframeAnalyzer.Xml;

namespace ReframeAnalyzer
{
    public static class Analyzer
    {
        public static string GetRegisteredReactors()
        {
            IReadOnlyList<IReactor> registeredReactors = ReactorRegistry.Instance.GetReactors();

            var xmlExporter = new XmlReactorExporter(registeredReactors);

            return xmlExporter.Export();
        }

        public static string GetRegisteredGraphs()
        {
            string xml = "";

            IReadOnlyList<IReactor> registeredReactors = ReactorRegistry.Instance.GetReactors();
            xml = XmlExporterImpl.ExportGraphs(registeredReactors);

            return xml;
        }

        public static string GetGraphNodes(IDependencyGraph graph)
        {
            string xml = "";

            xml = XmlExporterImpl.ExportNodes(graph.Nodes);

            return xml;
        }

        public static IAnalysisGraph GetClassMemberGraph(IDependencyGraph graph)
        {
            var analysisGraph = new ClassAnalysisGraph();

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

        #region Methods

        public static string GetClassAnalysisGraph(IDependencyGraph graph)
        {
            var graphFactory = new AnalysisGraphFactory();
            ClassAnalysisGraph classGraph = graphFactory.GetGraph(graph, GraphType.ClassGraph) as ClassAnalysisGraph;

            var xmlExporter = new XmlClassGraphExporter(classGraph);
            return xmlExporter.Export();
        }

        #endregion
    }
}
