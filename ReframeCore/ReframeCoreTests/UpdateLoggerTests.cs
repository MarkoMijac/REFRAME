using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    [TestClass]
    public class UpdateLoggerTests
    {
        [TestMethod]
        public void Equals_GivenOneLoggerIsNull_ReturnsFalse()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new NodeFactory();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            UpdateLogger logger1 = new UpdateLogger();
            logger1.Log(nodes);

            UpdateLogger logger2 = null;

            //Act
            bool areEqual = logger1.Equals(logger2);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_GivenAnArgumentIsNotLogger_ReturnsFalse()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new NodeFactory();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            UpdateLogger logger1 = new UpdateLogger();
            logger1.Log(nodes);

            //Act
            bool areEqual = logger1.Equals(obj);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_GivenLoggersContainSameNodes_ReturnsTrue()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new NodeFactory();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            UpdateLogger logger1 = new UpdateLogger();
            logger1.Log(nodes);

            UpdateLogger logger2 = new UpdateLogger();
            logger2.Log(nodes);

            //Act
            bool areEqual = logger1.Equals(logger2);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_GivenLoggersContainDifferentNodes_ReturnsFalse()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new NodeFactory();

            INode aNode = nodeFactory.CreateNode(obj, "A");
            INode bNode = nodeFactory.CreateNode(obj, "B");
            INode cNode = nodeFactory.CreateNode(obj, "C");
            INode dNode = nodeFactory.CreateNode(obj, "D");

            UpdateLogger logger1 = new UpdateLogger();
            logger1.Log(aNode);
            logger1.Log(bNode);

            UpdateLogger logger2 = new UpdateLogger();
            logger2.Log(cNode);
            logger2.Log(dNode);

            //Act
            bool areEqual = logger1.Equals(logger2);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_GivenLoggersContainSameNodesInDifferentOrder_ReturnsFalse()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new NodeFactory();

            INode aNode = nodeFactory.CreateNode(obj, "A");
            INode bNode = nodeFactory.CreateNode(obj, "B");
            INode cNode = nodeFactory.CreateNode(obj, "C");
            INode dNode = nodeFactory.CreateNode(obj, "D");

            UpdateLogger logger1 = new UpdateLogger();
            logger1.Log(aNode);
            logger1.Log(bNode);
            logger1.Log(cNode);
            logger1.Log(dNode);

            UpdateLogger logger2 = new UpdateLogger();
            logger2.Log(cNode);
            logger2.Log(dNode);
            logger2.Log(aNode);
            logger2.Log(bNode);

            //Act
            bool areEqual = logger1.Equals(logger2);

            //Assert
            Assert.IsFalse(areEqual);
        }
    }
}
