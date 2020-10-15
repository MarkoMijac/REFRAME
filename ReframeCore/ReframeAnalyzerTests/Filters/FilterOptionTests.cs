using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Nodes;

namespace ReframeAnalyzerTests.Filters
{
    [TestClass]
    public class FilterOptionTests
    {
        [TestMethod]
        public void SelectNode_GivenValidNode_NodeIsSelected()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            var firstNode = analysisNodes[0];

            //Act
            filterOption.SelectNode(firstNode);

            //Assert
            Assert.IsTrue(filterOption.IsSelected(firstNode));
        }

        [TestMethod]
        public void SelectNode_GivenNullNode_NodeIsNotSelected()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            filterOption.SelectNode(null);

            //Assert
            Assert.IsFalse(filterOption.IsSelected(null));
        }

        [TestMethod]
        public void SelectNode_GivenValidNode_NodeSelectedEventIsRaised()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            var firstNode = analysisNodes[0];

            bool eventRaised = false;

            filterOption.NodeSelected += delegate
            {
                eventRaised = true;
            };

            //Act
            filterOption.SelectNode(firstNode);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void SelectNode_GivenNotValidNode_NodeSelectedEventNotRaised()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            var firstNode = analysisNodes[0];

            bool eventRaised = false;

            filterOption.NodeSelected += delegate
            {
                eventRaised = true;
            };

            //Act
            filterOption.SelectNode(null);

            //Assert
            Assert.IsFalse(eventRaised);
        }

        [TestMethod]
        public void DeselectNode_GivenValidNode_NodeIsDeselected()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            var firstNode = analysisNodes[0];
            filterOption.SelectNode(firstNode);

            bool eventRaised = false;

            filterOption.NodeDeselected += delegate
            {
                eventRaised = true;
            };

            //Act
            filterOption.DeselectNode(firstNode);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void DeselectNode_GivenValidNode_NodeDeselectedEventIsRaised()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            var firstNode = analysisNodes[0];
            filterOption.SelectNode(firstNode);

            //Act
            filterOption.DeselectNode(firstNode);

            //Assert
            Assert.IsFalse(filterOption.IsSelected(firstNode));
        }

        [TestMethod]
        public void DeselectNode_GivenNullNode_NodeIsDeselected()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            filterOption.DeselectNode(null);

            //Assert
            Assert.IsFalse(filterOption.IsSelected(null));
        }

        [TestMethod]
        public void GetNodes_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            var filterOption = new FilterOption(analysisNodes);

            //Act
            var list = filterOption.GetNodes();

            //Assert
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void GetNodes_GivenNonEmptyListOfOriginalNodes_ReturnsNonEmptyList()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            var list = filterOption.GetNodes();

            //Assert
            Assert.IsTrue(list.Count == 3);
        }

        [TestMethod]
        public void GetNodes_GivenConditionProvided_ReturnsCorrectNodes()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            var list = filterOption.GetNodes(n => n.Identifier == 2222);

            //Assert
            Assert.IsTrue(list[0].Identifier == 2222);
        }

        [TestMethod]
        public void SelectNodes_GivenNoCondition_SelectsAllNodes()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            filterOption.SelectNodes();

            //Assert
            Assert.IsTrue(filterOption.IsSelected(analysisNodes[0])
                && filterOption.IsSelected(analysisNodes[1])
                && filterOption.IsSelected(analysisNodes[2]));
        }

        [TestMethod]
        public void SelectNodes_GivenProvidedCondition_SelectsCorrectNodes()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);

            //Act
            filterOption.SelectNodes(n => n.Identifier == 2222 || n.Identifier == 3333);

            //Assert
            Assert.IsTrue(filterOption.IsSelected(analysisNodes[1])
                && filterOption.IsSelected(analysisNodes[2]));
        }

        [TestMethod]
        public void DeselectNodes_GivenNoCondition_DeselectsAllNodes()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            filterOption.SelectNodes();

            //Act
            filterOption.DeselectNodes();

            //Assert
            Assert.IsTrue(filterOption.IsSelected(analysisNodes[0]) == false
                && filterOption.IsSelected(analysisNodes[1]) == false
                && filterOption.IsSelected(analysisNodes[2]) == false);
        }

        [TestMethod]
        public void DeselectNodes_GivenProvidedCondition_DeselectsCorrectNodes()
        {
            //Arrange
            List<IAnalysisNode> analysisNodes = new List<IAnalysisNode>();
            analysisNodes.Add(new ObjectMemberAnalysisNode(1111));
            analysisNodes.Add(new ObjectMemberAnalysisNode(2222));
            analysisNodes.Add(new ObjectMemberAnalysisNode(3333));

            var filterOption = new FilterOption(analysisNodes);
            filterOption.SelectNodes();

            //Act
            filterOption.DeselectNodes(n=>n.Identifier == 1111 || n.Identifier == 2222);

            //Assert
            Assert.IsTrue(filterOption.IsSelected(analysisNodes[0]) == false
                && filterOption.IsSelected(analysisNodes[1]) == false);
        }
    }
}
