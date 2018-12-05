using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeConstructor_GivenCorrectArguments_CreatesSourceNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode widthNode = new Node(building, memberName);

            //Assert
            Assert.IsInstanceOfType(widthNode, typeof(INode));
        }

        [TestMethod]
        public void NodeConstructor_CreatedSourceNode_HoldsProperData()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode widthNode = new Node(building, memberName);

            //Assert
            Assert.IsTrue(widthNode.OwnerObject == building && widthNode.MemberName == memberName);
        }

        [TestMethod]
        public void NodeConstructor_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            try
            {
                //Act
                INode widthNode = new Node(building, memberName);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        [TestMethod]
        public void NodeConstructor_GivenCorrectArguments_CreatesNonSourceNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            INode widthNode = new Node(building, memberName, updateMethodName);

            //Assert
            Assert.IsInstanceOfType(widthNode, typeof(INode));
        }

        [TestMethod]
        public void NodeConstructor_GivenNullObject_ThrowsException()
        {
            //Arrange
            Building00 building00 = null;
            string memberName = "Width";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new Node(building00, memberName));
        }

        [TestMethod]
        public void NodeConstructor_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "NonexistantMember";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new Node(building00, memberName));
        }

    }
}
