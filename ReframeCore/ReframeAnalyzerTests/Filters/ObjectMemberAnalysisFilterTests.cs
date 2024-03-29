﻿using System;
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
    public class ObjectMemberAnalysisFilterTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void Apply_GivenNullList_ThrowsException()
        {
            //Arrange
            var filter = new ObjectMemberAnalysisFilter(null);

            //Act
            var filteredNodes = filter.Apply();
        }

        [TestMethod]
        public void Apply_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            var originalNodes = new List<IAnalysisNode>();
            var filter = new ObjectMemberAnalysisFilter(originalNodes);

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
            var factory = new ObjectMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
            filter.ObjectFilterOption.SelectNodes();

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
            var factory = new ObjectMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
            filter.ObjectFilterOption.DeselectNodes();

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
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
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
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
            var node = filter.NamespaceFilterOption.GetNodes()[0];
            filter.NamespaceFilterOption.SelectNode(node);

            //Act
            filter.NamespaceFilterOption.DeselectNode(node);

            //Assert
            Assert.IsTrue(filter.ClassFilterOption.GetSelectedNodes().Count == 0);
        }

        [TestMethod]
        public void GivenClassNodeSelected_ChildNodesAreSelected()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
            filter.ClassFilterOption.DeselectNodes();

            //Act
            var node = filter.ClassFilterOption.GetNodes()[0];
            filter.ClassFilterOption.SelectNode(node);

            //Assert
            Assert.IsTrue(filter.ObjectFilterOption.GetSelectedNodes().Count > 0);
        }

        [TestMethod]
        public void GivenClassNodeDeselected_ChildNodesAreDeselected()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            var filter = new ObjectMemberAnalysisFilter(analysisGraph.Nodes);
            var node = filter.ClassFilterOption.GetNodes()[0];
            filter.ObjectFilterOption.DeselectNodes();
            filter.ClassFilterOption.SelectNode(node);

            //Act
            filter.ClassFilterOption.DeselectNode(node);

            //Assert
            Assert.IsTrue(filter.ObjectFilterOption.GetSelectedNodes().Count == 0);
        }
    }
}
