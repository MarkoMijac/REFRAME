using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;
using ReframeCoreExamples.E02;
using ReframeCoreExamples.E04;
using ReframeCoreExamples.E06;
using ReframeCore.Nodes;
using ReframeCoreExamples.E07;
using ReframeCoreExamples.E07_1;
using ReframeCore.ReactiveCollections;

namespace ReframeCoreTests
{
    public partial class DependencyGraphTests
    {
        #region PerformUpdate GENERAL

        [TestMethod]
        public void PerformUpdate_GivenNotInitializedGraph_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            PropertyNode widthNode = nodeFactory.CreateNode(building, "Width") as PropertyNode;
            PropertyNode lengthNode = nodeFactory.CreateNode(building, "Length") as PropertyNode;
            PropertyNode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;
            PropertyNode heightNode = nodeFactory.CreateNode(building, "Height") as PropertyNode;
            PropertyNode volumeNode = nodeFactory.CreateNode(building, "Volume") as PropertyNode;

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            INode initialNode = widthNode;

            //Act&Assert
            Assert.ThrowsException<DependencyGraphException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_GivenInitialNodeIsNull_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            PropertyNode widthNode = nodeFactory.CreateNode(building, "Width") as PropertyNode;
            PropertyNode lengthNode = nodeFactory.CreateNode(building, "Length") as PropertyNode;
            PropertyNode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;
            PropertyNode heightNode = nodeFactory.CreateNode(building, "Height") as PropertyNode;
            PropertyNode volumeNode = nodeFactory.CreateNode(building, "Volume") as PropertyNode;

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            graph.Initialize();

            INode initialNode = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_GivenNonExistingInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            PropertyNode widthNode = nodeFactory.CreateNode(building, "Width") as PropertyNode;
            PropertyNode lengthNode = nodeFactory.CreateNode(building, "Length") as PropertyNode;
            PropertyNode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;
            PropertyNode heightNode = nodeFactory.CreateNode(building, "Height") as PropertyNode;
            PropertyNode volumeNode = nodeFactory.CreateNode(building, "Volume") as PropertyNode;

            PropertyNode consumption = nodeFactory.CreateNode(building, "Consumption") as PropertyNode;

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            graph.Initialize();

