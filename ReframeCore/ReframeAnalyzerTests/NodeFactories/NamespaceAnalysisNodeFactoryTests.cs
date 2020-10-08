using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.NodeFactories;

namespace ReframeAnalyzerTests.NodeFactories
{
    [TestClass]
    public class NamespaceAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new NamespaceAnalysisNodeFactory();

            //Act
            var namespaceNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new NamespaceAnalysisNodeFactory();
            var xNode = XElement.Parse("<OwnerNamespace></OwnerNamespace>");

            //Act
            var namespaceNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsNamespaceAnalysisNode()
        {
            //Arrange
            var factory = new NamespaceAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetNamespaceNode_XElement();

            //Act
            var namespaceNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(namespaceNode != null && namespaceNode.Level == AnalysisLevel.NamespaceLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectNamespaceAnalysisNode()
        {
            //Arrange
            var factory = new NamespaceAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetNamespaceNode_XElement();

            //Act
            var namespaceNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(namespaceNode.Identifier == 3679577347 && namespaceNode.Name == "ReframeCoreExamples.E09");
        }
    }
}
