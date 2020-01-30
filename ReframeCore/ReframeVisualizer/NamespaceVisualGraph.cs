using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class NamespaceVisualGraph : VisualGraph
    {
        public NamespaceVisualGraph(IEnumerable<IAnalysisNode> analysisNode) : base(analysisNode)
        {
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel> { GroupingLevel.NoGrouping }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Degree", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("InDegree", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OutDegree", System.Type.GetType("System.String"));
        }

        protected override void AddDependenciesToGraph(Graph dgmlGraph)
        {
            GraphNode dgmlPredecessor;
            GraphNode dgmlSuccessor;
            foreach (var analysisNode in _analysisNodes)
            {
                dgmlPredecessor = dgmlGraph.Nodes.Get(analysisNode.Identifier.ToString());
                foreach (var analysisSuccessor in analysisNode.Successors)
                {
                    dgmlSuccessor = dgmlGraph.Nodes.Get(analysisSuccessor.Identifier.ToString());
                    if (dgmlSuccessor != null)
                    {
                        GraphLink dependency = dgmlGraph.Links.GetOrCreate(dgmlPredecessor, dgmlSuccessor);
                    }
                }
            }
        }

        protected override void AddNodesToGraph(Graph dgmlGraph)
        {
            AddGroupNodes(dgmlGraph);
            AddNodes(dgmlGraph);
        }

        private void AddGroupNodes(Graph dgmlGraph)
        {

        }

        private void AddNodes(Graph dgmlGraph)
        {
            foreach (NamespaceAnalysisNode node in _analysisNodes)
            {
                GraphNode g = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
            }
        }
    }
}
