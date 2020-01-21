using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeAnalyzer;
using ReframeCoreExamples.E00;
using ReframeCore.FluentAPI;
using IPCServer;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Xml;
using System.Collections;
using System.Collections.Generic;
using ReframeExporter;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            string xml = @"<ServerCommand><RouterIdentifier>AnalyzerRouter</RouterIdentifier><CommandName>GetRegisteredGraphs</CommandName><Parameters/></ServerCommand>";

            string identifier = CommandRouter.GetRouterIdentifier(xml);

            Assert.AreEqual("AnalyzerRouter", identifier);
        }

        [TestMethod]
        public void CreateGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);

            //Act
            XmlExporter xmlExporter = new XmlExporter();
            string xmlSource = xmlExporter.Export(reactor);
            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);

            //Assert
            Assert.IsNotNull(analysisGraph);
        }

        //[TestMethod]
        public void GetClassMemberNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);


        }

        [TestMethod]
        public void GetClassAnalysisGraphSourceNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);

            var xmlExporter = new XmlExporter();
            var xmlSource = xmlExporter.Export(reactor);

            //Act
            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);

            IEnumerable<IAnalysisNode> sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes != null);
        }

        [TestMethod]
        public void GetPredecessors()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objE.PE1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objB.PB1).DependOn(() => objF.PF1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            XmlExporter xmlExporter = new XmlExporter();
            string xmlSource = xmlExporter.Export(reactor);
            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassD));
            
            IEnumerable<IAnalysisNode> predecessors = analyzer.GetPredecessors(analysisGraph, startingNode.Identifier.ToString());

            //Assert
            Assert.IsTrue(predecessors != null);
        }

        [TestMethod]
        public void GetPredecessors_WithMaxDepth()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objF.PF1).DependOn(() => objD.PD1);
            reactor.Let(() => objF.PF1).DependOn(() => objE.PE1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objE.PE1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            var xmlExporter = new XmlExporter();
            string xmlSource = xmlExporter.Export(reactor);
            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassF));

            IEnumerable<IAnalysisNode> predecessors = analyzer.GetPredecessors(analysisGraph, startingNode.Identifier.ToString(), 3);

            //Assert
            Assert.IsTrue(predecessors != null);
        }

        [TestMethod]
        public void GetSuccessors_ForDepth()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objF.PF1).DependOn(() => objD.PD1);
            reactor.Let(() => objF.PF1).DependOn(() => objE.PE1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objE.PE1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            var xmlExporter = new XmlExporter();
            string xmlSource = xmlExporter.Export(reactor);
            var analyzer = new ClassLevelAnalyzer();
            var analysisGraph = analyzer.CreateGraph(xmlSource);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassC));

            IEnumerable<IAnalysisNode> successors = analyzer.GetSuccessors(analysisGraph, startingNode.Identifier.ToString(), 2);

            //Assert
            Assert.IsTrue(successors != null);
        }
    }
}
