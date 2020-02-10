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
    public static class AnalysisTestCases
    {
        public static IAnalysisGraph CreateAnalysisGraph(IReactor reactor, AnalysisLevel level)
        {
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            return factory.CreateGraph(xmlSource, level);
        }

        public static IReactor CreateEmptyReactor()
        {
            ReactorRegistry.Instance.Clear();
            return ReactorRegistry.Instance.CreateReactor("R1");
        }

        public static IReactor CreateCase1()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");
            ClassC objC = new ClassC("Third object C");

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);
            return reactor;
        }

        public static IReactor CreateCase2()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("Object A");
            ClassB objB = new ClassB("Object B");
            ClassC objC = new ClassC("Object C");
            ClassG objG = new ClassG("Object G");

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

            ClassA firstObject = new ClassA("First object A");
            ClassA secondObject = new ClassA("Second object A");

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

            return reactor;
        }

    }
}
