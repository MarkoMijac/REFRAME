using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public static class GraphMetrics
    {
        public static int GetNumberOfNodes(IAnalysisGraph analysisGraph)
        {
            return GetNumberOfNodes(analysisGraph.Nodes);
        }

        public static int GetNumberOfNodes(IEnumerable<IAnalysisNode> analysisNodes)
        {
            return analysisNodes.Count();
        }

        public static int GetNumberOfEdges(IAnalysisGraph analysisGraph)
        {
            return GetNumberOfEdges(analysisGraph.Nodes);
        }

        public static int GetNumberOfEdges(IEnumerable<IAnalysisNode> analysisNodes)
        {
            int numOfDependencies = 0;

            foreach (IAnalysisNode node in analysisNodes)
            {
                numOfDependencies += node.Successors.Count;
            }

            return numOfDependencies;
        }

        public static int GetMaximumNumberOfEdges(IEnumerable<IAnalysisNode> analysisNodes)
        {
            int maxNumOfEdges = 0;
            int numberOfNodes = GetNumberOfNodes(analysisNodes);

            if (numberOfNodes > 0)
            {
                maxNumOfEdges = (numberOfNodes * (numberOfNodes - 1)) / 2;
            }
            return maxNumOfEdges;
        }

        public static int GetMaximumNumberOfEdges(IAnalysisGraph analysisGraph)
        {
            return GetMaximumNumberOfEdges(analysisGraph.Nodes);
        }

        public static float GetGraphDensity(IEnumerable<IAnalysisNode> analysisNodes)
        {
            float numOfEdges = GetNumberOfEdges(analysisNodes);
            float maxNumOfEdges = GetMaximumNumberOfEdges(analysisNodes);

            float graphDensity = 0;

            if (maxNumOfEdges > 0)
            {
                graphDensity = numOfEdges / maxNumOfEdges;
            }

            return graphDensity;
        }

        public static float GetGraphDensity(IAnalysisGraph analysisGraph)
        {
            return GetGraphDensity(analysisGraph.Nodes);
        }
    }
}
