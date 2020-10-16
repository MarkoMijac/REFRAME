using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.GraphFactories;
using ReframeCore.Factories;
using ReframeExporter;

namespace ReframeAnalyzerTests.GraphFactories
{
    [TestClass]
    public class UpdateAnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_GivenObjectMemberGraphIsNull_ThrowsException()
        {
            //Arrange
            var factory = new UpdateAnalysisGraphFactory(null);

            //Act
            var analysisGraph = factory.CreateGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            var objectMemberAnalysisGraph = new AnalysisGraph("G1", AnalysisLevel.ObjectMemberLevel);
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            //Act
            var graph = factory.CreateGraph("");
        }

        [TestMethod]
        public void CreateGraph_XMLSourceIsValid_ReturnsNonEmptyGraph()
        {
            //Arrange
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            //Act
            var graph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            //Assert
            Assert.IsTrue(graph.Nodes.Count == 16);
        }

        [TestMethod]
        public void CreateGraph_GivenUpdateError_ContainsErrorData()
        {
            //Arrange
            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(AnalysisTestHelper.GetReactorXML());
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            //Act
            var graph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoStringWithUpdateError()) as UpdateAnalysisGraph;

            //Assert
            Assert.IsTrue(graph.FailedNodeIdentifier == 3451262663
                && graph.FailedNodeName == "B2"
                && graph.FailedNodeOwner == "ClassB"
                && graph.SourceException == "Null reference exception!"
                && graph.StackTrace == "Test stack trace");
        }

        [TestMethod]
        public void CreateGraph_GivenUpdateCause_ContainsCauseData()
        {
            //Arrange
            var objectMemberAnalysisGraph = new AnalysisGraph("G1", AnalysisLevel.ObjectMemberLevel);
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            //Act
            var graph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoStringWithCause()) as UpdateAnalysisGraph;

            //Assert
            Assert.IsTrue(graph.CauseMessage == "Complete graph update requested!"
                && graph.InitialNodeIdentifier == 3451262663
                && graph.InitialNodeName == "B2"
                && graph.InitialNodeOwner == "ClassB"
                && graph.InitialNodeCurrentValue == "10"
                && graph.InitialNodePreviousValue == "9");
        }
    }
}
