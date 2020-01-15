using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Xml;
using ReframeCore;
using ReframeCore.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public abstract class Analyzer
    {
        #region Revise

        public string GetRegisteredReactors()
        {
            IReadOnlyList<IReactor> registeredReactors = ReactorRegistry.Instance.GetReactors();

            var xmlExporter = new XmlReactorExporter(registeredReactors);

            return xmlExporter.Export();
        }

        public string GetRegisteredGraphs()
        {
            string xml = "";

            IReadOnlyList<IReactor> registeredReactors = ReactorRegistry.Instance.GetReactors();
            xml = XmlExporterImpl.ExportGraphs(registeredReactors);

            return xml;
        }

        public string GetGraphNodes(IDependencyGraph graph)
        {
            string xml = "";

            xml = XmlExporterImpl.ExportNodes(graph.Nodes);

            return xml;
        }

        public IAnalysisGraph GetClassMemberGraph(IDependencyGraph graph)
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

        #endregion

        #region Methods

        public abstract IAnalysisGraph GetAnalysisGraph(IDependencyGraph graph);

        protected string GetOrphanNodes(IAnalysisGraph analysisGraph)
        {
            var xmlExporter = new XmlNodesExporter(analysisGraph.GetOrphanNodes());
            return xmlExporter.Export();
        }

        protected string GetLeafNodes(IAnalysisGraph analysisGraph)
        {
            var xmlExporter = new XmlNodesExporter(analysisGraph.GetLeafNodes());
            return xmlExporter.Export();
        }

        protected string GetSourceNodes(IAnalysisGraph analysisGraph)
        {
            var xmlExporter = new XmlNodesExporter(analysisGraph.GetSourceNodes());
            return xmlExporter.Export();
        }

        protected string GetSinkNodes(IAnalysisGraph analysisGraph)
        {
            var xmlExporter = new XmlNodesExporter(analysisGraph.GetSinkNodes());
            return xmlExporter.Export();
        }

        protected string GetIntermediaryNodes(IAnalysisGraph analysisGraph)
        {
            var xmlExporter = new XmlNodesExporter(analysisGraph.GetIntermediaryNodes());
            return xmlExporter.Export();
        }

        #endregion
    }
}
