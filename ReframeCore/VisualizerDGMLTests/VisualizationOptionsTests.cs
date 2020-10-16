using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeVisualizer;

namespace VisualizerDGMLTests
{
    [TestClass]
    public class VisualizationOptionsTests
    {
        [TestMethod]
        public void ChosenGroupingLevel_GivenLevelIsNotAllowed_LevelNotChosen()
        {
            //Arrange
            var options = new VisualizationOptions();

            //Act
            options.ChosenGroupingLevel = GroupingLevel.AssemblyLevel;

            //Assert
            Assert.IsTrue(options.ChosenGroupingLevel == GroupingLevel.NoGrouping);
        }

        [TestMethod]
        public void ChosenGroupingLevel_GivenLevelIsAllowed_LevelChosen()
        {
            //Arrange
            var options = new VisualizationOptions();
            options.AllowedGroupingLevels.Add(GroupingLevel.ClassLevel);

            //Act
            options.ChosenGroupingLevel = GroupingLevel.ClassLevel;

            //Assert
            Assert.IsTrue(options.ChosenGroupingLevel == GroupingLevel.ClassLevel);
        }
    }
}
