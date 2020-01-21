using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public enum VisualizationLevel { ClassLevel };

    public class VisualGraphFactory
    {
        public IVisualGraph CreateGraph(IEnumerable<IAnalysisNode> analysisNodes, VisualizationLevel level)
        {
            IVisualGraph visualGraph;

            switch (level)
            {
                case VisualizationLevel.ClassLevel:
                    {
                        visualGraph = new ClassVisualGraph(analysisNodes);
                        break;
                    }
                default:
                    visualGraph = null;
                    break;
            }

            return visualGraph;
        }
    }
}
