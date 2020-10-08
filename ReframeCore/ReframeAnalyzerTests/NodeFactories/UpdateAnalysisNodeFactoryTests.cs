using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.NodeFactories;
using ReframeAnalyzer.Nodes;

namespace ReframeAnalyzerTests.NodeFactories
{
    [TestClass]
    public class UpdateAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new UpdateAnalysisNodeFactory();

            //Act
            var updateNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new UpdateAnalysisNodeFactory();
            var xNode = XElement.Parse("<Node></Node>");

            //Act
            var updateNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsUpdateAnalysisNode()
        {
            //Arrange
            var factory = new UpdateAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetUpdateNode_XElement();

            //Act
            var updateNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(updateNode != null && updateNode.Level == AnalysisLevel.UpdateAnalysisLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectUpdateAnalysisNode()
        {
            //Arrange
            var factory = new UpdateAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetUpdateNode_XElement();

            //Act
            var updateNode = factory.CreateNode(xNode) as UpdateAnalysisNode;

            //Assert
            Assert.IsTrue(updateNode.Identifier == 3451459271
                && updateNode.Name == "B3"
                && updateNode.UpdateOrder == 1
                && updateNode.UpdateLayer == 1
                && updateNode.UpdateStartedAt == "14:2:29:848"
                && updateNode.UpdateCompletedAt == "14:2:29:848"
                && updateNode.UpdateDuration == "00:00:00.0000058"
                && updateNode.CurrentValue == "2"
                && updateNode.PreviousValue == "2"
                && updateNode.IsInitialNode == false
                && updateNode.NodeType == "PropertyNode");
        }
    }
}
