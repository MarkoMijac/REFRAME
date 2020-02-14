using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Exceptions;
using ReframeCore;

namespace ReframeAnalyzerTests
{
    /// <summary>
    /// Summary description for AnalysisFilterFactoryTests
    /// </summary>
    [TestClass]
    public class AnalysisFilterFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateFilter_GivenNodesIsNull_ThrowsException()
        {
            //Arrange
            IEnumerable<IAnalysisNode> nodes = null;
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(nodes, AnalysisLevel.AssemblyLevel);
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsAssemblyAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var assemblyGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.AssemblyLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(assemblyGraph.Nodes, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(AssemblyAnalysisFilter));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateFilter_GivenGraphAndLevelMismatch_ThrowsException()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.NamespaceLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.AssemblyLevel);
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsNamespaceAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.NamespaceLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(NamespaceAnalysisFilter));
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsClassAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ClassLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(ClassAnalysisFilter));
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsClassMemberAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ClassMemberLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.ClassMemberLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(ClassMemberAnalysisFilter));
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsObjectAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(ObjectAnalysisFilter));
        }

        [TestMethod]
        public void CreateFilter_GivenMatchingGraphAndLevel_ReturnsObjectMemberAnalysisFilter()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var factory = new AnalysisFilterFactory();

            //Act
            var filter = factory.CreateFilter(analysisGraph.Nodes, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsInstanceOfType(filter, typeof(ObjectMemberAnalysisFilter));
        }
    }
}
