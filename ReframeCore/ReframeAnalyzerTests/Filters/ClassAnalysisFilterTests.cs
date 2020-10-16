using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
using ReframeCore;
using ReframeExporter;

namespace ReframeAnalyzerTests.Filters
{
    [TestClass]
    public class ClassAnalysisFilterTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void Apply_GivenNullList_ThrowsException()
        {
            //Arrange
            var filter = new ClassAnalysisFilter(null);

            //Act
            var filteredNodes = filter.Apply();
        }

        [TestMethod]
        public void Apply_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            var originalNodes = new List<IAnalysisNode>();
            var filter = new ClassAnalysisFilter(originalNodes);

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
            var factory = new ClassAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new ClassAnalysisFilter(analysisGraph.Nodes);
            filter.ClassFilterOption.SelectNodes();

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
            var factory = new ClassAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new ClassAnalysisFilter(analysisGraph.Nodes);
            filter.ClassFilterOption.DeselectNodes();

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(filteredNodes.Count == 0);
        }

        [TestMethod]
        public void GivenNamespaceNodeSelected_ChildNodesAreSelected()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ClassLevel);

            var filter = new ClassAnalysisFilter(analysisGraph.Nodes);
            filter.NamespaceFilterOption.DeselectNodes();

            //Act
            var node = filter.NamespaceFilterOption.GetNodes()[0];
            filter.NamespaceFilterOption.SelectNode(node);

            //Assert
            Assert.IsTrue(filter.ClassFilterOption.GetSelectedNodes().Count > 0);
        }

        [TestMethod]
        public void GivenNamespaceNodeDeselected_ChildNodesAreDeselected()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ClassLevel);

            var filter = new ClassAnalysisFilter(analysisGraph.Nodes);
            var node = filter.NamespaceFilterOption.GetNodes()[0];
            filter.NamespaceFilterOption.SelectNode(node);

            //Act
            filter.NamespaceFilterOption.DeselectNode(node);

            //Assert
            Assert.IsTrue(filter.ClassFilterOption.GetSelectedNodes().Count == 0);
        }
    }
}
