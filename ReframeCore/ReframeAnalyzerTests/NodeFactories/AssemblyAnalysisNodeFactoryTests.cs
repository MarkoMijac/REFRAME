using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.NodeFactories;

namespace ReframeAnalyzerTests.NodeFactories
{
    [TestClass]
    public class AssemblyAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new AssemblyAnalysisNodeFactory();

            //Act
            var assemblyNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new AssemblyAnalysisNodeFactory();
            var xNode = XElement.Parse("<OwnerAssembly></OwnerAssembly>");

            //Act
            var assemblyNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsAssemblyAnalysisNode()
        {
            //Arrange
            var factory = new AssemblyAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetAssemblyNode_XElement();

            //Act
            var assemblyNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(assemblyNode != null && assemblyNode.Level == AnalysisLevel.AssemblyLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectAssemblyAnalysisNode()
        {
            //Arrange
            var factory = new AssemblyAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetAssemblyNode_XElement();

            //Act
            var assemblyNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(assemblyNode.Identifier == 63906962 && assemblyNode.Name == "ReframeCoreExamples.dll");
        }
    }
}
