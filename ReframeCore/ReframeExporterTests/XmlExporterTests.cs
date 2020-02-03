using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.Factories;
using ReframeExporter;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using ReframeCoreExamples.E09;
using ReframeCoreFluentAPI;

namespace ReframeExporterTests
{
    [TestClass]
    public class XmlExporterTests
    {
        [TestMethod]
        public void Export_GivenRegisteredReactors_ExportsDocumentWithReactors()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            ReactorRegistry.Instance.GetOrCreateReactor("Graph_01");
            ReactorRegistry.Instance.GetOrCreateReactor("Graph_02");
            ReactorRegistry.Instance.GetOrCreateReactor("Graph_03");
            var xmlExporter = new XmlExporter();
            var reactors = ReactorRegistry.Instance.GetReactors();

            //Act
            string xmlSource = xmlExporter.Export(reactors);

            //Assert
            XElement xmlDocument = XElement.Parse(xmlSource);
            Assert.IsTrue(xmlDocument.Name == "Reactors");
            IEnumerable<XElement> reactorNodes = xmlDocument.Descendants("Reactor");
            Assert.IsTrue(reactorNodes.Count() == 3);
            XElement r1 = reactorNodes.First();
            Assert.IsTrue(r1!=null && r1.Name == "Reactor");
            Assert.IsTrue(r1.Element("Identifier").Value == "Graph_01");
            XElement xeGraph = r1.Element("Graph");
            Assert.IsTrue(xeGraph != null && xeGraph.Name == "Graph");
        }

        [TestMethod]
        public void Export_GivenNoRegisteredReactors_ExportsDocumentWithNoreactors()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var xmlExporter = new XmlExporter();
            var reactors = ReactorRegistry.Instance.GetReactors();

            //Act
            string xmlSource = xmlExporter.Export(reactors);

            //Assert
            XElement xmlDocument = XElement.Parse(xmlSource);
            IEnumerable<XElement> reactorNodes = from r in xmlDocument.Descendants("Reactor") select r;
            Assert.IsTrue(reactorNodes.Count() == 0);
        }

        [TestMethod]
        public void Export_GivenNonEmptyReactor_ExportsDocumentWithReactorData()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("Reactor_01");
            ClassA classA = new ClassA();
            ClassB classB = new ClassB();
            ClassC classC = new ClassC();

            reactor.Let(() => classC.PC1).DependOn(() => classB.PB1);
            reactor.Let(() => classC.PC2).DependOn(() => classA.PA2);
            reactor.Let(() => classB.PB1).DependOn(() => classA.PA1);

            var xmlExporter = new XmlExporter();

            //Act
            string xmlSource = xmlExporter.Export(reactor);

            //Assert
            XElement xmlDocument = XElement.Parse(xmlSource);
            Assert.IsTrue(xmlDocument != null && xmlDocument.Name == "Reactor");
            Assert.IsTrue(xmlDocument.Element("Identifier").Value == "Reactor_01");
            XElement xmlGraph = xmlDocument.Element("Graph");
            Assert.IsTrue(xmlGraph != null);
            Assert.IsTrue(xmlGraph.Descendants("Node").Count() == 5);
        }

        [TestMethod]
        public void Export_GivenEmptyReactor_ExportsDocumentWithNoGraphData()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("Reactor_01");

            var xmlExporter = new XmlExporter();

            //Act
            string xmlSource = xmlExporter.Export(reactor);

            //Assert
            XElement xmlDocument = XElement.Parse(xmlSource);
            Assert.IsTrue(xmlDocument != null && xmlDocument.Name == "Reactor");
            Assert.IsTrue(xmlDocument.Element("Identifier").Value == "Reactor_01");
            XElement xmlGraph = xmlDocument.Element("Graph");
            Assert.IsTrue(xmlGraph != null);
            Assert.IsTrue(xmlGraph.Descendants("Node").Count() == 0);
        }
    }
}
