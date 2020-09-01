using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;

namespace ReframeVisualizer
{
    public class ClassVisualGraph : VisualGraph
    {
        public ClassVisualGraph(IEnumerable<IAnalysisNode> analysisNodes)
            :base(analysisNodes)
        {
            Options = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel}
            };
        }

        protected override void AddCustomProperties(Graph graph)
        {
            base.AddCustomProperties(graph);
            graph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
        }

        protected override void AddNodesToGraph(Graph graph)
        {
            AddGroupNodes(graph);
            AddNodes(graph);
        }

        private void AddGroupNodes(Graph dgmlGraph)
        {
            if (Options.ChosenGroupingLevel == GroupingLevel.NoGrouping)
            {

            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (var node in AnalysisNodes)
            {
                IAnalysisNode ownerAssembly = node.Parent2;
                GraphNode groupNode = dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (var node in AnalysisNodes)
            {
                IAnalysisNode ownerNamespace = node.Parent;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.Parent2.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddNodes(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (var node in AnalysisNodes)
            {
                GraphNode g = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("FullName", node.Name);
                g.SetValue("Namespace", node.Parent.Name);
                g.SetValue("Assembly", node.Parent2.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);

                GraphNode namespaceNode = dgmlGraph.Nodes.Get(node.Parent.Identifier.ToString());
                if (namespaceNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(namespaceNode, g, "", catContains);
                }
            }
        }
    }
}
