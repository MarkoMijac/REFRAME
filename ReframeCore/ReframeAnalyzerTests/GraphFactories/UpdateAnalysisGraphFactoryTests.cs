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
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XMLSourceIsValid_ReturnsNonEmptyGraph()
        {
            //Arrange
            var objectMemberAnalysisGraph = new AnalysisGraph("G1", AnalysisLevel.ObjectMemberLevel);
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            //Act
            var graph = factory.CreateGraph(AnalysisTestHelper.GetUpdateInfoString());

            //Assert
            Assert.IsTrue(graph.Nodes.Count == 16);
        }
    }
}
