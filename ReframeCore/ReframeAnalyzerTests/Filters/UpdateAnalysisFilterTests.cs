using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
using ReframeCore;
using ReframeExporter;

namespace ReframeAnalyzerTests.Filters
{
    [TestClass]
    public class UpdateAnalysisFilterTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void Apply_GivenNullList_ThrowsException()
        {
            //Arrange
            var filter = new UpdateAnalysisFilter(null);

            //Act
            var filteredNodes = filter.Apply();
        }

        [TestMethod]
        public void Apply_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            var originalNodes = new List<IAnalysisNode>();
            var filter = new UpdateAnalysisFilter(originalNodes);

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(filteredNodes.Count == 0);
        }

        public void GivenNamespaceNodeSelected_ChildNodesAreSelected()
        {
            //Arrange
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);
            var analysisGraph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            var filter = new ObjectAnalysisFilter(analysisGraph.Nodes);
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
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);
            var analysisGraph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            var filter = new UpdateAnalysisFilter(analysisGraph.Nodes);
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
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);
            var analysisGraph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            var filter = new UpdateAnalysisFilter(analysisGraph.Nodes);
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
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);
            var analysisGraph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            var filter = new UpdateAnalysisFilter(analysisGraph.Nodes);
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
