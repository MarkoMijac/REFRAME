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
    public class ObjectMemberAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new ObjectMemberAnalysisNodeFactory();

            //Act
            var objectMemberNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new ObjectMemberAnalysisNodeFactory();
            var xNode = XElement.Parse("<Node></Node>");

            //Act
            var objectMemberNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsObjectMemberAnalysisNode()
        {
            //Arrange
            var factory = new ObjectMemberAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectMemberNode_XElement();

            //Act
            var objectMemberNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(objectMemberNode != null && objectMemberNode.Level == AnalysisLevel.ObjectMemberLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectObjectMemberAnalysisNode()
        {
            //Arrange
            var factory = new ObjectMemberAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectMemberNode_XElement();

            //Act
            var objectMemberNode = factory.CreateNode(xNode) as ObjectMemberAnalysisNode;

            //Assert
            Assert.IsTrue(objectMemberNode.Identifier == 1526585190 
                && objectMemberNode.Name == "PA1"
                && objectMemberNode.NodeType == "PropertyNode"
                && objectMemberNode.CurrentValue == "0"
                && objectMemberNode.PreviousValue == ""
                && objectMemberNode.Parent.Level == AnalysisLevel.ObjectLevel);
        }
    }
}
