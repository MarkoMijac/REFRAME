using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeCore.Factories;

namespace ReframeCoreTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void ClearPredecessors_GivenNodeHasNoPredecessors_NoPredecessorsAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode nodeA = nodeFactory.CreateNode(obj, "A");

            //Act
            int numOfRemovedPredecessors = nodeA.ClearPredecessors();

            //Assert
            Assert.AreEqual(0, numOfRemovedPredecessors);
        }

        [TestMethod]
        public void ClearPredecessors_GivenNodeHasPredecessors_AllPredecessorsAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode nodeA = nodeFactory.CreateNode(obj, "A");
            INode nodeB = nodeFactory.CreateNode(obj, "B");
            INode nodeC = nodeFactory.CreateNode(obj, "C");

            var graph = new DependencyGraph("G1");

            graph.AddDependency(nodeB, nodeA);
            graph.AddDependency(nodeC, nodeA);

            //Act
            int numOfRemovedPredecessors = nodeA.ClearPredecessors();

            //Assert
            Assert.AreEqual(2, numOfRemovedPredecessors);
            Assert.AreEqual(0, nodeA.Predecessors.Count);
            Assert.IsFalse(nodeB.Successors.Contains(nodeA));
            Assert.IsFalse(nodeC.Successors.Contains(nodeA));
        }

        [TestMethod]
        public void ClearSuccessors_GivenNodeHasNoSuccessors_NoSuccessorsAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode nodeA = nodeFactory.CreateNode(obj, "A");

            var graph = new DependencyGraph("G1");

            //Act
            int numOfRemovedSuccessors = nodeA.ClearSuccessors();

            //Assert
            Assert.AreEqual(0, numOfRemovedSuccessors);
        }

        [TestMethod]
        public void ClearSuccessors_GivenNodeHasSuccessors_AllSuccessorsAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode nodeA = nodeFactory.CreateNode(obj, "A");
            INode nodeB = nodeFactory.CreateNode(obj, "B");
            INode nodeC = nodeFactory.CreateNode(obj, "C");

            var graph = new DependencyGraph("G1");

            graph.AddDependency(nodeA, nodeB);
            graph.AddDependency(nodeA, nodeC);

            //Act
            int numOfRemovedSuccessors = nodeA.ClearSuccessors();

            //Assert
            Assert.AreEqual(2, numOfRemovedSuccessors);
            Assert.AreEqual(0, nodeA.Successors.Count);
            Assert.IsFalse(nodeB.Predecessors.Contains(nodeA));
            Assert.IsFalse(nodeC.Predecessors.Contains(nodeA));
        }

        [TestMethod]
        public void IsAlive_GivenStrongReferenceToObjectExists_WeakReferenceIsAlive()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode node = nodeFactory.CreateNode(obj, "A");

            //Act
            bool isAlive = node.OwnerObject != null;

            //Assert
            Assert.IsTrue(isAlive);
        }

        [TestMethod]
        public void IsAlive_GivenStrongReferenceToObjectDoesntExist_WeakReferenceIsNotAlive()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            INode node = nodeFactory.CreateNode(obj, "A");

            obj = null;
            GC.Collect();

            //Act
            bool isAlive = node.OwnerObject != null;

            //Assert
            Assert.IsFalse(isAlive);
        }

        [TestMethod]
        public void TestGarbageCollection()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();

            PropertyNode p = nodeFactory.CreateNode(obj, "A") as PropertyNode;

            Assert.IsTrue(p.OwnerObject != null);

            //Act
            obj = null;
            GC.Collect();

            //Assert
            Assert.IsTrue(p.OwnerObject == null);

        }
    }
}
