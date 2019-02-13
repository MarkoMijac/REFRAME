using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    [TestClass]
    public class CollectionNodeTests
    {
        #region NodeCreation

        [TestMethod]
        public void NodeCreation_GivenCorrectArguments_CreatesSourceNode()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1"};

            //Act
            INode partsNode = new CollectionNode<Part>(whole.Parts, "A");

            //Assert
            Assert.IsInstanceOfType(partsNode, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_CreatedSourceNode_HoldsProperData()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = whole.Parts;

            //Act
            INode partsNode = new CollectionNode<Part>(parts, "A");

            //Assert
            Assert.IsTrue(partsNode.OwnerObject == parts && partsNode.MemberName == "A");
        }

        [TestMethod]
        public void NodeCreation_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            try
            {
                //Act
                INode partsNode = new CollectionNode<Part>(whole.Parts, "A");
                partsNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        [TestMethod]
        public void NodeCreation_GivenNullObject_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new CollectionNode<Part>(parts, "A"));
        }

        [TestMethod]
        public void NodeCreation_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new CollectionNode<Part>(whole.Parts, "AA"));
        }

        [TestMethod]
        public void NodeCreation_CreatedReactiveNode_ObtainedIdentifier()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act
            INode partsNode = new CollectionNode<Part>(whole.Parts, "A");

            //Assert
            Assert.AreNotEqual(default(uint), partsNode.Identifier);
        }

        #endregion
    }
}
