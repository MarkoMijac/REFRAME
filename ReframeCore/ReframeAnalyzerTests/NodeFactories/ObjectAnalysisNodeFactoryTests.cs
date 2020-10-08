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
    public class ObjectAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new ObjectAnalysisNodeFactory();

            //Act
            var objectNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new ObjectAnalysisNodeFactory();
            var xNode = XElement.Parse("<OwnerObject></OwnerObject>");

            //Act
            var objectNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsObjectAnalysisNode()
        {
            //Arrange
            var factory = new ObjectAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectNode_XElement();

            //Act
            var objectNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(objectNode != null && objectNode.Level == AnalysisLevel.ObjectLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectObjectAnalysisNode()
        {
            //Arrange
            var factory = new ObjectAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectNode_XElement();

            //Act
            var objectNode = factory.CreateNode(xNode) as ObjectAnalysisNode;

            //Assert
            Assert.IsTrue(objectNode.Identifier == 12852035 
                && objectNode.Name == "First object A"
                && objectNode.Parent.Level == AnalysisLevel.ClassLevel);
        }
    }
}
