﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeAnalyzer.Graph;
using System.Collections.Generic;
using ReframeExporter;
using ReframeAnalyzer;
using ReframeCoreFluentAPI;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class ClassAnalysisGraphTests
    {
        private AnalysisGraphFactory graphFactory = new AnalysisGraphFactory();

        [TestMethod]
        public void GetSourceNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);

            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            var xmlSource = xmlExporter.Export();

            var analyzer = new Analyzer();
            var analysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Act
            IEnumerable<IAnalysisNode> sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes!=null);
        }
    }
}
