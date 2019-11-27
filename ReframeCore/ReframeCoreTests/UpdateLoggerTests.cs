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
using ReframeCore.Factories;

namespace ReframeCoreTests
{
    [TestClass]
    public class UpdateLoggerTests
    {
        private NodeFactory nodeFactory = new StandardNodeFactory();

        [TestMethod]
        public void Equals_GivenOneLoggerIsNull_ReturnsFalse()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            NodeLog logger1 = new NodeLog();
            logger1.Log(nodes);

            NodeLog logger2 = null;

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

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            NodeLog logger1 = new NodeLog();
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

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            NodeLog logger1 = new NodeLog();
            logger1.Log(nodes);

            NodeLog logger2 = new NodeLog();
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

            INode aNode = nodeFactory.CreateNode(obj, "A");
            INode bNode = nodeFactory.CreateNode(obj, "B");
            INode cNode = nodeFactory.CreateNode(obj, "C");
            INode dNode = nodeFactory.CreateNode(obj, "D");

            NodeLog logger1 = new NodeLog();
            logger1.Log(aNode);
            logger1.Log(bNode);

            NodeLog logger2 = new NodeLog();
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

            INode aNode = nodeFactory.CreateNode(obj, "A");
            INode bNode = nodeFactory.CreateNode(obj, "B");
            INode cNode = nodeFactory.CreateNode(obj, "C");
            INode dNode = nodeFactory.CreateNode(obj, "D");

            NodeLog logger1 = new NodeLog();
            logger1.Log(aNode);
            logger1.Log(bNode);
            logger1.Log(cNode);
            logger1.Log(dNode);

            NodeLog logger2 = new NodeLog();
            logger2.Log(cNode);
            logger2.Log(dNode);
            logger2.Log(aNode);
            logger2.Log(bNode);

            //Act
            bool areEqual = logger1.Equals(logger2);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Log_GivenNullNode_NodeIsNotLogged()
        {
            //Arrange
            NodeLog logger = new NodeLog();
            INode nullNode = null;

            //Act
            logger.Log(nullNode);

            //Assert
            Assert.IsTrue(logger.Count == 0);
        }

        [TestMethod]
        public void Log_GivenNullNodeList_NodeIsNotLogged()
        {
            //Arrange
            NodeLog logger = new NodeLog();
            IList<INode> nullNodeList = null;

            //Act
            logger.Log(nullNodeList);

            //Assert
            Assert.IsTrue(logger.Count == 0);
        }

        [TestMethod]
        public void Log_GivenEmptyNodeList_NodesAreNotLogged()
        {
            //Arrange
            NodeLog logger = new NodeLog();
            IList<INode> emptyList = new List<INode>();

            //Act
            logger.Log(emptyList);

            //Assert
            Assert.IsTrue(logger.Count == 0);
        }

        [TestMethod]
        public void Log_GivenValidNode_NodeIsLogged()
        {
            GenericReactiveObject obj = new GenericReactiveObject();

            INode node = nodeFactory.CreateNode(obj, "A");
            NodeLog logger = new NodeLog();

            //Act
            logger.Log(node);

            //Assert
            Assert.IsTrue(logger.Count == 1);
        }

        [TestMethod]
        public void Log_GivenNonEmptyNodeList_NodesAreLogged()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            NodeLog logger = new NodeLog();

            //Act
            logger.Log(nodes);

            //Assert
            Assert.IsTrue(logger.Count == 4);
        }

        [TestMethod]
        public void Log_GivenNodeListContainsNullNode_OnlyNonNullNodesAreLogged()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));
            INode nullNode = null;
            nodes.Add(nullNode);

            NodeLog logger = new NodeLog();

            //Act
            logger.Log(nodes);

            //Assert
            Assert.IsTrue(logger.Count == 4);
        }

        [TestMethod]
        public void ClearLog_GivenLoggerAlreadyEmpty_LoggerRemainsEmpty()
        {
            //Arrange
            NodeLog logger = new NodeLog();

            //Act
            logger.ClearLog();

            //Assert
            Assert.IsTrue(logger.Count == 0);
        }

        [TestMethod]
        public void ClearLog_GivenLoggerNotEmpty_LoggerBecomesEmpty()
        {
            //Arrange
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> nodes = new List<INode>();
            nodes.Add(nodeFactory.CreateNode(obj, "A"));
            nodes.Add(nodeFactory.CreateNode(obj, "B"));
            nodes.Add(nodeFactory.CreateNode(obj, "C"));
            nodes.Add(nodeFactory.CreateNode(obj, "D"));

            NodeLog logger = new NodeLog();
            logger.Log(nodes);

            //Act
            logger.ClearLog();

            //Assert
            Assert.IsTrue(logger.Count == 0);
        }
    }
}
