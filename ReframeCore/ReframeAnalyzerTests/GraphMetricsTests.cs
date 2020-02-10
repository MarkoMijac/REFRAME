using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class GraphMetricsTests
    {
        #region GetNumberOfNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNumberOfNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;

            //Act
            int result = GraphMetrics.GetNumberOfNodes(analysisGraph);
        }

        [TestMethod]
        public void GetNumberOfNodes_GivenGraphIsEmpty_ReturnsZero()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateEmptyReactor();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetNumberOfNodes(analysisGraph);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetNumberOfNodes_GivenNonEmptyGraph_ReturnsCorrectNumberOfNodes()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetNumberOfNodes(analysisGraph);

            //Assert
            Assert.IsTrue(result == 5);
        }

        #endregion

        #region GetNumberOfEdges

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNumberOfEdges_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;

            //Act
            int result = GraphMetrics.GetNumberOfEdges(analysisGraph);
        }

        [TestMethod]
        public void GetNumberOfEdges_GivenGraphIsEmpty_ReturnsZero()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateEmptyReactor();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetNumberOfEdges(analysisGraph);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetNumberOfEdges_GivenNonEmptyGraph_ReturnsCorrectNumberOfEdges()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetNumberOfEdges(analysisGraph);

            //Assert
            Assert.IsTrue(result == 5);
        }

        #endregion

        #region GetMaximumNumberOfEdges

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetMaximumNumberOfEdges_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;

            //Act
            int result = GraphMetrics.GetMaximumNumberOfEdges(analysisGraph);
        }

        [TestMethod]
        public void GetMaximumNumberOfEdges_GivenGraphIsEmpty_ReturnsZero()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateEmptyReactor();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetMaximumNumberOfEdges(analysisGraph);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetMaximumNumberOfEdges_GivenObjectMemberGraph_ReturnsCorrectNumberOfEdges()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            int result = GraphMetrics.GetMaximumNumberOfEdges(analysisGraph);

            //Assert
            Assert.IsTrue(result == 10);
        }

        [TestMethod]
        public void GetMaximumNumberOfEdges_GivenNonObjectMemberGraph_ReturnsCorrectNumberOfEdges()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectLevel);

            //Act
            int result = GraphMetrics.GetMaximumNumberOfEdges(analysisGraph);

            //Assert
            Assert.IsTrue(result == 12);
        }

        #endregion

        #region GetGraphDensity

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetGraphDensity_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;

            //Act
            float result = GraphMetrics.GetGraphDensity(analysisGraph);
        }

        [TestMethod]
        public void GetGraphDensity_GivenGraphIsEmpty_ReturnsZero()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateEmptyReactor();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            float result = GraphMetrics.GetGraphDensity(analysisGraph);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetGraphDensity_GivenObjectMemberGraph_ReturnsCorrectGraphDensity()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);

            //Act
            float result = GraphMetrics.GetGraphDensity(analysisGraph);

            //Assert
            Assert.IsTrue(result == 0.5f);
        }

        [TestMethod]
        public void GetGraphDensity_GivenNonObjectMemberGraph_ReturnsCorrectGraphDensity()
        {
            //Arrange
            var reactor = AnalysisTestCases.CreateCase2();
            var analysisGraph = AnalysisTestCases.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectLevel);

            //Act
            float result = GraphMetrics.GetGraphDensity(analysisGraph);

            //Assert
            Assert.IsTrue(Math.Round(result, 3) == 0.417);
        }

        #endregion
    }
}
