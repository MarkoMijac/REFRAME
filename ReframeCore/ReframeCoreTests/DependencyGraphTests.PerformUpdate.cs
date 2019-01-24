using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;

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

        #region PerformUpdate CASE2

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


    }
}
