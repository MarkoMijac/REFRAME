using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReframeCoreTests
{   
    public partial class DependencyGraphTests
    {
        #region SeparateThreadExecution

        [TestMethod]
        public async Task PerformUpdate_GivenCompletePerformUpdateIsPerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate();
            if (task != null) await task;
           
            //Assert
            UpdateLogger logger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "A"));
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(graph.GetNode(o, "B"), false);
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread1_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(graph.GetNode(o, "B"));
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread2_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(o, "B");
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenSuccessfulCompleteUpdateInSeparateThread_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            bool eventRaised = false;

            graph.UpdateCompleted += delegate 
            {
                eventRaised = true;
            };

            //Act
            Task task = graph.PerformUpdate();
            if (task != null) await task;

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public async Task PerformUpdate_GivenSuccessfulUpdateWithInitialNodeInSeparateThread_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.Updater.EnableUpdateInSeparateThread = true;

            bool eventRaised = false;

            graph.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            //Act
            Task task = graph.PerformUpdate(o, "C");
            if (task != null) await task;

            //Assert
            Assert.IsTrue(eventRaised);
        }

        #endregion

        #region ParallelExecution

        private void CreateCase1(IDependencyGraph graph, GenericReactiveObject3 obj)
        {
            INode aNode = graph.AddNode(obj, "A");
            INode bNode = graph.AddNode(obj, "B");
            INode cNode = graph.AddNode(obj, "C");
            INode dNode = graph.AddNode(obj, "D");
            INode eNode = graph.AddNode(obj, "E");
            INode fNode = graph.AddNode(obj, "F");
            INode gNode = graph.AddNode(obj, "G");
            INode hNode = graph.AddNode(obj, "H");
            INode iNode = graph.AddNode(obj, "I");

            INode jNode = graph.AddNode(obj, "J");
            INode kNode = graph.AddNode(obj, "K");
            INode lNode = graph.AddNode(obj, "L");
            INode mNode = graph.AddNode(obj, "M");

            graph.AddDependency(aNode, dNode);

            graph.AddDependency(bNode, dNode);

            graph.AddDependency(cNode, aNode);
            graph.AddDependency(cNode, bNode);

            graph.AddDependency(dNode, gNode);
            graph.AddDependency(dNode, hNode);

            graph.AddDependency(eNode, aNode);
            graph.AddDependency(eNode, dNode);
            graph.AddDependency(eNode, fNode);

            graph.AddDependency(fNode, jNode);
            graph.AddDependency(fNode, kNode);

            graph.AddDependency(gNode, iNode);

            graph.AddDependency(hNode, jNode);
            graph.AddDependency(hNode, iNode);

            graph.AddDependency(iNode, lNode);

            graph.AddDependency(jNode, lNode);
            graph.AddDependency(jNode, mNode);

            graph.AddDependency(kNode, jNode);

            graph.Initialize();
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenCompletePerformUpdatePerformedSynchronously_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            graph.PerformUpdate();

            //Assert
            UpdateLogger actualLogger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "E"));
            expectedLogger.Log(graph.GetNode(o, "F"));
            expectedLogger.Log(graph.GetNode(o, "K"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "A"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "H"));
            expectedLogger.Log(graph.GetNode(o, "J"));
            expectedLogger.Log(graph.GetNode(o, "M"));
            expectedLogger.Log(graph.GetNode(o, "G"));
            expectedLogger.Log(graph.GetNode(o, "I"));
            expectedLogger.Log(graph.GetNode(o, "L"));

            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenCompletePerformUpdatePerformedSynchronously_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(ResultsAsExpected(o));
        }

        private bool ResultsAsExpected(GenericReactiveObject3 obj)
        {
            bool correct = false;

            if (obj.C == 1 && obj.E == 1 && obj.A == 2 && obj.B == 2
                && obj.D == 5 && obj.F ==2 && obj.K == 3 && obj.J == 11
                && obj.G == 6 && obj.H == 6 && obj.I == 12 && obj.M == 12 && obj.L == 23)
            {
                correct = true;
            }

            return correct;
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenUpdatePerformedSynchronouslyWithInitialNodeH_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            graph.PerformUpdate(graph.GetNode(o, "H"), false);

            //Assert
            UpdateLogger actualLogger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "H"));
            expectedLogger.Log(graph.GetNode(o, "I"));
            expectedLogger.Log(graph.GetNode(o, "J"));
            expectedLogger.Log(graph.GetNode(o, "M"));
            expectedLogger.Log(graph.GetNode(o, "L"));

            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenUpdatePerformedSynchronouslyWithInitialNodeH_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);
            graph.PerformUpdate();

            //Act
            o.H = 7;
            graph.PerformUpdate(graph.GetNode(o, "H"));

            //Assert
            Assert.IsTrue(o.H == 7);
            Assert.IsTrue(o.J == 12);
            Assert.IsTrue(o.M == 13);
            Assert.IsTrue(o.I == 13);
            Assert.IsTrue(o.L == 25);
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();
            graph.Updater.EnableUpdateInSeparateThread = true;
            CreateCase1(graph, o);

            //Act
            Task t = graph.PerformUpdate();
            if (t != null) await t;

            //Assert
            UpdateLogger actualLogger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "E"));
            expectedLogger.Log(graph.GetNode(o, "F"));
            expectedLogger.Log(graph.GetNode(o, "K"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "A"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "H"));
            expectedLogger.Log(graph.GetNode(o, "J"));
            expectedLogger.Log(graph.GetNode(o, "M"));
            expectedLogger.Log(graph.GetNode(o, "G"));
            expectedLogger.Log(graph.GetNode(o, "I"));
            expectedLogger.Log(graph.GetNode(o, "L"));

            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInSeparateThread_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();
            graph.Updater.EnableUpdateInSeparateThread = true;
            CreateCase1(graph, o);

            //Act
            Task t = graph.PerformUpdate();
            if (t != null) await t;

            //Assert
            Assert.IsTrue(ResultsAsExpected(o));
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenUpdatePerformedInSeparateThreadWithInitialNodeH_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();
            graph.Updater.EnableUpdateInSeparateThread = true;
            CreateCase1(graph, o);

            //Act
            Task t = graph.PerformUpdate(graph.GetNode(o, "H"), false);
            if (t != null) await t;

            //Assert
            UpdateLogger actualLogger = graph.Updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "H"));
            expectedLogger.Log(graph.GetNode(o, "I"));
            expectedLogger.Log(graph.GetNode(o, "J"));
            expectedLogger.Log(graph.GetNode(o, "M"));
            expectedLogger.Log(graph.GetNode(o, "L"));

            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenUpdatePerformedInSeparateThreadWithInitialNodeH_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();
            graph.Updater.EnableUpdateInSeparateThread = true;
            CreateCase1(graph, o);

            Task tAll = graph.PerformUpdate();
            if (tAll != null) await tAll;

            //Act
            o.H = 7;
            Task t = graph.PerformUpdate(graph.GetNode(o, "H"));
            if (t != null) await t;

            //Assert
            Assert.IsTrue(o.H == 7);
            Assert.IsTrue(o.J == 12);
            Assert.IsTrue(o.M == 13);
            Assert.IsTrue(o.I == 13);
            Assert.IsTrue(o.L == 25);
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInParallel_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            graph.Updater.EnableUpdateInSeparateThread = true;
            graph.Updater.EnableParallelUpdate = true;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            Task t = graph.PerformUpdate();
            if (t != null) await t;

            //Assert
            Assert.AreEqual(5, graph.GetNode(o, "E").Level);
            Assert.AreEqual(5, graph.GetNode(o, "C").Level);

            Assert.AreEqual(4, graph.GetNode(o, "A").Level);
            Assert.AreEqual(4, graph.GetNode(o, "B").Level);

            Assert.AreEqual(3, graph.GetNode(o, "D").Level);
            Assert.AreEqual(3, graph.GetNode(o, "F").Level);

            Assert.AreEqual(2, graph.GetNode(o, "G").Level);
            Assert.AreEqual(2, graph.GetNode(o, "H").Level);
            Assert.AreEqual(2, graph.GetNode(o, "K").Level);

            Assert.AreEqual(1, graph.GetNode(o, "I").Level);
            Assert.AreEqual(1, graph.GetNode(o, "J").Level);

            Assert.AreEqual(0, graph.GetNode(o, "L").Level);
            Assert.AreEqual(0, graph.GetNode(o, "M").Level);
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInParallel_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            graph.Updater.EnableUpdateInSeparateThread = true;
            graph.Updater.EnableParallelUpdate = true;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            Task t = graph.PerformUpdate();
            if (t != null) await t;

            //Assert
            Assert.IsTrue(ResultsAsExpected(o));
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenUpdatePerformedInParallelWidthInitialNodeH_GivesCorrectResults()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            DependencyGraph graph = GraphRegistry.Instance.CreateGraph("G") as DependencyGraph;
            graph.Updater.EnableUpdateInSeparateThread = true;
            graph.Updater.EnableParallelUpdate = true;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);
            Task tAll = graph.PerformUpdate();
            if (tAll != null) await tAll;


            //Act
            o.H = 7;
            Task t = graph.PerformUpdate(o, "H");
            if (t != null) await t;

            //Assert
            Assert.IsTrue(o.H == 7);
            Assert.IsTrue(o.J == 12);
            Assert.IsTrue(o.M == 13);
            Assert.IsTrue(o.I == 13);
            Assert.IsTrue(o.L == 25);
        }

        #endregion
    }
}
