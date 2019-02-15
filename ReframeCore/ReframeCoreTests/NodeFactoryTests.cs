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
            NodeFactory nodeFactory = new NodeFactory();
            Building00 building00 = null;
            string memberName = "Width";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => nodeFactory.CreateNode(building00, memberName));
        }

        [TestMethod]
        public void CreateNode_GivenWrongMemberName_ThrowsException()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            Building00 building00 = new Building00();
            string memberName = "Width_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => nodeFactory.CreateNode(building00, memberName));
        }

        #region PropertyNode

        [TestMethod]
        public void CreateNode_GivenWrongUpdateMethodIsProvided_ThrowsException()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => nodeFactory.CreateNode(building00, propertyName, updateMethodName));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectUpdateMethodIsProvided_ReturnsPropertyNodeWithUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            Building00 building = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName, updateMethodName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod.Method.Name == updateMethodName);
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsEnabledAndExisting_ReturnsPropertyNodeWithDefaultUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = true; //This is default option
            Building00 building = new Building00();
            string propertyName = "Area";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod.Method.Name == nodeFactory.GenerateDefaultUpdateMethodName(propertyName));
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsDisabled_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = false; //This is default option
            Building00 building = new Building00();
            string propertyName = "Area";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod == null);
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsEnabledButNotExisting_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = true; //This is default option
            Building00 building = new Building00();
            string propertyName = "Width";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod==null);
        }

        [TestMethod]
        public void CreateNode_GivenMethodNameInsteadOfPropertyName_ThrowsException()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Update_Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => nodeFactory.CreateNode(building00, propertyName));
        }

        #endregion
    }
}
