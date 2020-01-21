﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeAnalyzer.Graph;
using ReframeCore.FluentAPI;
using System.Collections.Generic;
using ReframeExporter;
using ReframeAnalyzer;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class ClassAnalysisGraphTests
    {
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

            XmlExporter xmlExporter = new XmlExporter();
            var xmlSource = xmlExporter.Export(reactor);

            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);

            //Act
            IReadOnlyList<IAnalysisNode> sourceNodes = analysisGraph.GetSourceNodes();

            //Assert
            Assert.IsTrue(sourceNodes.Count == 2);
        }
    }
}
