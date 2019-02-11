using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;
using ReframeCoreExamples.E02;
using ReframeCoreExamples.E04;

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

            INode widthNode = new PropertyNode(building, "Width");
            INode lengthNode = new PropertyNode(building, "Length");
            INode areaNode = new PropertyNode(building, "Area", "Update_Area");
            INode heightNode = new PropertyNode(building, "Height");
            INode volumeNode = new PropertyNode(building, "Volume");

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

            INode widthNode = new PropertyNode(building, "Width");
            INode lengthNode = new PropertyNode(building, "Length");
            INode areaNode = new PropertyNode(building, "Area", "Update_Area");
            INode heightNode = new PropertyNode(building, "Height");
            INode volumeNode = new PropertyNode(building, "Volume");

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

            INode widthNode = new PropertyNode(building, "Width");
            INode lengthNode = new PropertyNode(building, "Length");
            INode areaNode = new PropertyNode(building, "Area", "Update_Area");
            INode heightNode = new PropertyNode(building, "Height");
            INode volumeNode = new PropertyNode(building, "Volume");

            INode consumption = new PropertyNode(building, "Consumption");

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

            INode widthNode = new PropertyNode(building, "Width");
            INode lengthNode = new PropertyNode(building, "Length");
            INode areaNode = new PropertyNode(building, "Area", "Update_Area");
            INode heightNode = new PropertyNode(building, "Height");
            INode volumeNode = new PropertyNode(building, "Volume", "Update_Volume");
            INode consumptionNode = new PropertyNode(building, "Consumption");
            INode totalConsumptionNode = new PropertyNode(building, "TotalConsumption", "Update_TotalConsumption");
            INode totalConsumptionPer_m3 = new PropertyNode(building, "TotalConsumptionPer_m3", "Update_TotalConsumptionPer_m3");

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

            INode updateAreaNode = new MethodNode(building, "Update_Area");
            INode updateVolumeNode = new MethodNode(building, "Update_Volume");
            INode updateTotalConsumptionNode = new MethodNode(building, "Update_TotalConsumption");
            INode updateTotalConsumptionPer_m3Node = new MethodNode(building, "Update_TotalConsumptionPer_m3");

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
    }
}
