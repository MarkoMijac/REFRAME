using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Nodes;
using ReframeVisualizer;
using VisualizerDGML.Factories;
using VisualizerDGML.Graphs;

namespace VisualizerDGMLTests.Factories
{
    [TestClass]
    public class ObjectGraphFactoryDGML_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(VisualizationException))]
        public void CreateGraph_GivenEmptyIdentifier_ThrowsException()
        {
            //Arrange
            var factory = new ObjectGraphFactoryDGML();

            //Act
            factory.CreateGraph("", new List<IAnalysisNode>());
        }

        [TestMethod]
        [ExpectedException(typeof(VisualizationException))]
        public void CreateGraph_GivenNodeListIsNull_ThrowsException()
        {
            //Arrange
            var factory = new ObjectGraphFactoryDGML();

            //Act
            factory.CreateGraph("G1", null);
        }

        [TestMethod]
        public void CreateGraph_GivenValidParameters_ReturnsClassMemberGraph()
        {
            //Arrange
            var factory = new ObjectGraphFactoryDGML();

            //Act
            var graph = factory.CreateGraph("G1", new List<IAnalysisNode>());

            //Assert
            Assert.IsTrue(graph != null && graph.GetType() == typeof(ObjectVisualGraphDGML));
        }
    }
}
