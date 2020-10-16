using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeImporter;

namespace ReframeImporterTests
{
    [TestClass]
    public class ImporterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ImporterException))]
        public void GetReactor_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            Importer importer = new Importer();

            //Act&Assert
            XElement xReactor = importer.GetReactor("");
        }

        [TestMethod]
        [ExpectedException(typeof(ImporterException))]
        public void GetReactor_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            Importer importer = new Importer();

            //Act&Assert
            XElement xReactor = importer.GetReactor("<not valid source?_");
        }

        [TestMethod]
        public void GetReactor_XmlSourceValid_ReturnsReactorElement()
        {
            //Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>R1</Identifier><TotalNodeCount>0</TotalNodeCount><Nodes/></Graph></Reactor>";

            //Act
            XElement xReactor = importer.GetReactor(source);

            //Assert
            Assert.IsTrue(xReactor != null);
            Assert.IsTrue(xReactor.Name == "Reactor");
            Assert.IsTrue(xReactor.Element("Identifier").Value == "R1");
        }

        [TestMethod]
        [ExpectedException(typeof(ImporterException))]
        public void GetGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            Importer importer = new Importer();

            //Act&Assert
            XElement xGraph = importer.GetGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(ImporterException))]
        public void GetGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            Importer importer = new Importer();

            //Act&Assert
            XElement xGraph = importer.GetGraph("<not valid source?_");
        }

        [TestMethod]
        public void GetGraph_XmlSourceValid_ReturnsGraphElement()
        {
            //Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>0</TotalNodeCount><Nodes/></Graph></Reactor>";

            //Act
            XElement xGraph = importer.GetGraph(source);

            //Assert
            Assert.IsTrue(xGraph != null);
            Assert.IsTrue(xGraph.Name == "Graph");
            Assert.IsTrue(xGraph.Element("Identifier").Value == "G1");
        }

        [TestMethod]
        public void GetIdentifier_GivenValidGraph_ReturnsGraphIdentifier()
        {
            ///Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>0</TotalNodeCount><Nodes/></Graph></Reactor>";
            XElement xGraph = importer.GetGraph(source);

            //Act

            string identifier = importer.GetIdentifier(xGraph);

            //Assert
            Assert.AreEqual(identifier, "G1");
        }

        [TestMethod]
        public void GetIdentifier_GivenValidNode_ReturnsNodeIdentifier()
        {
            ///Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>3</TotalNodeCount>" +
                "<Nodes><Node><Identifier>N1</Identifier></Node><Node><Identifier>N2</Identifier></Node><Node><Identifier>N3</Identifier></Node></Nodes>" +
                "</Graph></Reactor>";
            XElement xGraph = importer.GetGraph(source);
            var nodes = importer.GetNodes(xGraph);

            //Act
            string identifier = importer.GetIdentifier(nodes.First());

            //Assert
            Assert.AreEqual(identifier, "N1");
        }

        [TestMethod]
        [ExpectedException(typeof(ImporterException))]
        public void GetIdentifier_GivenElementWithNoIdentifier_ThrowsException()
        {
            ///Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>3</TotalNodeCount>" +
                "<Nodes><Node><Identifier>N1</Identifier></Node><Node><Identifier>N2</Identifier></Node><Node><Identifier>N3</Identifier></Node></Nodes>" +
                "</Graph></Reactor>";
            XElement xGraph = importer.GetGraph(source);
            XElement xNodes = xGraph.Element("Nodes");

            //Act&Assert
            string identifier = importer.GetIdentifier(xNodes);
        }

        [TestMethod]
        public void GetNodes_GivenValidNonEmptyGraph_ReturnsListOfNodes()
        {
            ///Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>3</TotalNodeCount>" +
                "<Nodes><Node><Identifier>N1</Identifier></Node><Node><Identifier>N2</Identifier></Node><Node><Identifier>N3</Identifier></Node></Nodes>" +
                "</Graph></Reactor>";
            XElement xGraph = importer.GetGraph(source);

            //Act
            IEnumerable<XElement> nodes = importer.GetNodes(xGraph);

            //Assert
            Assert.IsTrue(nodes.Count() == 3);
        }

        [TestMethod]
        public void GetNodes_GivenValidEmptyGraph_ReturnsEmptyListOfNodes()
        {
            ///Arrange
            Importer importer = new Importer();
            string source = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><Reactor><Identifier>R1</Identifier><Graph><Identifier>G1</Identifier><TotalNodeCount>0</TotalNodeCount><Nodes/></Graph></Reactor>";
            XElement xGraph = importer.GetGraph(source);

            //Act
            IEnumerable<XElement> nodes = importer.GetNodes(xGraph);

            //Assert
            Assert.IsTrue(nodes.Count() == 0);
        }

        [TestMethod]
        public void GetNodes_GivenGraphIsNull_ReturnsEmptyListOfNodes()
        {
            ///Arrange
            Importer importer = new Importer();
            XElement xGraph = null;

            //Act
            IEnumerable<XElement> nodes = importer.GetNodes(xGraph);

            //Assert
            Assert.IsTrue(nodes.Count() == 0);
        }

        [TestMethod]
        public void GetSuccessors_GivenNodeIsNull_ReturnsEmptyListOfNodes()
        {
            ///Arrange
            Importer importer = new Importer();
            XElement xNode = null;

            //Act
            IEnumerable<XElement> nodes = importer.GetSuccessors(xNode);

            //Assert
            Assert.IsTrue(nodes.Count() == 0);
        }

    }
}
