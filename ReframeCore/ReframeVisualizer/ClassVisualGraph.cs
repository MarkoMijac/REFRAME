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
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel}
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            base.AddCustomProperties(dgmlGraph);
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
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
            if (VisualizationOptions.GroupingLevel == GroupingLevel.NoGrouping)
            {

            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (ClassAnalysisNode node in _analysisNodes)
            {
                AssemblyAnalysisNode ownerAssembly = node.OwnerAssembly;
                GraphNode groupNode = dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ClassAnalysisNode node in _analysisNodes)
            {
                NamespaceAnalysisNode ownerNamespace = node.OwnerNamespace;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.OwnerAssembly.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddNodes(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (ClassAnalysisNode node in _analysisNodes)
            {
                GraphNode g = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("FullName", node.FullName);
                g.SetValue("Namespace", node.OwnerNamespace.Name);
                g.SetValue("Assembly", node.OwnerAssembly.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);

                GraphNode namespaceNode = dgmlGraph.Nodes.Get(node.OwnerNamespace.Identifier.ToString());
                if (namespaceNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(namespaceNode, g, "", catContains);
                }
            }
        }
    }
}
