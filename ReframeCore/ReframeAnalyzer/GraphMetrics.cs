using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Nodes;
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
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetNumberOfNodes(analysisGraph.Nodes);
        }

        public static int GetNumberOfNodes(IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return analysisNodes.Count();
        }

        public static int GetNumberOfEdges(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetNumberOfEdges(analysisGraph.Nodes);
        }

        public static int GetNumberOfEdges(IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            int numOfDependencies = 0;

            foreach (IAnalysisNode node in analysisNodes)
            {
                numOfDependencies += node.Successors.Count;
            }

            return numOfDependencies;
        }

        public static int GetMaximumNumberOfEdges(IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            int maxNumOfEdges = 0;
            int numberOfNodes = GetNumberOfNodes(analysisNodes);

            if (numberOfNodes > 0)
            {
                maxNumOfEdges = (numberOfNodes * (numberOfNodes - 1));
                maxNumOfEdges = ApplyCorrectionForObjectMemberGraphs(analysisNodes, maxNumOfEdges);
            }
            return maxNumOfEdges;
        }

        private static int ApplyCorrectionForObjectMemberGraphs(IEnumerable<IAnalysisNode> analysisNodes, int maxNumOfEdges)
        {
            int afterCorrection = maxNumOfEdges;

            if (analysisNodes.ElementAt(0).Level == AnalysisLevel.ObjectMemberLevel)
            {
                afterCorrection = afterCorrection / 2;
            }

            return afterCorrection;
        }

        public static int GetMaximumNumberOfEdges(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetMaximumNumberOfEdges(analysisGraph.Nodes);
        }

        public static float GetGraphDensity(IEnumerable<IAnalysisNode> analysisNodes)
        {
            if (analysisNodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

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
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetGraphDensity(analysisGraph.Nodes);
        }
    }
}
