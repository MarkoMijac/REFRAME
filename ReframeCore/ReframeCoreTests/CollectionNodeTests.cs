using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E07;
using ReframeCore;
using ReframeCore.Nodes;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class CollectionNodeTests
    {
        [TestMethod]
        public void ContainsChildNode_GivenProvidedArgumentIsNull_ReturnsFalse()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.GetOrCreate("G1");
            Whole whole = new Whole();
            ICollectionNode parts = graph.AddNode(whole.Parts, "A") as ICollectionNode;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => parts.ContainsChildNode(null));
        }

        [TestMethod]
        public void ContainsChildNode_GivenNodeIsChildNode_ReturnsTrue()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.GetOrCreate("G1");
            Whole whole = new Whole();
            ICollectionNode parts = graph.AddNode(whole.Parts, "A") as ICollectionNode;
            Part p = whole.Parts[0];
            INode pNode = graph.AddNode(p, "A");

            //Act
            bool contains = parts.ContainsChildNode(pNode);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsChildNode_GivenNodeIsNotChildNode_ReturnsFalse()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.GetOrCreate("G1");
            Whole whole = new Whole();
            ICollectionNode parts = graph.AddNode(whole.Parts, "A") as ICollectionNode;
            Part p = new Part();
            INode pNode = graph.AddNode(p, "A");

            //Act
            bool contains = parts.ContainsChildNode(pNode);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsChildNode_GivenNodeOwnerObjectDoesNotMatch_ReturnsFalse()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.GetOrCreate("G1");
            Whole whole = new Whole();
            ICollectionNode parts = graph.AddNode(whole.Parts, "A") as ICollectionNode;
            
            INode wNode = graph.AddNode(whole, "A");

            //Act
            bool contains = parts.ContainsChildNode(wNode);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsChildNode_GivenNodeMemberNameDoesNotMatch_ReturnsFalse()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.GetOrCreate("G1");
            Whole whole = new Whole();
            ICollectionNode parts = graph.AddNode(whole.Parts, "A") as ICollectionNode;
            Part p = whole.Parts[0];
            INode pNode = graph.AddNode(p, "B");

            //Act
            bool contains = parts.ContainsChildNode(pNode);

            //Assert
            Assert.IsFalse(contains);
        }
    }
}
