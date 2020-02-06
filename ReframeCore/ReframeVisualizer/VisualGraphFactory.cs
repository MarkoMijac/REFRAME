using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class VisualGraphFactory
    {
        public IVisualGraph CreateGraph(IEnumerable<IAnalysisNode> analysisNodes, AnalysisLevel level)
        {
            IVisualGraph visualGraph;

            switch (level)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        visualGraph = new ObjectMemberVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        visualGraph = new ObjectVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        visualGraph = new ClassMemberVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        visualGraph = new ClassVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        visualGraph = new AssemblyVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        visualGraph = new NamespaceVisualGraph(analysisNodes);
                        break;
                    }
                case AnalysisLevel.UpdateAnalysisLevel:
                    {
                        visualGraph = new UpdateVisualGraph(analysisNodes);
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
