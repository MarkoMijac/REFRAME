using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore.Nodes;
using ReframeCore;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class NodeFactoryTests
    {
        [TestMethod]
        public void CreateNode_GivenNullOwnerObject_ThrowsException()
        {
            //Arrange
            Building00 building00 = null;
            string memberName = "Width";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => NodeFactory.CreateNode(building00, memberName));
        }

        [TestMethod]
        public void CreateNode_GivenWrongMemberName_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => NodeFactory.CreateNode(building00, memberName));
        }

        #region PropertyNode

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsSourcePropertyNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode node = NodeFactory.CreateNode(building, memberName);

            //Assert
            Assert.IsInstanceOfType(node, typeof(PropertyNode));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsSourcePropertyNodeWithCorrectData()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode widthNode = NodeFactory.CreateNode(building, memberName);

            //Assert
            Assert.IsTrue(widthNode.OwnerObject == building && widthNode.MemberName == memberName);
        }

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsNONSourcePropertyNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            INode node = NodeFactory.CreateNode(building, memberName, updateMethodName);

            //Assert
            Assert.IsInstanceOfType(node, typeof(PropertyNode));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsNONSourcePropertyNodeWithProperData()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            PropertyNode node = NodeFactory.CreateNode(building, memberName, updateMethodName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == memberName
                && node.UpdateMethod.Method.Name == updateMethodName);
        }

        [TestMethod]
        public void CreateNode_GivenMethodNameInsteadPropertyName_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Update_Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => NodeFactory.CreateNode(building00, memberName));
        }

        #endregion
    }
}
