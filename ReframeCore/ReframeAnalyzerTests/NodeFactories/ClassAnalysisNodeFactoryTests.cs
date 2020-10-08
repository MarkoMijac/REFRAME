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
    public class ClassAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new ClassAnalysisNodeFactory();

            //Act
            var classNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new ClassAnalysisNodeFactory();
            var xNode = XElement.Parse("<OwnerClass></OwnerClass>");

            //Act
            var classNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsClassAnalysisNode()
        {
            //Arrange
            var factory = new ClassAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetClassNode_XElement();

            //Act
            var classNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(classNode != null && classNode.Level == AnalysisLevel.ClassLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectClassAnalysisNode()
        {
            //Arrange
            var factory = new ClassAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetClassNode_XElement();

            //Act
            var classNode = factory.CreateNode(xNode) as ClassAnalysisNode;

            //Assert
            Assert.IsTrue(classNode.Identifier == 776132068
                && classNode.Name == "ClassB"
                && classNode.FullName == "ReframeCoreExamples.E09.ClassB"
                && classNode.Parent.Level == AnalysisLevel.NamespaceLevel
                && classNode.Parent2.Level == AnalysisLevel.AssemblyLevel);
        }
    }
}