            INode initialNode = consumption;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(initialNode));
        }

        #endregion

        #region PerformUpdate CASE 1

        /*
         * Simple dependency graph with 8 nodes and 8 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arranged so that there would be a glitch if there was no topological order.
         */

        [TestMethod]
        public void PerformUpdate1_Case1_GivenValidObjectAndMemberName_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");
            Building00 building = new Building00();
            CreateCase1(graph as DependencyGraph, building);

            //Act
            graph.PerformUpdate(building, "Width");

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph as DependencyGraph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenNullObject_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");
            Building00 building = new Building00();
            CreateCase1(graph as DependencyGraph, building);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(null, "Width"));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenEmptyMemberName_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");
            Building00 building = new Building00();
            CreateCase1(graph as DependencyGraph, building);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(building, ""));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenInvalidMemberName_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");
            Building00 building = new Building00();
            CreateCase1(graph as DependencyGraph, building);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(building, "WidthInv"));
        }

        [TestMethod]
        public void PerformUpdate1_Case1_GivenInvalidObject_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");
            Building00 building = new Building00();
            CreateCase1(graph as DependencyGraph, building);

            //Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(new Building00(), "Width"));
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenLengthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Length");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenHeightAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Height");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenHeightAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Consumption");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Area");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_AreaAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenTotalConsumptionPer_m3AsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "TotalConsumptionPer_m3");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;

            Assert.AreEqual("", actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case1_ChangingLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");
            graph.PerformUpdate(initialNode);

            building.Length = 10;
            initialNode = graph.GetNode(building, "Length");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 100 && building.Volume == 400 && building.TotalConsumption == 2000 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenNoInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private void CreateCase1(DependencyGraph graph, Building00 building)
        {
            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            PropertyNode widthNode = nodeFactory.CreateNode(building, "Width") as PropertyNode;
            PropertyNode lengthNode = nodeFactory.CreateNode(building, "Length") as PropertyNode;
            PropertyNode areaNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;
            PropertyNode heightNode = nodeFactory.CreateNode(building, "Height") as PropertyNode;
            PropertyNode volumeNode = nodeFactory.CreateNode(building, "Volume", "Update_Volume") as PropertyNode;
            PropertyNode consumptionNode = nodeFactory.CreateNode(building, "Consumption") as PropertyNode;
            PropertyNode totalConsumptionNode = nodeFactory.CreateNode(building, "TotalConsumption", "Update_TotalConsumption") as PropertyNode;
            PropertyNode totalConsumptionPer_m3 = nodeFactory.CreateNode(building, "TotalConsumptionPer_m3", "Update_TotalConsumptionPer_m3") as PropertyNode;

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(areaNode, totalConsumptionNode);
            graph.AddDependency(heightNode, volumeNode);
            graph.AddDependency(consumptionNode, totalConsumptionNode);
            graph.AddDependency(totalConsumptionNode, totalConsumptionPer_m3);
            graph.AddDependency(volumeNode, totalConsumptionPer_m3);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(DependencyGraph graph, Building00 building00)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building00, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_GivenHeightAsInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_AreaAsInitialNode(DependencyGraph graph, Building00 building00)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_GivenNoInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Consumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Height"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        #endregion

        #region PerformUpdate CASE 2

        /*
         * Simple dependency graph with nodes from three objects of two different classes.
         */

        private void CreateCase2(DependencyGraph graph, Apartment01 apartment)
        {
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

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case2_GivenNoInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "UtilCoeff"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "UtilCoeff"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Consumption"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Height"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case2_GivenWidthAsInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalConsumption"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenNoInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);

            //Act
            graph.PerformUpdate();

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

        [TestMethod]
        public void PerformUpdate_Case2_GivenApartmentWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            INode initialNode = graph.GetNode(apartment, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenBalconyWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            INode initialNode = graph.GetNode(apartment.Balcony, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_ChangingApartmentLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            graph.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(apartment, "Length");
            apartment.Length = 8;
            graph.PerformUpdate(initialNode);

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

        #region PerformUpdate CASE 3

        /*
         * Simple dependency graph with 4 method nodes and 4 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arranged so that there would be a glitch if there was no topological order.
         */

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case3_GivenUpdateAreaAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateVolumeAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_Volume");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case3_GivenUpdateVolumeAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateTotalConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_TotalConsumption");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_TotalConsumptionPer_m3");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case3_GivenNoInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case3_GivenUpdateAreaAsInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case3_ChangingLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase3(graph, building);
            INode initialNode = graph.GetNode(building, "Update_Area");

            //Act
            graph.PerformUpdate(initialNode, false);
            building.Length = 10;

            graph.PerformUpdate(initialNode, false);

            //Assert
            Assert.IsTrue(building.Area == 100 && building.Volume == 400 && building.TotalConsumption == 2000 && building.TotalConsumptionPer_m3 == 5);
        }

        private void CreateCase3(DependencyGraph graph, Building02 building)
        {
            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            MethodNode updateAreaNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode updateVolumeNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode updateTotalConsumptionNode = nodeFactory.CreateNode(building, "Update_TotalConsumption") as MethodNode;
            MethodNode updateTotalConsumptionPer_m3Node = nodeFactory.CreateNode(building, "Update_TotalConsumptionPer_m3") as MethodNode;

            graph.AddDependency(updateAreaNode, updateVolumeNode);
            graph.AddDependency(updateAreaNode, updateTotalConsumptionNode);
            graph.AddDependency(updateTotalConsumptionNode, updateTotalConsumptionPer_m3Node);
            graph.AddDependency(updateVolumeNode, updateTotalConsumptionPer_m3Node);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case3_GivenUpdateAreaAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case3_GivenUpdateVolumeAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case3_GivenUpdateTotalConsumptionPer_m3AsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case3_GivenNoInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        #endregion

        #region PerformUpdate CASE 4

        /*
         * Simple dependency graph with method nodes from three objects of two different classes.
         */

        private void CreateCase4(DependencyGraph graph, Apartment04 apartment)
        {
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

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case4_GivenNoInitialNode(DependencyGraph graph, Apartment04 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Update_Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Update_UtilityArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Update_Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Update_UtilityArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_TotalArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_TotalConsumption"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case4_GivenBalconyUpdateAreaAsInitialNode(DependencyGraph graph, Apartment04 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Update_Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Update_UtilityArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_TotalArea"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode(DependencyGraph graph, Apartment04 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_TotalArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Update_TotalConsumption"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            CreateCase4(graph, apartment);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case4_GivenNoInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            CreateCase4(graph, apartment);

            //Act
            graph.PerformUpdate();

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

        [TestMethod]
        public void PerformUpdate_Case4_GivenBalconyUpdateAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            CreateCase4(graph, apartment);
            INode initialNode = graph.GetNode(apartment.Balcony, "Update_Area");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case4_GivenBalconyUpdateAreaAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            CreateCase4(graph, apartment);
            INode initialNode = graph.GetNode(apartment, "Update_HeatedArea");

            //Act
            graph.PerformUpdate(initialNode, false);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case4_GivenApartmentUpdateHeatedAreaAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case4_ChangingBalconyLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment04 apartment = new Apartment04 { Name = "Apartment 04" };

            CreateCase4(graph, apartment);
            graph.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(apartment.Balcony, "Update_Area");
            apartment.Balcony.Length = 4;
            graph.PerformUpdate(initialNode, false);

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

        #region PerformUpdate CASE 5

        /*
         * Simple dependency graph with mixed method and property nodes from same object.
         */

        private void CreateCase5(DependencyGraph graph, Building02 building)
        {
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

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case5_GivenNoInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Consumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Height"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case5_GivenHeightAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case5_GivenConsumptionAsInitialNode(DependencyGraph graph, Building02 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Update_TotalConsumptionPer_m3"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02 { Name = "Building 02" };

            CreateCase5(graph, building);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case5_GivenNoInitialNode(graph, building);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02 { Name = "Building 02" };

            CreateCase5(graph, building);

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase5(graph, building);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenLengthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase5(graph, building);
            INode initialNode = graph.GetNode(building, "Length");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case5_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenHeightAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase5(graph, building);
            INode initialNode = graph.GetNode(building, "Height");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case5_GivenHeightAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case5_GivenConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building02 building = new Building02();

            CreateCase5(graph, building);
            INode initialNode = graph.GetNode(building, "Consumption");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case5_GivenConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        #endregion

        #region PerformUpdate CASE 6

        /*Simple graph with cycles */

        private void CreateCase6(DependencyGraph graph, Cycle cycle)
        {
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

            graph.Initialize();
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenNoInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            CreateCase6(graph, cycle);

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => graph.PerformUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenANodeAsInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();
            
            CreateCase6(graph, cycle);
            INode initialNode = graph.GetNode(cycle, "A");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenBNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            CreateCase6(graph, cycle);
            INode initialNode = graph.GetNode(cycle, "B");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenCNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            CreateCase6(graph, cycle);
            INode initialNode = graph.GetNode(cycle, "C");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenDNodeAsInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            CreateCase6(graph, cycle);
            INode initialNode = graph.GetNode(cycle, "D");

            //Act&Assert
            Assert.ThrowsException<CyclicReactiveDependencyException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_Case6_GivenENodeAsInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Cycle cycle = new Cycle();

            CreateCase6(graph, cycle);
            INode initialNode = graph.GetNode(cycle, "E");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = new Logger();
            expectedLogger.LogNodeToUpdate(graph.GetNode(cycle, "F"));

            Assert.AreEqual(actualLogger.ToString(), expectedLogger.ToString());
        }

        #endregion

        #region PerformUpdate CASE 7

        /*Simple Graph with Collection Node as a predecessor*/

        private void CreateCase7(DependencyGraph graph, Whole whole)
        {
            INode a = graph.AddNode(whole, "A");
            INode b = graph.AddNode(whole, "B");
            INode c = graph.AddNode(whole, "C");

            INode partsA = graph.AddNode(whole.Parts, "A");
            INode partsB = graph.AddNode(whole.Parts, "B");
            INode partsC = graph.AddNode(whole.Parts, "C");

            graph.AddDependency(partsA, a);
            graph.AddDependency(partsB, b);
            graph.AddDependency(partsC, c);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case7_GivenNoInitialNode(DependencyGraph graph, Whole whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "C"));
            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "B"));
            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "A"));

            logger.LogNodeToUpdate(graph.GetNode(whole, "C"));
            logger.LogNodeToUpdate(graph.GetNode(whole, "B"));
            logger.LogNodeToUpdate(graph.GetNode(whole, "A"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case7_GivenAAsInitialNode(DependencyGraph graph, Whole whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole, "A"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case7_GivenBAsInitialNode(DependencyGraph graph, Whole whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            CreateCase7(graph, whole);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_GivenNoInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            CreateCase7(graph, whole);

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(whole.A == 12 && whole.B == 15 && whole.C == 18);
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenAAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            CreateCase7(graph, whole);

            //Act
            INode initialNode = graph.GetNode(whole.Parts, "A");
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_GivenAAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case7_GivenBAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            CreateCase7(graph, whole);

            //Act
            INode initialNode = graph.GetNode(whole.Parts, "B");
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_GivenBAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case7_ChangingA_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            CreateCase7(graph, whole);
            graph.PerformUpdate();
            INode initialNode = graph.GetNode(whole.Parts, "A");

            //Act
            whole.Parts[0].A = 8;
            graph.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(whole.A == 19 && whole.B == 15 && whole.C == 18);
        }

        #endregion

        #region PerformUpdate CASE 7.1

        /*Simple graph with Collection node as a successor */

        private void CreateCase7_1(DependencyGraph graph, out Whole2 whole)
        {
            whole = new Whole2();

            INode wholeCoeffA = nodeFactory.CreateNode(whole, "CoeffA");
            INode wholeCoeffB = nodeFactory.CreateNode(whole, "CoeffB");
            INode wholeCoeffC = nodeFactory.CreateNode(whole, "CoeffC");

            INode partsA = nodeFactory.CreateNode(whole.Parts, "A", "Update_A");
            INode partsB = nodeFactory.CreateNode(whole.Parts, "B", "Update_B");
            INode partsC = nodeFactory.CreateNode(whole.Parts, "C", "Update_C");

            graph.AddDependency(wholeCoeffA, partsA);
            graph.AddDependency(wholeCoeffB, partsB);
            graph.AddDependency(wholeCoeffC, partsC);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case7_1_GivenNoInitialNode(DependencyGraph graph, Whole2 whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole, "CoeffC"));
            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "C"));
            logger.LogNodeToUpdate(graph.GetNode(whole, "CoeffB"));
            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "B"));
            logger.LogNodeToUpdate(graph.GetNode(whole, "CoeffA"));
            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole;
            CreateCase7_1(graph, out whole);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_1_GivenNoInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole;
            CreateCase7_1(graph, out whole);

            //Act
            graph.PerformUpdate();

            //Assert
            Part2 p1 = whole.Parts[0];
            Part2 p2 = whole.Parts[1];
            Part2 p3 = whole.Parts[2];
            Assert.IsTrue(p1.A == 2 && p1.B == 4 && p1.C == 6
                && p2.A == 4 && p2.B == 8 && p2.C == 12
                && p3.A == 6 && p3.B == 12 && p3.C == 18);
        }

        private Logger CreateExpectedLogger_Case7_1_GivenCoeffAAsInitialNode(DependencyGraph graph, Whole2 whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "A"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenCoeffAAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole;
            CreateCase7_1(graph, out whole);

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffA");
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_1_GivenCoeffAAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case7_1_GivenCoeffBAsInitialNode(DependencyGraph graph, Whole2 whole)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(whole.Parts, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_GivenCoeffBAsInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole;
            CreateCase7_1(graph, out whole);

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffB");
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case7_1_GivenCoeffBAsInitialNode(graph, whole);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case7_1_ChangingCoeffA_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole2 whole;
            CreateCase7_1(graph, out whole);
            graph.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(whole, "CoeffA");
            whole.CoeffA = 3;
            graph.PerformUpdate(initialNode);

            //Assert
            Part2 p1 = whole.Parts[0];
            Part2 p2 = whole.Parts[1];
            Part2 p3 = whole.Parts[2];
            Assert.IsTrue(p1.A == 3 && p1.B == 4 && p1.C == 6
                && p2.A == 6 && p2.B == 8 && p2.C == 12
                && p3.A == 9 && p3.B == 12 && p3.C == 18);
        }

        #endregion
    }
}
