using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class AssemblyVisualGraph : VisualGraph
    {
        public AssemblyVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {

        }

        protected override void AddNodesToGraph(Graph dgmlGraph)
        {
            AddNodes(dgmlGraph);
        }

        private void AddNodes(Graph dgmlGraph)
        {
            foreach (var node in AnalysisNodes)
            {
                GraphNode g = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);
            }
        }
    }
}
