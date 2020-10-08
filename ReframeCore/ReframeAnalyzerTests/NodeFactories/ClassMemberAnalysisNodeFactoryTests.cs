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
    public class ClassMemberAnalysisNodeFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenNullParameter_ThrowsException()
        {
            //Arrange
            var factory = new ClassMemberAnalysisNodeFactory();

            //Act
            var classMemberNode = factory.CreateNode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateNode_GivenInvalidParameter_ThrowsException()
        {
            //Arrange
            var factory = new ClassMemberAnalysisNodeFactory();
            var xNode = XElement.Parse("<Node></Node>");

            //Act
            var classMemberNode = factory.CreateNode(xNode);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsClassMemberAnalysisNode()
        {
            //Arrange
            var factory = new ClassMemberAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectMemberNode_XElement(); //Koristi isti kao ObjectMember

            //Act
            var classMemberNode = factory.CreateNode(xNode);

            //Assert
            Assert.IsTrue(classMemberNode != null && classMemberNode.Level == AnalysisLevel.ClassMemberLevel);
        }

        [TestMethod]
        public void CreateNode_GivenValidParameter_ReturnsCorrectClassMemberAnalysisNode()
        {
            //Arrange
            var factory = new ClassMemberAnalysisNodeFactory();
            var xNode = AnalysisTestHelper.GetObjectMemberNode_XElement(); //Koristi isti kao ObjectMember

            //Act
            var classMemberNode = factory.CreateNode(xNode) as ClassMemberAnalysisNode;

            //Assert
            Assert.IsTrue(classMemberNode.Identifier == 1954224577
                && classMemberNode.Name == "PA1"
                && classMemberNode.NodeType == "PropertyNode"
                && classMemberNode.Parent.Level == AnalysisLevel.ClassLevel);
        }
    }
}
