using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCoreFluentAPI;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerDGMLTests
{
    class VisualizationTestHelper
    {
        public static List<IAnalysisNode> GetAssemblyAnalysisNodes()
        {
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            var n1 = new AssemblyAnalysisNode(1);
            var n2 = new AssemblyAnalysisNode(2);
            var n3 = new AssemblyAnalysisNode(3);

            n1.AddSuccessor(n3);
            n2.AddSuccessor(n3);

            analysisNodes.Add(n1);
            analysisNodes.Add(n2);
            analysisNodes.Add(n3);

            return analysisNodes;
        }

        public static List<IAnalysisNode> GetNamespaceAnalysisNodes()
        {
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            var n1 = new NamespaceAnalysisNode(1);
            var n2 = new NamespaceAnalysisNode(2);
            var n3 = new NamespaceAnalysisNode(3);

            n1.AddSuccessor(n3);
            n2.AddSuccessor(n3);

            analysisNodes.Add(n1);
            analysisNodes.Add(n2);
            analysisNodes.Add(n3);

            return analysisNodes;
        }

        public static List<IAnalysisNode> GetClassAnalysisNodes()
        {
            var reactor = CreateCase1();
            var analysisGraph = CreateAnalysisGraph(reactor, AnalysisLevel.ClassLevel);
            return analysisGraph.Nodes;
        }

        public static List<IAnalysisNode> GetClassMemberAnalysisNodes()
        {
            var reactor = CreateCase1();
            var analysisGraph = CreateAnalysisGraph(reactor, AnalysisLevel.ClassMemberLevel);
            return analysisGraph.Nodes;
        }

        public static List<IAnalysisNode> GetObjectAnalysisNodes()
        {
            var reactor = CreateCase1();
            var analysisGraph = CreateAnalysisGraph(reactor, AnalysisLevel.ObjectLevel);
            return analysisGraph.Nodes;
        }

        public static List<IAnalysisNode> GetObjectMemberAnalysisNodes()
        {
            var reactor = CreateCase1();
            var analysisGraph = CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            return analysisGraph.Nodes;
        }

        public static List<IAnalysisNode> GetUpdateAnalysisNodes()
        {
            List<object> repository = new List<object>();
            var reactor = CreateCase1(repository);
            reactor.PerformUpdate();
            var analysisGraph = CreateAnalysisGraph(reactor, AnalysisLevel.UpdateAnalysisLevel);
            repository.Clear();
            return analysisGraph.Nodes;
        }

        private static IReactor CreateCase1(List<object> repo = null)
        {

            List<object> repository = repo;
            if (repository == null)
            {
                repository = new List<object>();
            }
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");
            ClassC objC = new ClassC("Third object C");
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);
            return reactor;
        }

        public static IAnalysisGraph CreateAnalysisGraph(IReactor reactor, AnalysisLevel level)
        {
            IAnalysisGraph result;

            switch (level)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new ObjectMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new ObjectAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new ClassMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new ClassAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new AssemblyAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
                        var factory = new NamespaceAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.UpdateAnalysisLevel:
                    {
                        var omGraph = CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
                        var factory = new UpdateAnalysisGraphFactory(omGraph);

                        var xmlUpdateSource = new XmlUpdaterInfoExporter(reactor.Identifier).Export();
                        result = factory.CreateGraph(xmlUpdateSource);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            return result;
        }
    }
}
