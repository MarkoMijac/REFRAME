using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public enum GroupingLevel { NoGrouping, AssemblyLevel, NamespaceLevel, ClassLevel, ObjectLevel}

    public class VisualizationOptions
    {
        public VisualizationOptions()
        {
            groupingLevel = GroupingLevel.NoGrouping;
        }

        private GroupingLevel groupingLevel;

        public GroupingLevel ChosenGroupingLevel
        {
            get { return groupingLevel; }
            set
            {
                if (AllowedGroupingLevels.Contains(value) == true)
                {
                    groupingLevel = value;
                }
            }
        }

        public List<GroupingLevel> AllowedGroupingLevels { get; set; } = new List<GroupingLevel> { GroupingLevel.NoGrouping };
    }
}
