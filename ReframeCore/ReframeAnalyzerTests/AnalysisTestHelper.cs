using ReframeAnalyzer.Graph;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCoreExamples.E09._01;
using ReframeCoreFluentAPI;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzerTests
{
    public static class AnalysisTestHelper
    {
        public static IAnalysisGraph CreateAnalysisGraph(IReactor reactor, AnalysisLevel level)
        {
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            IAnalysisGraph result;

            switch (level)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        var factory = new ObjectMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var factory = new ObjectAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var factory = new ClassMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var factory = new ClassAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var factory = new AssemblyAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var factory = new NamespaceAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public static IReactor CreateEmptyReactor()
        {
            ReactorRegistry.Instance.Clear();
            return ReactorRegistry.Instance.CreateReactor("R1");
        }

        public static IReactor CreateCase1()
        {
            List<object> repository = new List<object>();
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

        public static IReactor CreateCase2()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            List<object> repository = new List<object>();
            ClassA objA = new ClassA("Object A");
            ClassB objB = new ClassB("Object B");
            ClassC objC = new ClassC("Object C");
            ClassG objG = new ClassG("Object G");
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);
            repository.Add(objG);

            reactor.Let(() => objG.PG1)
                .DependOn(() => objB.PB1, () => objC.PC1, () => objG.PG2);
            reactor.Let(() => objB.PB1, () => objC.PC1).DependOn(() => objA.PA1);
            return reactor;
        }

        public static IReactor CreateCase3()
        {
            /*
             * Same class, different objects, same member.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            List<object> repository = new List<object>();

            ClassA firstObject = new ClassA("First object A");
            ClassA secondObject = new ClassA("Second object A");
            repository.Add(firstObject);
            repository.Add(secondObject);

            reactor.Let(() => firstObject.PA1)
                .DependOn(() => secondObject.PA1);
            return reactor;
        }

        /// <summary>
        /// Same class, same objects, different members.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase4()
        {
            /*
             * Same class, same objects, different members.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("First object A");
            reactor.Let(() => objA.PA1)
                .DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);

            return reactor;
        }

        /// <summary>
        /// Two different classes, two objects, 4 members.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase5()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");

            reactor.Let(() => objA.PA1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// More complex graph with orphan nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase6()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();
            ClassG objG = new ClassG();

            reactor.Let(() => objG.PG1).DependOn(() => objF.PF1, () => objE.PE1);
            reactor.Let(() => objF.PF1).DependOn(() => objB.PB1);
            reactor.Let(() => objE.PE1).DependOn(() => objB.PB1, () => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1, () => objD.PD1);

            //Add few orphan nodes
            reactor.AddNode(objA, nameof(objA.PA2));
            reactor.AddNode(objB, nameof(objB.PB2));

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);
            repository.Add(objD);
            repository.Add(objE);
            repository.Add(objF);
            repository.Add(objG);

            return reactor;
        }

        /// <summary>
        /// Graph with only orphan nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase7()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            //Add few orphan nodes
            reactor.AddNode(objA, nameof(objA.PA2));
            reactor.AddNode(objB, nameof(objB.PB2));

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// Graph with no intermediary nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase8()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase9()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objC.PC1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);

            return reactor;
        }
    }
}
