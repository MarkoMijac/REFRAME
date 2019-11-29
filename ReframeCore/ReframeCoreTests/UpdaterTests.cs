using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using ReframeCoreExamples.E01;
using ReframeCoreExamples.E02;
using ReframeCoreExamples.E04;
using ReframeCoreExamples.E06;
using ReframeCoreExamples.E07;
using ReframeCoreExamples.E07_1;
using ReframeCoreExamples.E08.E1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    [TestClass]
    public partial class UpdaterTests
    {
        private NodeFactory nodeFactory = new StandardNodeFactory();

        #region GENERAL

        [TestMethod]
        public void PerformUpdate_GivenUpdateIsSuspended_PerformsNoUpdate()
        {
            //Arrange
            IDependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = nodeFactory.CreateNode(building, "Width");
            INode lengthNode = nodeFactory.CreateNode(building, "Length");
            INode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area");
            INode heightNode = nodeFactory.CreateNode(building, "Height");
            INode volumeNode = nodeFactory.CreateNode(building, "Volume");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            INode initialNode = widthNode;

            Updater updater = CreateUpdater(graph);

            //Act
            updater.SuspendUpdate();
            updater.PerformUpdate(initialNode);
            updater.ResumeUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = new NodeLog();
            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_GivenInitialNodeIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = nodeFactory.CreateNode(building, "Width");
            INode lengthNode = nodeFactory.CreateNode(building, "Length");
            INode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area");
            INode heightNode = nodeFactory.CreateNode(building, "Height");
            INode volumeNode = nodeFactory.CreateNode(building, "Volume");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            Updater updater = CreateUpdater(graph);

            INode initialNode = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_GivenNonExistingInitialNode_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = nodeFactory.CreateNode(building, "Width");
            INode lengthNode = nodeFactory.CreateNode(building, "Length");
            INode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area");
            INode heightNode = nodeFactory.CreateNode(building, "Height");
            INode volumeNode = nodeFactory.CreateNode(building, "Volume");

            INode consumption = nodeFactory.CreateNode(building, "Consumption");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            INode initialNode = consumption;
            Updater updater = CreateUpdater(graph);

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(initialNode));
        }

        #endregion

        #region UpdateCompleted

        [TestMethod]
        public void PerformUpdate_GivenSuccessfulCompleteUpdate_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void PerformUpdate_GivenSuccessfulUpdateWithInitialNode_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            //Act
            updater.PerformUpdate(building, "Width");

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void PerformUpdate_GivenSuccessfulUpdateWithInitialNode1_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            INode initialNode = graph.GetNode(building, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void PerformUpdate_GivenUnsuccessfulUpdate_PerformUpdateCompletedIsNotRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, GenericReactiveObject3> caseParameters = CreateCase12();
            IDependencyGraph graph = caseParameters.Item1;
            GenericReactiveObject3 obj = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            
            INode badNode = graph.AddNode(obj, "BadNode");
            graph.AddDependency(graph.GetNode(obj, "B"), badNode);

            bool eventRaised = false;

            updater.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception)
            {
            }

            //Assert
            Assert.IsFalse(eventRaised);
        }

        #endregion

        #region UpdateStarted

        [TestMethod]
        public void PerformUpdate_GivenCompleteUpdate_PerformUpdateStartedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateStarted += delegate
            {
                eventRaised = true;
            };

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void PerformUpdate_GivenUpdateWithInitialNode_PerformUpdateStartedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateStarted += delegate
            {
                eventRaised = true;
            };

            //Act
            updater.PerformUpdate(building, "Width");

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void PerformUpdate_GivenUpdateWithInitialNode1_PerformUpdateStartedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateStarted += delegate
            {
                eventRaised = true;
            };

            INode initialNode = graph.GetNode(building, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        #endregion

        #region UpdateFailed

        [TestMethod]
        public void PerformUpdate_GivenUnsuccessfulUpdate_UpdateFailedIsRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, GenericReactiveObject3> caseParameters = CreateCase12();
            IDependencyGraph graph = caseParameters.Item1;
            GenericReactiveObject3 obj = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            
            INode badNode = graph.AddNode(obj, "BadNode");
            graph.AddDependency(graph.GetNode(obj, "B"), badNode);

            bool eventRaised = false;
            INode failedNode = null;
            Exception thrownException = null;

            updater.UpdateFailed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
                failedNode = (sender as UpdateError).FailedNode;
                thrownException = (sender as UpdateError).SourceException;
            };

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception)
            {
            }

            //Assert
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(badNode, failedNode);
            Assert.IsInstanceOfType(thrownException, typeof(NullReferenceException));
        }

        [TestMethod]
        public void PerformUpdate_GivenSuccessfulUpdate_UpdateFailedIsNotRaised()
        {
            //Arrange
            Tuple<IDependencyGraph, GenericReactiveObject3> caseParameters = CreateCase12();
            IDependencyGraph graph = caseParameters.Item1;
            GenericReactiveObject3 obj = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            bool eventRaised = false;

            updater.UpdateFailed += delegate
            {
                eventRaised = true;
            };

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception)
            {
            }

            //Assert
            Assert.IsFalse(eventRaised);
        }

        #endregion

        #region TestCase1

        /*
         * Simple dependency graph with 8 nodes and 8 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arranged so that there would be a glitch if there was no topological order.
         */

        private Tuple<IDependencyGraph, Building00> CreateTestCase1()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            INode widthNode = nodeFactory.CreateNode(building, "Width");
            INode lengthNode = nodeFactory.CreateNode(building, "Length");
            INode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area");
            INode heightNode = nodeFactory.CreateNode(building, "Height");
            INode volumeNode = nodeFactory.CreateNode(building, "Volume", "Update_Volume");
            INode consumptionNode = nodeFactory.CreateNode(building, "Consumption");
            INode totalConsumptionNode = nodeFactory.CreateNode(building, "TotalConsumption", "Update_TotalConsumption");
            INode totalConsumptionPer_m3 = nodeFactory.CreateNode(building, "TotalConsumptionPer_m3", "Update_TotalConsumptionPer_m3");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(areaNode, totalConsumptionNode);
            graph.AddDependency(heightNode, volumeNode);
            graph.AddDependency(consumptionNode, totalConsumptionNode);
            graph.AddDependency(totalConsumptionNode, totalConsumptionPer_m3);
            graph.AddDependency(volumeNode, totalConsumptionPer_m3);

            Tuple<IDependencyGraph, Building00> caseParameters = new Tuple<IDependencyGraph, Building00>(graph, building);

            return caseParameters;
        }

        private Updater CreateUpdater(IDependencyGraph graph)
        {
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            return new Updater(graph, scheduler);
        }

        private NodeLog CreateExpectedLogger_Case1_GivenNoInitialNode(IDependencyGraph graph, Building00 building)
        {
            NodeLog expectedUpdateLog = new NodeLog();

            expectedUpdateLog.Log(graph.GetNode(building, "Consumption"));
            expectedUpdateLog.Log(graph.GetNode(building, "Height"));
            expectedUpdateLog.Log(graph.GetNode(building, "Length"));
            expectedUpdateLog.Log(graph.GetNode(building, "Width"));
            expectedUpdateLog.Log(graph.GetNode(building, "Area"));
            expectedUpdateLog.Log(graph.GetNode(building, "TotalConsumption"));
            expectedUpdateLog.Log(graph.GetNode(building, "Volume"));
            expectedUpdateLog.Log(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return expectedUpdateLog;
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenValidObjectAndMemberName_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate(building, "Width");

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(IDependencyGraph graph, Building00 building00)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building00, "Area"));
            logger.Log(graph.GetNode(building00, "TotalConsumption"));
            logger.Log(graph.GetNode(building00, "Volume"));
            logger.Log(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLog = updater.NodeLog;
            NodeLog expectedLog = CreateExpectedLogger_Case1_GivenNoInitialNode(caseParameters.Item1, caseParameters.Item2); 

            Assert.AreEqual(expectedLog, actualLog);
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenNullObject_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(null, "Width"));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenEmptyMemberName_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(building, ""));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenInvalidMemberName_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(building, "WidthInv"));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenInvalidObject_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => updater.PerformUpdate(new Building00(), "Width"));
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenLengthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Length");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case1_GivenHeightAsInitialNode(IDependencyGraph graph, Building00 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Volume"));
            logger.Log(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenHeightAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Height");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_GivenHeightAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(IDependencyGraph graph, Building00 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "TotalConsumption"));
            logger.Log(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Consumption");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case1_AreaAsInitialNode(IDependencyGraph graph, Building00 building00)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building00, "TotalConsumption"));
            logger.Log(graph.GetNode(building00, "Volume"));
            logger.Log(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Area");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case1_AreaAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenTotalConsumptionPer_m3AsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "TotalConsumptionPer_m3");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = new NodeLog();

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case1_ChangingLength_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Building00> caseParameters = CreateTestCase1();
            IDependencyGraph graph = caseParameters.Item1;
            Building00 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Width");
            updater.PerformUpdate(initialNode);

            building.Length = 10;
            initialNode = graph.GetNode(building, "Length");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 100 && building.Volume == 400 && building.TotalConsumption == 2000 && building.TotalConsumptionPer_m3 == 5);
        }

        #endregion

        #region TestCase2

        /*
         * Simple dependency graph with nodes from three objects of two different classes.
         */

        private Tuple<IDependencyGraph, Apartment01> CreateTestCase2()
        {
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };
            apartment.Basement = new AdditionalPart01 { Name = "Basement" };
            apartment.Balcony = new AdditionalPart01 { Name = "Balcony" };

            apartment.Width = 10;
            apartment.Length = 7;
            apartment.Height = 2.5;
            apartment.Consumption = 6;

            apartment.Basement.Width = 4;
            apartment.Basement.Length = 2;
            apartment.Basement.UtilCoeff = 0.6;

            apartment.Balcony.Width = 1;
            apartment.Balcony.Length = 3;
            apartment.Balcony.UtilCoeff = 0.5;

            graph.AddDependency(apartment.Balcony, "Width", apartment.Balcony, "Area");
            graph.AddDependency(apartment.Balcony, "Length", apartment.Balcony, "Area");

            graph.AddDependency(apartment.Basement, "Width", apartment.Basement, "Area");
            graph.AddDependency(apartment.Basement, "Length", apartment.Basement, "Area");

            graph.AddDependency(apartment, "Width", apartment, "HeatedArea");
            graph.AddDependency(apartment, "Length", apartment, "HeatedArea");

            graph.AddDependency(apartment, "Height", apartment, "HeatedVolume");
            graph.AddDependency(apartment, "HeatedArea", apartment, "HeatedVolume");

            graph.AddDependency(apartment, "Consumption", apartment, "TotalConsumption");
            graph.AddDependency(apartment, "HeatedVolume", apartment, "TotalConsumption");

            INode totalAreaNode = graph.AddNode(apartment, "TotalArea");
            INode balconyUtilCoeffNode = graph.AddNode(apartment.Balcony, "UtilCoeff");
            INode basementUtilCoeffNode = graph.AddNode(apartment.Basement, "UtilCoeff");

            graph.AddDependency(apartment, "HeatedArea", apartment, "TotalArea");
            graph.AddDependency(apartment.Balcony, "Area", apartment, "TotalArea");
            graph.AddDependency(apartment.Basement, "Area", apartment, "TotalArea");
            graph.AddDependency(apartment.Balcony, "UtilCoeff", apartment, "TotalArea");
            graph.AddDependency(apartment.Basement, "UtilCoeff", apartment, "TotalArea");

            return new Tuple<IDependencyGraph, Apartment01>(graph, apartment);
        }

        private NodeLog CreateExpectedLogger_Case2_GivenNoInitialNode(IDependencyGraph graph, Apartment01 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment.Basement, "UtilCoeff"));
            logger.Log(graph.GetNode(apartment.Balcony, "UtilCoeff"));
            logger.Log(graph.GetNode(apartment, "Consumption"));
            logger.Log(graph.GetNode(apartment, "Height"));
            logger.Log(graph.GetNode(apartment, "Length"));
            logger.Log(graph.GetNode(apartment, "Width"));
            logger.Log(graph.GetNode(apartment, "HeatedArea"));
            logger.Log(graph.GetNode(apartment, "HeatedVolume"));
            logger.Log(graph.GetNode(apartment, "TotalConsumption"));
            logger.Log(graph.GetNode(apartment.Basement, "Length"));
            logger.Log(graph.GetNode(apartment.Basement, "Width"));
            logger.Log(graph.GetNode(apartment.Basement, "Area"));
            logger.Log(graph.GetNode(apartment.Balcony, "Length"));
            logger.Log(graph.GetNode(apartment.Balcony, "Width"));
            logger.Log(graph.GetNode(apartment.Balcony, "Area"));
            logger.Log(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment01> caseParameters = CreateTestCase2();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment01 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case2_GivenNoInitialNode(graph, apartment);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment01> caseParameters = CreateTestCase2();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment01 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 &&
                apartment.Balcony.Area == 3 &&
                apartment.HeatedArea == 70 &&
                apartment.TotalArea == 76.3 &&
                apartment.HeatedVolume == 175 &&
                apartment.TotalConsumption == 1050
                );
        }

        private NodeLog CreateExpectedLogger_Case2_GivenWidthAsInitialNode(IDependencyGraph graph, Apartment01 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment, "HeatedArea"));
            logger.Log(graph.GetNode(apartment, "TotalArea"));
            logger.Log(graph.GetNode(apartment, "HeatedVolume"));
            logger.Log(graph.GetNode(apartment, "TotalConsumption"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenApartmentWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment01> caseParameters = CreateTestCase2();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment01 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(apartment, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case2_GivenWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        private NodeLog CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(IDependencyGraph graph, Apartment01 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment.Balcony, "Area"));
            logger.Log(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenBalconyWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment01> caseParameters = CreateTestCase2();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment01 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(apartment.Balcony, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case2_ChangingApartmentLength_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment01> caseParameters = CreateTestCase2();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment01 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            updater.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(apartment, "Length");
            apartment.Length = 8;
            updater.PerformUpdate(initialNode);

            //Assert
            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 &&
                apartment.Balcony.Area == 3 &&
                apartment.HeatedArea == 80 &&
                apartment.TotalArea == 86.3 &&
                apartment.HeatedVolume == 200 &&
                apartment.TotalConsumption == 1200
                );
        }

        #endregion

        #region TestCase3

        /*
         * Simple dependency graph with 4 method nodes and 4 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arranged so that there would be a glitch if there was no topological order.
         */

        private Tuple<IDependencyGraph, Building02> CreateCase3()
        {
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            INode updateAreaNode = nodeFactory.CreateNode(building, "Update_Area");
            INode updateVolumeNode = nodeFactory.CreateNode(building, "Update_Volume");
            INode updateTotalConsumptionNode = nodeFactory.CreateNode(building, "Update_TotalConsumption");
            INode updateTotalConsumptionPer_m3Node = nodeFactory.CreateNode(building, "Update_TotalConsumptionPer_m3");

            graph.AddDependency(updateAreaNode, updateVolumeNode);
            graph.AddDependency(updateAreaNode, updateTotalConsumptionNode);
            graph.AddDependency(updateTotalConsumptionNode, updateTotalConsumptionPer_m3Node);
            graph.AddDependency(updateVolumeNode, updateTotalConsumptionPer_m3Node);

            return new Tuple<IDependencyGraph, Building02>(graph, building);
        }

        private NodeLog CreateExpectedLogger_Case3_GivenUpdateAreaAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_Area"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case3_GivenUpdateAreaAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case3_GivenUpdateVolumeAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateVolumeAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_Volume");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case3_GivenUpdateVolumeAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateTotalConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_TotalConsumption");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_TotalConsumptionPer_m3");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case3_GivenNoInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_Area"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case3_GivenNoInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateAreaAsInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case3_ChangingLength_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase3();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            updater.PerformUpdate(initialNode, false);
            building.Length = 10;

            updater.PerformUpdate(initialNode, false);

            //Assert
            Assert.IsTrue(building.Area == 100 && building.Volume == 400 && building.TotalConsumption == 2000 && building.TotalConsumptionPer_m3 == 5);
        }

        #endregion

        #region TestCase4

        /*
         * Simple dependency graph with method nodes from three objects of two different classes.
         */

        private Tuple<IDependencyGraph, Apartment04> CreateCase4()
        {
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            apartment.Basement = new AdditionalPart04 { Name = "Basement" };
            apartment.Balcony = new AdditionalPart04 { Name = "Balcony" };

            apartment.Width = 10;
            apartment.Length = 7;
            apartment.Height = 2.5;
            apartment.Consumption = 6;

            apartment.Basement.Width = 4;
            apartment.Basement.Length = 2;
            apartment.Basement.UtilCoeff = 0.6;

            apartment.Balcony.Width = 1;
            apartment.Balcony.Length = 3;
            apartment.Balcony.UtilCoeff = 0.5;

            graph.AddDependency(apartment, "Update_HeatedArea", apartment, "Update_HeatedVolume");
            graph.AddDependency(apartment, "Update_HeatedArea", apartment, "Update_TotalArea");
            graph.AddDependency(apartment, "Update_HeatedVolume", apartment, "Update_TotalConsumption");

            graph.AddDependency(apartment.Balcony, "Update_Area", apartment.Balcony, "Update_UtilityArea");
            graph.AddDependency(apartment.Balcony, "Update_UtilityArea", apartment, "Update_TotalArea");

            graph.AddDependency(apartment.Basement, "Update_Area", apartment.Basement, "Update_UtilityArea");
            graph.AddDependency(apartment.Basement, "Update_UtilityArea", apartment, "Update_TotalArea");

            return new Tuple<IDependencyGraph, Apartment04>(graph, apartment);
        }

        private NodeLog CreateExpectedLogger_Case4_GivenNoInitialNode(IDependencyGraph graph, Apartment04 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment.Basement, "Update_Area"));
            logger.Log(graph.GetNode(apartment.Basement, "Update_UtilityArea"));
            logger.Log(graph.GetNode(apartment.Balcony, "Update_Area"));
            logger.Log(graph.GetNode(apartment.Balcony, "Update_UtilityArea"));
            logger.Log(graph.GetNode(apartment, "Update_HeatedArea"));
            logger.Log(graph.GetNode(apartment, "Update_TotalArea"));
            logger.Log(graph.GetNode(apartment, "Update_HeatedVolume"));
            logger.Log(graph.GetNode(apartment, "Update_TotalConsumption"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment04> caseParameters = CreateCase4();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment04 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case4_GivenNoInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment04> caseParameters = CreateCase4();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment04 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 && apartment.Basement.UtilityArea == 4.8 &&
                apartment.Balcony.Area == 3 && apartment.Balcony.UtilityArea == 1.5 &&
                apartment.HeatedArea == 70 &&
                apartment.TotalArea == 76.3 &&
                apartment.HeatedVolume == 175 &&
                apartment.TotalConsumption == 1050
                );
        }

        private NodeLog CreateExpectedLogger_Case4_GivenBalconyUpdateAreaAsInitialNode(IDependencyGraph graph, Apartment04 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment.Balcony, "Update_Area"));
            logger.Log(graph.GetNode(apartment.Balcony, "Update_UtilityArea"));
            logger.Log(graph.GetNode(apartment, "Update_TotalArea"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenBalconyUpdateAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment04> caseParameters = CreateCase4();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment04 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(apartment.Balcony, "Update_Area");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case4_GivenBalconyUpdateAreaAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        private NodeLog CreateExpectedLogger_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode(IDependencyGraph graph, Apartment04 apartment)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(apartment, "Update_HeatedArea"));
            logger.Log(graph.GetNode(apartment, "Update_TotalArea"));
            logger.Log(graph.GetNode(apartment, "Update_HeatedVolume"));
            logger.Log(graph.GetNode(apartment, "Update_TotalConsumption"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment04> caseParameters = CreateCase4();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment04 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(apartment, "Update_HeatedArea");

            //Act
            updater.PerformUpdate(initialNode, false);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case4_ChangingBalconyLength_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Apartment04> caseParameters = CreateCase4();
            IDependencyGraph graph = caseParameters.Item1;
            Apartment04 apartment = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            updater.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(apartment.Balcony, "Update_Area");
            apartment.Balcony.Length = 4;
            updater.PerformUpdate(initialNode, false);

            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 && apartment.Basement.UtilityArea == 4.8 &&
                apartment.Balcony.Area == 4 && apartment.Balcony.UtilityArea == 2 &&
                apartment.HeatedArea == 70 &&
                apartment.TotalArea == 76.8 &&
                apartment.HeatedVolume == 175 &&
                apartment.TotalConsumption == 1050
                );
        }

        #endregion

        #region TestCase5

        /*
         * Simple dependency graph with mixed method and property nodes from same object.
         */

        private Tuple<IDependencyGraph, Building02> CreateCase5()
        {
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02 { Name = "Building 02" };

            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            PropertyNode widthNode = graph.AddNode(building, "Width") as PropertyNode;
            PropertyNode lengthNode = graph.AddNode(building, "Length") as PropertyNode;
            PropertyNode heightNode = graph.AddNode(building, "Height") as PropertyNode;
            PropertyNode consumptionNode = graph.AddNode(building, "Consumption") as PropertyNode;
            PropertyNode areaNode = graph.AddNode(building, "Area") as PropertyNode;

            MethodNode updateVolumeNode = graph.AddNode(building, "Update_Volume") as MethodNode;
            MethodNode updateTotalConsumptionNode = graph.AddNode(building, "Update_TotalConsumption") as MethodNode;
            MethodNode updateTotalConsumptionPer_m3Node = graph.AddNode(building, "Update_TotalConsumptionPer_m3") as MethodNode;

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(heightNode, updateVolumeNode);
            graph.AddDependency(consumptionNode, updateTotalConsumptionNode);
            graph.AddDependency(areaNode, updateVolumeNode);
            graph.AddDependency(areaNode, updateTotalConsumptionNode);
            graph.AddDependency(updateTotalConsumptionNode, updateTotalConsumptionPer_m3Node);
            graph.AddDependency(updateVolumeNode, updateTotalConsumptionPer_m3Node);

            return new Tuple<IDependencyGraph, Building02>(graph, building);
        }

        private NodeLog CreateExpectedLogger_Case5_GivenNoInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Consumption"));
            logger.Log(graph.GetNode(building, "Height"));
            logger.Log(graph.GetNode(building, "Length"));
            logger.Log(graph.GetNode(building, "Width"));
            logger.Log(graph.GetNode(building, "Area"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case5_GivenNoInitialNode(graph, building);

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        private NodeLog CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Area"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenLengthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Length");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case5_GivenHeightAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_Volume"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenHeightAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Height");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case5_GivenHeightAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case5_GivenConsumptionAsInitialNode(IDependencyGraph graph, Building02 building)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(building, "Update_TotalConsumption"));
            logger.Log(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Building02> caseParameters = CreateCase5();
            IDependencyGraph graph = caseParameters.Item1;
            Building02 building = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(building, "Consumption");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case5_GivenConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        #endregion

        #region TestCase6

        /*Simple graph with cycles */

        private Tuple<IDependencyGraph, Cycle> CreateTestCase6()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            INode aNode = graph.AddNode(cycle, "A");
            INode bNode = graph.AddNode(cycle, "B");
            INode cNode = graph.AddNode(cycle, "C");
            INode dNode = graph.AddNode(cycle, "D");
            INode eNode = graph.AddNode(cycle, "E");
            INode fNode = graph.AddNode(cycle, "F");

            graph.AddDependency(aNode, bNode);
            graph.AddDependency(aNode, cNode);
            graph.AddDependency(bNode, dNode);
            graph.AddDependency(cNode, bNode);
            graph.AddDependency(cNode, eNode);
            graph.AddDependency(dNode, cNode);
            graph.AddDependency(dNode, fNode);
            graph.AddDependency(eNode, fNode);

            return new Tuple<IDependencyGraph, Cycle>(graph, cycle);
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenNoInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => updater.PerformUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenANodeAsInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(cycle, "A");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => updater.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenBNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(cycle, "B");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => updater.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenCNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(cycle, "C");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => updater.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenDNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(cycle, "D");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => updater.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenENodeAsInitialNode_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Cycle> caseParameters = CreateTestCase6();
            IDependencyGraph graph = caseParameters.Item1;
            Cycle cycle = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            INode initialNode = graph.GetNode(cycle, "E");

            //Act
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = new NodeLog();
            expectedLogger.Log(graph.GetNode(cycle, "F"));

            Assert.AreEqual(actualLogger, expectedLogger);
        }

        #endregion

        #region TestCase7

        /*Simple Graph with Collection Node as a predecessor*/

        private Tuple<IDependencyGraph, Whole> CreateCase7()
        {
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();

            INode a = graph.AddNode(whole, "A");
            INode b = graph.AddNode(whole, "B");
            INode c = graph.AddNode(whole, "C");

            INode partsA = graph.AddNode(whole.Parts, "A");
            INode partsB = graph.AddNode(whole.Parts, "B");
            INode partsC = graph.AddNode(whole.Parts, "C");

            graph.AddDependency(partsA, a);
            graph.AddDependency(partsB, b);
            graph.AddDependency(partsC, c);

            return new Tuple<IDependencyGraph, Whole>(graph, whole);
        }

        private NodeLog CreateExpectedLogger_Case7_GivenNoInitialNode(IDependencyGraph graph, Whole whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole.Parts, "C"));
            logger.Log(graph.GetNode(whole.Parts, "B"));
            logger.Log(graph.GetNode(whole.Parts, "A"));

            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole> caseParameters = CreateCase7();
            IDependencyGraph graph = caseParameters.Item1;
            Whole whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            
            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_GivenNoInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole> caseParameters = CreateCase7();
            IDependencyGraph graph = caseParameters.Item1;
            Whole whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 15 && whole.C == 18);
        }

        private NodeLog CreateExpectedLogger_Case7_GivenAAsInitialNode(IDependencyGraph graph, Whole whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenAAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole> caseParameters = CreateCase7();
            IDependencyGraph graph = caseParameters.Item1;
            Whole whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            INode initialNode = graph.GetNode(whole.Parts, "A");
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_GivenAAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case7_GivenBAsInitialNode(IDependencyGraph graph, Whole whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenBAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole> caseParameters = CreateCase7();
            IDependencyGraph graph = caseParameters.Item1;
            Whole whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            INode initialNode = graph.GetNode(whole.Parts, "B");
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_GivenBAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case7_ChangingA_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole> caseParameters = CreateCase7();
            IDependencyGraph graph = caseParameters.Item1;
            Whole whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            updater.PerformUpdate();
            INode initialNode = graph.GetNode(whole.Parts, "A");

            //Act
            whole.Parts[0].A = 8;
            updater.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(whole.A == 19 && whole.B == 15 && whole.C == 18);
        }

        #endregion

        #region TestCase7_1

        /*Simple graph with Collection node as a successor */

        private Tuple<IDependencyGraph, Whole2> CreateCase7_1()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole = new Whole2();

            INode wholeCoeffA = nodeFactory.CreateNode(whole, "CoeffA");
            INode wholeCoeffB = nodeFactory.CreateNode(whole, "CoeffB");
            INode wholeCoeffC = nodeFactory.CreateNode(whole, "CoeffC");

            INode partsA = nodeFactory.CreateNode(whole.Parts, "A", "Update_A");
            INode partsB = nodeFactory.CreateNode(whole.Parts, "B", "Update_B");
            INode partsC = nodeFactory.CreateNode(whole.Parts, "C", "Update_C");

            graph.AddDependency(wholeCoeffA, partsA);
            graph.AddDependency(wholeCoeffB, partsB);
            graph.AddDependency(wholeCoeffC, partsC);

            return new Tuple<IDependencyGraph, Whole2>(graph, whole);
        }

        private NodeLog CreateExpectedLogger_Case7_1_GivenNoInitialNode(IDependencyGraph graph, Whole2 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "CoeffC"));
            logger.Log(graph.GetNode(whole.Parts, "C"));
            logger.Log(graph.GetNode(whole, "CoeffB"));
            logger.Log(graph.GetNode(whole.Parts, "B"));
            logger.Log(graph.GetNode(whole, "CoeffA"));
            logger.Log(graph.GetNode(whole.Parts, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole2> caseParameters = CreateCase7_1();
            IDependencyGraph graph = caseParameters.Item1;
            Whole2 whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_1_GivenNoInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole2> caseParameters = CreateCase7_1();
            IDependencyGraph graph = caseParameters.Item1;
            Whole2 whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            Part2 p1 = whole.Parts[0];
            Part2 p2 = whole.Parts[1];
            Part2 p3 = whole.Parts[2];
            Assert.IsTrue(p1.A == 2 && p1.B == 4 && p1.C == 6
                && p2.A == 4 && p2.B == 8 && p2.C == 12
                && p3.A == 6 && p3.B == 12 && p3.C == 18);
        }

        private NodeLog CreateExpectedLogger_Case7_1_GivenCoeffAAsInitialNode(IDependencyGraph graph, Whole2 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole.Parts, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenCoeffAAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole2> caseParameters = CreateCase7_1();
            IDependencyGraph graph = caseParameters.Item1;
            Whole2 whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffA");
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_1_GivenCoeffAAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenCoeffBAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole2> caseParameters = CreateCase7_1();
            IDependencyGraph graph = caseParameters.Item1;
            Whole2 whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffB");
            updater.PerformUpdate(initialNode);

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case7_1_GivenCoeffBAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case7_1_GivenCoeffBAsInitialNode(IDependencyGraph graph, Whole2 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole.Parts, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_ChangingCoeffA_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Whole2> caseParameters = CreateCase7_1();
            IDependencyGraph graph = caseParameters.Item1;
            Whole2 whole = caseParameters.Item2;
            Updater updater = CreateUpdater(graph);
            updater.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffA");
            whole.CoeffA = 3;
            updater.PerformUpdate(initialNode);

            //Assert
            Part2 p1 = whole.Parts[0];
            Part2 p2 = whole.Parts[1];
            Part2 p3 = whole.Parts[2];
            Assert.IsTrue(p1.A == 3 && p1.B == 4 && p1.C == 6
                && p2.A == 6 && p2.B == 8 && p2.C == 12
                && p3.A == 9 && p3.B == 12 && p3.C == 18);
        }

        #endregion

        #region TestCase8_1

        /*Simple dependency graph with property node triggering within classes*/

        private Tuple<IDependencyGraph, Updater, Whole_8_1> CreateCase_8_1()
        {
            var graph = new DependencyGraph("GRAPH_CASE_8_1");
            Updater updater = CreateUpdater(graph);
            updater.SuspendUpdate();
            var whole = new Whole_8_1(updater);

            INode coeffA = graph.AddNode(whole, "CoeffA");
            INode coeffB = graph.AddNode(whole, "CoeffB");
            INode coeffC = graph.AddNode(whole, "CoeffC");
            INode a = graph.AddNode(whole, "A");
            INode b = graph.AddNode(whole, "B");
            INode c = graph.AddNode(whole, "C");
            INode d = graph.AddNode(whole, "D");

            graph.AddDependency(coeffA, a);
            graph.AddDependency(coeffB, b);
            graph.AddDependency(coeffC, c);

            graph.AddDependency(a, b);
            graph.AddDependency(b, c);
            graph.AddDependency(b, d);
            graph.AddDependency(c, d);

            INode partsA = graph.AddNode(whole.Parts, "A");
            INode partsB = graph.AddNode(whole.Parts, "B");
            INode partsC = graph.AddNode(whole.Parts, "C");

            graph.AddDependency(partsA, a);
            graph.AddDependency(partsB, b);
            graph.AddDependency(partsC, c);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Whole_8_1>(graph, updater, whole);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenNoInitialNode(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole.Parts, "C"));
            logger.Log(graph.GetNode(whole.Parts, "B"));
            logger.Log(graph.GetNode(whole.Parts, "A"));

            logger.Log(graph.GetNode(whole, "CoeffC"));
            logger.Log(graph.GetNode(whole, "CoeffB"));
            logger.Log(graph.GetNode(whole, "CoeffA"));


            logger.Log(graph.GetNode(whole, "A"));
            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            Tuple <IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenNoInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 42 && whole.C == 96 && whole.D == 138);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenCoeffAIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "A"));
            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffAIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;

            //Act
            whole.CoeffA = 2;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenCoeffAIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffAIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            whole.CoeffA = 2;

            //Assert
            Assert.IsTrue(whole.A == 24 && whole.B == 54 && whole.C == 108 && whole.D == 162);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenCoeffBIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffBIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;

            //Act
            whole.CoeffB = 3;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenCoeffBIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffBIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            whole.CoeffB = 3;

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 57 && whole.C == 111 && whole.D == 168);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenCoeffCIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffCIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;

            //Act
            whole.CoeffC = 4;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenCoeffCIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCoeffCIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            whole.CoeffC = 4;

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 42 && whole.C == 114 && whole.D == 156);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenAIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "A"));
            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenAIsTriggered_SchedulesCorrectUpdateOrder()
        {
            ///Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.A = 2;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenAIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenAIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.A = 2;

            //Assert
            Assert.IsTrue(whole.A == 13 && whole.B == 43 && whole.C == 97 && whole.D == 140);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenBIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "B"));
            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenBIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.B = 3;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenBIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenBIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.B = 3;

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 44 && whole.C == 98 && whole.D == 142);
        }

        private NodeLog CreateExpectedLogger_Case_8_1_GivenCIsTriggered(IDependencyGraph graph, Whole_8_1 whole)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(whole, "C"));
            logger.Log(graph.GetNode(whole, "D"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCIsTriggered_SchedulesCorrectUpdateOrder()
        {
            ///Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.C = 4;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_1_GivenCIsTriggered(graph, whole);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_1_GivenCIsTriggered_GivesCorrectResults()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, Whole_8_1> caseParameters = CreateCase_8_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var whole = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            Part_8_1 p = whole.Parts[0];
            p.C = 4;

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 42 && whole.C == 99 && whole.D == 141);
        }

        #endregion

        #region TestCase9

        /*Simple dependency graph with method node triggering within classes*/

        private Tuple<IDependencyGraph, Updater, GenericReactiveObject2> CreateCase_9()
        {
            var graph = new DependencyGraph("GRAPH_CASE_9");
            var obj = new GenericReactiveObject2();
            Updater updater = CreateUpdater(graph);
            updater.SuspendUpdate();
            obj.Updater = updater;

            INode updateA = graph.AddNode(obj, "Update_A");
            INode updateB = graph.AddNode(obj, "Update_B");
            INode updateC = graph.AddNode(obj, "Update_C");
            INode updateD = graph.AddNode(obj, "Update_D");
            INode updateE = graph.AddNode(obj, "Update_E");
            INode updateF = graph.AddNode(obj, "Update_F");
            INode updateG = graph.AddNode(obj, "Update_G");
            INode updateH = graph.AddNode(obj, "Update_H");
            INode updateI = graph.AddNode(obj, "Update_I");

            graph.AddDependency(updateA, updateB);
            graph.AddDependency(updateB, updateC);
            graph.AddDependency(updateC, updateD);
            graph.AddDependency(updateD, updateE);

            graph.AddDependency(updateF, updateG);
            graph.AddDependency(updateG, updateH);
            graph.AddDependency(updateH, updateI);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, GenericReactiveObject2>(graph, updater, obj);
        }

        private NodeLog CreateExpectedLogger_Case_9_GivenNoInitialNode(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "Update_F"));
            logger.Log(graph.GetNode(obj, "Update_G"));
            logger.Log(graph.GetNode(obj, "Update_H"));
            logger.Log(graph.GetNode(obj, "Update_I"));

            logger.Log(graph.GetNode(obj, "Update_A"));
            logger.Log(graph.GetNode(obj, "Update_B"));
            logger.Log(graph.GetNode(obj, "Update_C"));
            logger.Log(graph.GetNode(obj, "Update_D"));
            logger.Log(graph.GetNode(obj, "Update_E"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_9_GivenNoInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_9();
            var graph = caseParameters.Item1;
            Updater updater = caseParameters.Item2;
            GenericReactiveObject2 obj = caseParameters.Item3;
            
            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_9_GivenNoInitialNode(graph, obj);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case_9_GivenNodeUpdateAIsTriggered(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "Update_B"));
            logger.Log(graph.GetNode(obj, "Update_C"));
            logger.Log(graph.GetNode(obj, "Update_D"));
            logger.Log(graph.GetNode(obj, "Update_E"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_9_GivenNodeUpdateAIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_9();
            var graph = caseParameters.Item1;
            Updater updater = caseParameters.Item2;
            GenericReactiveObject2 obj = caseParameters.Item3;

            //Act
            obj.Update_A();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_9_GivenNodeUpdateAIsTriggered(graph, obj);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case_9_GivenNodeUpdateFIsTriggered(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "Update_G"));
            logger.Log(graph.GetNode(obj, "Update_H"));
            logger.Log(graph.GetNode(obj, "Update_I"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_9_GivenNodeUpdateFIsTriggered_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_9();
            var graph = caseParameters.Item1;
            Updater updater = caseParameters.Item2;
            GenericReactiveObject2 obj = caseParameters.Item3;

            //Act
            obj.Update_F();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_9_GivenNodeUpdateFIsTriggered(graph, obj);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        #endregion

        #region TestCase10

        /*Demonstration of exception happening during performing update process*/

        private Tuple<IDependencyGraph, Updater, GenericReactiveObject2> CreateCase_10()
        {
            var graph = new DependencyGraph("GRAPH_CASE_10");
            var obj = new GenericReactiveObject2();
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();
            obj.Updater = updater;

            INode updateA = graph.AddNode(obj, "Update_A");
            INode updateB = graph.AddNode(obj, "Update_B");
            INode updateC = graph.AddNode(obj, "Update_C");
            INode updateD = graph.AddNode(obj, "Update_D");
            INode updateE = graph.AddNode(obj, "Update_E");
            INode updateF = graph.AddNode(obj, "Update_F");
            INode updateG = graph.AddNode(obj, "Update_G");
            INode updateH = graph.AddNode(obj, "Update_H");
            INode updateI = graph.AddNode(obj, "Update_I");
            INode updateJ = graph.AddNode(obj, "Update_J");

            graph.AddDependency(updateA, updateB);
            graph.AddDependency(updateB, updateC);
            graph.AddDependency(updateC, updateD);
            graph.AddDependency(updateD, updateE);

            graph.AddDependency(updateF, updateG);
            graph.AddDependency(updateG, updateH);
            graph.AddDependency(updateH, updateI);
            graph.AddDependency(updateI, updateJ);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, GenericReactiveObject2>(graph, updater, obj);
        }

        [TestMethod]
        public void PerformUpdate_Case_10_GivenExceptionInClientCodeDuringUpdateProcess_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            //Act&Assert
            Assert.ThrowsException<GraphUpdateException>(() => updater.PerformUpdate());
        }

        private NodeLog CreateExpectedNodesForUpdateLogger_Case_10_GivenNoInitialNode(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "Update_F"));
            logger.Log(graph.GetNode(obj, "Update_G"));
            logger.Log(graph.GetNode(obj, "Update_H"));
            logger.Log(graph.GetNode(obj, "Update_I"));
            logger.Log(graph.GetNode(obj, "Update_J"));

            logger.Log(graph.GetNode(obj, "Update_A"));
            logger.Log(graph.GetNode(obj, "Update_B"));
            logger.Log(graph.GetNode(obj, "Update_C"));
            logger.Log(graph.GetNode(obj, "Update_D"));
            logger.Log(graph.GetNode(obj, "Update_E"));

            return logger;
        }

        private NodeLog CreateExpectedUpdatedNodesLogger_Case_10_GivenNoInitialNode(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "Update_F"));
            logger.Log(graph.GetNode(obj, "Update_G"));
            logger.Log(graph.GetNode(obj, "Update_H"));
            logger.Log(graph.GetNode(obj, "Update_I"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_10_GivenExceptionInClientCodeDuringUpdateProcess_UpdateProcessIsAborted()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception) { }

            //Assert
            NodeLog nodesForUpdateLogger = CreateExpectedNodesForUpdateLogger_Case_10_GivenNoInitialNode(graph, obj);
            NodeLog updatedNodesLogger = CreateExpectedUpdatedNodesLogger_Case_10_GivenNoInitialNode(graph, obj);

            ILoggable scheduler = updater.Scheduler as ILoggable;
            NodeLog actualNodesForUpdate = scheduler.NodeLog;
            NodeLog actualUpdatedNodes = updater.NodeLog;

            Assert.IsTrue(nodesForUpdateLogger.Equals(actualNodesForUpdate) && updatedNodesLogger.Equals(actualUpdatedNodes));
        }

        [TestMethod]
        public void PerformUpdate_Case_10_GivenExceptionInClientCodeDuringUpdateProcess_ProvidesNodeWhichCausedException()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            INode errorNode = null;
            INode jNode = graph.GetNode(obj, "Update_J");

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (GraphUpdateException e)
            {
                errorNode = e.ErrorData.FailedNode as INode;
            }

            //Assert
            Assert.AreEqual(errorNode, jNode);
        }

        #endregion

        #region TestCase10_1

        /*Demonstration of exception happening during performing update process*/

        private Tuple<IDependencyGraph, Updater, GenericReactiveObject2> CreateCase_10_1()
        {
            var graph = new DependencyGraph("GRAPH_CASE_10_1");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();
            var obj = new GenericReactiveObject2();
            obj.Updater = updater;

            INode nodeA = graph.AddNode(obj, "A");
            INode nodeB = graph.AddNode(obj, "B");
            INode nodeC = graph.AddNode(obj, "C");
            INode nodeD = graph.AddNode(obj, "D");
            INode nodeE = graph.AddNode(obj, "E");
            INode nodeF = graph.AddNode(obj, "F");
            INode nodeG = graph.AddNode(obj, "G");
            INode nodeH = graph.AddNode(obj, "H");
            INode nodeI = graph.AddNode(obj, "I");
            INode nodeJ = graph.AddNode(obj, "J");

            graph.AddDependency(nodeA, nodeB);
            graph.AddDependency(nodeB, nodeC);
            graph.AddDependency(nodeC, nodeD);
            graph.AddDependency(nodeD, nodeE);

            graph.AddDependency(nodeF, nodeG);
            graph.AddDependency(nodeG, nodeH);
            graph.AddDependency(nodeH, nodeI);
            graph.AddDependency(nodeI, nodeJ);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, GenericReactiveObject2>(graph, updater, obj);
        }

        [TestMethod]
        public void PerformUpdate_Case_10_1_GivenExceptionInClientCodeDuringUpdateProcess_ThrowsException()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            //Act&Assert
            Assert.ThrowsException<GraphUpdateException>(() => updater.PerformUpdate());
        }

        private NodeLog CreateExpectedNodesForUpdateLogger_Case_10_1_GivenNoInitialNode(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "F"));
            logger.Log(graph.GetNode(obj, "G"));
            logger.Log(graph.GetNode(obj, "H"));
            logger.Log(graph.GetNode(obj, "I"));
            logger.Log(graph.GetNode(obj, "J"));

            logger.Log(graph.GetNode(obj, "A"));
            logger.Log(graph.GetNode(obj, "B"));
            logger.Log(graph.GetNode(obj, "C"));
            logger.Log(graph.GetNode(obj, "D"));
            logger.Log(graph.GetNode(obj, "E"));

            return logger;
        }

        private NodeLog CreateExpectedUpdatedNodesLogger_Case_10_1_GivenNoInitialNode(IDependencyGraph graph, GenericReactiveObject2 obj)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(obj, "F"));
            logger.Log(graph.GetNode(obj, "G"));
            logger.Log(graph.GetNode(obj, "H"));
            logger.Log(graph.GetNode(obj, "I"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_10_1_GivenExceptionInClientCodeDuringUpdateProcess_UpdateProcessIsAborted()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception) { }

            //Assert
            NodeLog nodesForUpdateLogger = CreateExpectedNodesForUpdateLogger_Case_10_1_GivenNoInitialNode(graph, obj);
            NodeLog updatedNodesLogger = CreateExpectedUpdatedNodesLogger_Case_10_1_GivenNoInitialNode(graph, obj);
            ILoggable scheduler = updater.Scheduler as ILoggable;
            NodeLog actualNodesForUpdate = scheduler.NodeLog;
            NodeLog actualUpdatedNodes = updater.NodeLog;

            Assert.IsTrue(nodesForUpdateLogger.Equals(actualNodesForUpdate) && updatedNodesLogger.Equals(actualUpdatedNodes));
        }

        [TestMethod]
        public void PerformUpdate_Case_10_1_GivenExceptionInClientCodeDuringUpdateProcess_ProvidesNodeWhichCausedException()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            INode errorNode = null;
            INode jNode = graph.GetNode(obj, "J");

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (GraphUpdateException e)
            {
                errorNode = e.ErrorData.FailedNode as INode;
            }

            //Assert
            Assert.AreEqual(errorNode, jNode);
        }

        [TestMethod]
        public void PerformUpdate_Case_10_1_GivenExceptionInClientCodeDuringUpdateProcess_ProvidesSourceException()
        {
            //Arrange
            Tuple<IDependencyGraph, Updater, GenericReactiveObject2> caseParameters = CreateCase_10_1();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var obj = caseParameters.Item3;

            INode jNode = graph.GetNode(obj, "J");
            Exception ex = null;

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (GraphUpdateException e)
            {
                ex = e.ErrorData.SourceException;
            }

            //Assert
            Assert.IsInstanceOfType(ex, typeof(NotImplementedException));
        }

        #endregion

        #region TestCase11

        /*Demonstration of Garbage collector collecting weak references in dependency graph*/

        private IDependencyGraph CreateCase_11(GenericReactiveObject reactiveObject)
        {
            var graph = new DependencyGraph("GRAPH_CASE_11");
            NodeFactory nodeFactory = new StandardNodeFactory();

            INode nodeA = nodeFactory.CreateNode(reactiveObject, "A");
            INode nodeB = nodeFactory.CreateNode(reactiveObject, "B");
            INode nodeC = nodeFactory.CreateNode(reactiveObject, "C");
            INode nodeD = nodeFactory.CreateNode(reactiveObject, "D");

            graph.AddDependency(nodeA, nodeB);
            graph.AddDependency(nodeB, nodeC);
            graph.AddDependency(nodeC, nodeD);

            return graph;
        }

        [TestMethod]
        public void PerformUpdate_GivenStrongReferencesToOwnerObjectExist_GivesCorrectUpdateOrder()
        {
            //Arrange
            var obj = new GenericReactiveObject();
            var graph = CreateCase_11(obj);
            Updater updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            // Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = new NodeLog();
            expectedLogger.Log(graph.GetNode(obj, "A"));
            expectedLogger.Log(graph.GetNode(obj, "B"));
            expectedLogger.Log(graph.GetNode(obj, "C"));
            expectedLogger.Log(graph.GetNode(obj, "D"));

            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        [TestMethod]
        public void PerformUpdate_GivenStrongReferencesToOwnerObjectDoNoExist_GivesCorrectUpdateOrder()
        {
            //Arrange
            var obj = new GenericReactiveObject();
            var graph = CreateCase_11(obj);
            Updater updater = CreateUpdater(graph);

            obj = null;
            GC.Collect();

            //Act
            updater.PerformUpdate();

            // Assert
            NodeLog expectedLogger = new NodeLog();
            NodeLog actualLogger = updater.NodeLog;
            Assert.IsTrue(expectedLogger.Equals(actualLogger));
        }

        #endregion

        #region TestCase12

        /*Demonstrate collecting information on update process*/

        private Tuple<IDependencyGraph, GenericReactiveObject3> CreateCase12()
        {
            IDependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

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

            return new Tuple<IDependencyGraph, GenericReactiveObject3>(graph, obj);
        }

        [TestMethod]
        public void PerformUpdate_GivenSuccessfulUpdate_ProvidesUpdateInfo()
        {
            //Arrange
            Tuple<IDependencyGraph, GenericReactiveObject3> caseParameters = CreateCase12();
            var graph = caseParameters.Item1;
            var obj = caseParameters.Item2;
            var updater = CreateUpdater(graph);

            //Act
            updater.PerformUpdate();

            //Assert
            UpdateInfo info = updater.LatestUpdateInfo;
            Assert.IsNotNull(info, "Info-null failed");
            Assert.IsTrue(info.UpdateStartedAt != DateTime.MinValue, "UpdateStartedAt failed");
            Assert.IsTrue(info.UpdateEndedAt != DateTime.MinValue, "UpdateEndedAt failed");
            //Assert.IsTrue(info.UpdateDuration > TimeSpan.Zero, $"UpdateDuration failed. UpdateDuration = {info.UpdateDuration}");
            Assert.IsTrue(info.UpdateSuccessfull, "UpdateSuccessfull failed");
            Assert.IsNull(info.ErrorData, "ErrorData failed");
        }

        [TestMethod]
        public void PerformUpdate_GivenUnsuccessfulUpdate_ProvidesUpdateInfo()
        {
            //Arrange
            Tuple<IDependencyGraph, GenericReactiveObject3> caseParameters = CreateCase12();
            var graph = caseParameters.Item1;
            var obj = caseParameters.Item2;
            var updater = CreateUpdater(graph);
            INode badNode = graph.AddNode(obj, "BadNode");
            graph.AddDependency(graph.GetNode(obj, "B"), badNode);

            //Act
            try
            {
                updater.PerformUpdate();
            }
            catch (Exception)
            {
            }

            //Assert
            UpdateInfo info = updater.LatestUpdateInfo;
            Assert.IsNotNull(info);
            Assert.IsTrue(info.UpdateStartedAt != DateTime.MinValue);
            Assert.IsTrue(info.UpdateEndedAt == DateTime.MinValue);
            Assert.IsTrue(info.UpdateDuration == TimeSpan.Zero);
            Assert.IsFalse(info.UpdateSuccessfull);
            Assert.IsNotNull(info.ErrorData);
            Assert.AreEqual(info.ErrorData.Graph, graph);
            Assert.AreEqual(info.ErrorData.FailedNode, badNode);
            Assert.IsInstanceOfType(info.ErrorData.SourceException, typeof(NullReferenceException));
        }

        #endregion
    }
}
