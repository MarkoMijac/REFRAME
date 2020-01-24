using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class ObjectVisualGraph : VisualGraph
    {
        public ObjectVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel }
            };
        }

        protected override void AddCustomProperties()
        {
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Class", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Degree", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("InDegree", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("OutDegree", System.Type.GetType("System.String"));
        }

        protected override void AddDependenciesToGraph()
        {
            GraphNode dgmlPredecessor;
            GraphNode dgmlSuccessor;
            foreach (var analysisNode in _analysisNodes)
            {
                dgmlPredecessor = _dgmlGraph.Nodes.Get(analysisNode.Identifier.ToString());
                foreach (var analysisSuccessor in analysisNode.Successors)
                {
                    dgmlSuccessor = _dgmlGraph.Nodes.Get(analysisSuccessor.Identifier.ToString());
                    if (dgmlSuccessor != null)
                    {
                        GraphLink dependency = _dgmlGraph.Links.GetOrCreate(dgmlPredecessor, dgmlSuccessor);
                    }
                }
            }
        }

        protected override void AddNodesToGraph()
        {
            foreach (ObjectAnalysisNode node in _analysisNodes)
            {
                GraphNode g = _dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("Name", node.OwnerClass.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
            }
        }
    }
}
