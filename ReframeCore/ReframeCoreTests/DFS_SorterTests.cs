using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Factories;
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
    public class DFS_SorterTests
    {
        #region IList<INode> Sort(IEnumerable<INode> graphNodes)

        [TestMethod]
        public void Sort_GivenGraphIsNull_ReturnsEmptySortedList()
        {
            //Arrange
            ISorter sorter = new DFS_Sorter();
            IEnumerable<INode> graphNodes = null;

            //Act
            IList<INode> sortedNodes = sorter.Sort(graphNodes);

            //Assert
            Assert.IsTrue(sortedNodes.Count == 0);
        }

        [TestMethod]
        public void Sort_GivenGraphIsEmpty_ReturnsEmptySortedList()
        {
            //Arrange
            ISorter sorter = new DFS_Sorter();
            IEnumerable<INode> graphNodes = new List<INode>();

            //Act
            IList<INode> sortedNodes = sorter.Sort(graphNodes);

            //Assert
            Assert.IsTrue(sortedNodes.Count == 0);
        }

        private Tuple<IList<INode>, NodeLog> CreateTestCase1()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));

            graph.AddDependency(o1_A, o1_C);
            graph.AddDependency(o1_C, o1_E);
            graph.AddDependency(o1_E, o1_B);
            graph.AddDependency(o1_B, o1_D);
            graph.AddDependency(o1_D, o1_F);

            NodeLog properOrderLog = new NodeLog();
            properOrderLog.Log(o1_A);
            properOrderLog.Log(o1_C);
            properOrderLog.Log(o1_E);
            properOrderLog.Log(o1_B);
            properOrderLog.Log(o1_D);
            properOrderLog.Log(o1_F);

            Tuple<IList<INode>, NodeLog> caseParameters = new Tuple<IList<INode>, NodeLog>(graph.Nodes, properOrderLog);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase1_ReturnsProperlySortedList()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog> caseParameters = CreateTestCase1();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;

            ISorter sorter = new DFS_Sorter();

            //Act
            IList<INode> sortedNodes = sorter.Sort(nodesToSort);

            //Assert
            NodeLog actualNodeOrderLog = new NodeLog();
            actualNodeOrderLog.Log(sortedNodes);

            Assert.IsTrue(expectedNodeOrderLog.Equals(actualNodeOrderLog));
        }

        private Tuple<IList<INode>, NodeLog> CreateTestCase2()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));

            graph.AddDependency(o1_A, o1_C);
            graph.AddDependency(o1_C, o1_D);
            graph.AddDependency(o1_D, o1_F);
            graph.AddDependency(o1_A, o1_E);
            graph.AddDependency(o1_E, o1_B);
            graph.AddDependency(o1_B, o1_F);

            NodeLog properOrderLog = new NodeLog();
            properOrderLog.Log(o1_A);
            properOrderLog.Log(o1_E);
            properOrderLog.Log(o1_B);
            properOrderLog.Log(o1_C);
            properOrderLog.Log(o1_D);
            properOrderLog.Log(o1_F);

            Tuple<IList<INode>, NodeLog> caseParameters = new Tuple<IList<INode>, NodeLog>(graph.Nodes, properOrderLog);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase2_ReturnsProperlySortedList()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog> caseParameters = CreateTestCase2();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;

            ISorter sorter = new DFS_Sorter();

            //Act
            IList<INode> sortedNodes = sorter.Sort(nodesToSort);

            //Assert
            NodeLog actualNodeOrderLog = new NodeLog();
            actualNodeOrderLog.Log(sortedNodes);

            Assert.IsTrue(expectedNodeOrderLog.Equals(actualNodeOrderLog));
        }

        private Tuple<IList<INode>, NodeLog> CreateTestCase3()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));

            graph.AddDependency(o1_A, o1_C);
            graph.AddDependency(o1_A, o1_E);
            graph.AddDependency(o1_C, o1_E);
            graph.AddDependency(o1_C, o1_D);
            graph.AddDependency(o1_E, o1_D);
            graph.AddDependency(o1_E, o1_B);
            graph.AddDependency(o1_D, o1_B);
            graph.AddDependency(o1_D, o1_F);
            graph.AddDependency(o1_B, o1_F);

            NodeLog properOrderLog = new NodeLog();
            properOrderLog.Log(o1_A);
            properOrderLog.Log(o1_C);
            properOrderLog.Log(o1_E);
            properOrderLog.Log(o1_D);
            properOrderLog.Log(o1_B);
            properOrderLog.Log(o1_F);

            Tuple<IList<INode>, NodeLog> caseParameters = new Tuple<IList<INode>, NodeLog>(graph.Nodes, properOrderLog);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase3_ReturnsProperlySortedList()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog> caseParameters = CreateTestCase3();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;

            ISorter sorter = new DFS_Sorter();

            //Act
            IList<INode> sortedNodes = sorter.Sort(nodesToSort);

            //Assert
            NodeLog actualNodeOrderLog = new NodeLog();
            actualNodeOrderLog.Log(sortedNodes);

            Assert.IsTrue(expectedNodeOrderLog.Equals(actualNodeOrderLog));
        }

        private Tuple<IList<INode>, NodeLog> CreateTestCase4()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));

            graph.AddDependency(o1_A, o1_B);
            graph.AddDependency(o1_B, o1_C);
            graph.AddDependency(o1_C, o1_D);
            graph.AddDependency(o1_D, o1_E);
            graph.AddDependency(o1_E, o1_F);
            graph.AddDependency(o1_F, o1_A);

            NodeLog properOrderLog = new NodeLog();

            Tuple<IList<INode>, NodeLog> caseParameters = new Tuple<IList<INode>, NodeLog>(graph.Nodes, properOrderLog);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase4_ThrowsCyclicException()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog> caseParameters = CreateTestCase4();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;

            ISorter sorter = new DFS_Sorter();

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => sorter.Sort(nodesToSort));
        }

        #endregion

        #region IList<INode> Sort(IEnumerable<INode> graphNodes, INode initialNode)

        [TestMethod]
        public void Sort_GivenGraphIsNullAndInitialNotNull_ReturnsEmptySortedList()
        {
            //Arrange
            ISorter sorter = new DFS_Sorter();
            IEnumerable<INode> graphNodes = null;
            GenericReactiveObject o = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode initialNode = nodeFactory.CreateNode(o, nameof(o.A));

            //Act
            IList<INode> sortedNodes = sorter.Sort(graphNodes, initialNode);

            //Assert
            Assert.IsTrue(sortedNodes.Count == 0);
        }

        [TestMethod]
        public void Sort_GivenGraphIsEmptyAndInitialNodeNotNull_ReturnsEmptySortedList()
        {
            //Arrange
            ISorter sorter = new DFS_Sorter();
            IEnumerable<INode> graphNodes = new List<INode>();
            GenericReactiveObject o = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode initialNode = nodeFactory.CreateNode(o, nameof(o.A));

            //Act
            IList<INode> sortedNodes = sorter.Sort(graphNodes, initialNode);

            //Assert
            Assert.IsTrue(sortedNodes.Count == 0);
        }

        [TestMethod]
        public void Sort_GivenInitialNodeIsNull_ReturnsEmptySortedList()
        {
            //Arrange
            ISorter sorter = new DFS_Sorter();
            List<INode> graphNodes = new List<INode>();
            GenericReactiveObject o = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode nodeA = nodeFactory.CreateNode(o, nameof(o.A));
            graphNodes.Add(nodeA);

            INode initialNode = null;

            //Act
            IList<INode> sortedNodes = sorter.Sort(graphNodes, initialNode);

            //Assert
            Assert.IsTrue(sortedNodes.Count == 0);
        }

        private Tuple<IList<INode>, NodeLog, INode> CreateTestCase5()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));
            INode o1_G = nodeFactory.CreateNode(o1, nameof(o1.G));
            INode o1_H = nodeFactory.CreateNode(o1, nameof(o1.H));

            graph.AddDependency(o1_A, o1_C);
            graph.AddDependency(o1_C, o1_E);
            graph.AddDependency(o1_E, o1_B);
            graph.AddDependency(o1_B, o1_D);
            graph.AddDependency(o1_D, o1_F);
            graph.AddDependency(o1_F, o1_G);
            graph.AddDependency(o1_G, o1_H);

            NodeLog properOrderLog = new NodeLog();
            properOrderLog.Log(o1_D);
            properOrderLog.Log(o1_F);
            properOrderLog.Log(o1_G);
            properOrderLog.Log(o1_H);

            Tuple<IList<INode>, NodeLog, INode> caseParameters = new Tuple<IList<INode>, NodeLog, INode>(graph.Nodes, properOrderLog, o1_D);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase5_ReturnsProperlySortedList()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog, INode> caseParameters = CreateTestCase5();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;
            INode initialNode = caseParameters.Item3;

            ISorter sorter = new DFS_Sorter();

            //Act
            IList<INode> sortedNodes = sorter.Sort(nodesToSort, initialNode);

            //Assert
            NodeLog actualNodeOrderLog = new NodeLog();
            actualNodeOrderLog.Log(sortedNodes);

            Assert.IsTrue(expectedNodeOrderLog.Equals(actualNodeOrderLog));
        }

        private Tuple<IList<INode>, NodeLog, INode> CreateTestCase6()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            NodeFactory nodeFactory = new StandardNodeFactory();

            GenericReactiveObject o1 = new GenericReactiveObject();

            INode o1_A = nodeFactory.CreateNode(o1, nameof(o1.A));
            INode o1_B = nodeFactory.CreateNode(o1, nameof(o1.B));
            INode o1_C = nodeFactory.CreateNode(o1, nameof(o1.C));
            INode o1_D = nodeFactory.CreateNode(o1, nameof(o1.D));
            INode o1_E = nodeFactory.CreateNode(o1, nameof(o1.E));
            INode o1_F = nodeFactory.CreateNode(o1, nameof(o1.F));

            graph.AddDependency(o1_A, o1_C);
            graph.AddDependency(o1_A, o1_E);
            graph.AddDependency(o1_C, o1_E);
            graph.AddDependency(o1_C, o1_D);
            graph.AddDependency(o1_E, o1_D);
            graph.AddDependency(o1_E, o1_B);
            graph.AddDependency(o1_D, o1_B);
            graph.AddDependency(o1_D, o1_F);
            graph.AddDependency(o1_B, o1_F);

            NodeLog properOrderLog = new NodeLog();
            properOrderLog.Log(o1_E);
            properOrderLog.Log(o1_D);
            properOrderLog.Log(o1_B);
            properOrderLog.Log(o1_F);

            Tuple<IList<INode>, NodeLog, INode> caseParameters = new Tuple<IList<INode>, NodeLog, INode>(graph.Nodes, properOrderLog, o1_E);

            return caseParameters;
        }

        [TestMethod]
        public void Sort_Given_TestCase6_ReturnsProperlySortedList()
        {
            //Arrange
            Tuple<IList<INode>, NodeLog, INode> caseParameters = CreateTestCase6();
            IList<INode> nodesToSort = caseParameters.Item1;
            NodeLog expectedNodeOrderLog = caseParameters.Item2;
            INode initialNode = caseParameters.Item3;

            ISorter sorter = new DFS_Sorter();

            //Act
            IList<INode> sortedNodes = sorter.Sort(nodesToSort, initialNode);

            //Assert
            NodeLog actualNodeOrderLog = new NodeLog();
            actualNodeOrderLog.Log(sortedNodes);

            Assert.IsTrue(expectedNodeOrderLog.Equals(actualNodeOrderLog));
        }

        #endregion
    }
}
