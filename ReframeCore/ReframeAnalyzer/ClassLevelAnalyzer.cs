using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ReframeCore.Factories;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Xml;

namespace ReframeAnalyzer
{
    public class ClassLevelAnalyzer : Analyzer
    {
        #region Methods

        public override IAnalysisGraph GetAnalysisGraph(IDependencyGraph graph)
        {
            var classGraph = new ClassAnalysisGraph();
            classGraph.InitializeGraph(graph);

            return classGraph;
        }

        public IAnalysisGraph GetSourceNodes(IDependencyGraph graph)
        {
            IEnumerable<INode> sourceNodes = graph.Nodes.Where(n => n.Predecessors.Count == 0 && n.Successors.Count > 0);

            var classGraph = new ClassAnalysisGraph();
            classGraph.InitializeGraph(graph, sourceNodes);
            return classGraph;
        }

        #endregion
    }
}
