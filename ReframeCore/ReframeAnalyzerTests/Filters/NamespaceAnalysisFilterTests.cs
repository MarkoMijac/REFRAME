using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
using ReframeCore;
using ReframeExporter;

namespace ReframeAnalyzerTests.Filters
{
    [TestClass]
    public class NamespaceAnalysisFilterTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void Apply_GivenNullList_ThrowsException()
        {
            //Arrange
            var filter = new NamespaceAnalysisFilter(null);

            //Act
            var filteredNodes = filter.Apply();
        }

        [TestMethod]
        public void Apply_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            var originalNodes = new List<IAnalysisNode>();
            var filter = new NamespaceAnalysisFilter(originalNodes);

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(filteredNodes.Count == 0);
        }

        [TestMethod]
        public void Apply_GivenAllOptionsSelected_ReturnsCorrectFilteredList()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new NamespaceAnalysisFilter(analysisGraph.Nodes);
            filter.NamespaceFilterOption.SelectNodes();

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == filteredNodes.Count);
        }

        [TestMethod]
        public void Apply_GivenNoOptionsSelected_ReturnsEmptyFilteredList()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new NamespaceAnalysisFilter(analysisGraph.Nodes);
            filter.NamespaceFilterOption.DeselectNodes();

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(filteredNodes.Count == 0);
        }
    }
}
