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
            Options = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel }
            };
        }

        protected override void AddCustomProperties(Graph graph)
        {
            base.AddCustomProperties(graph);
            graph.DocumentSchema.Properties.AddNewProperty("Class", System.Type.GetType("System.String"));
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
            else if (Options.ChosenGroupingLevel == GroupingLevel.ClassLevel)
            {
                AddClassGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (var node in AnalysisNodes)
            {
                IAnalysisNode ownerAssembly = node.Parent.Parent2;
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
                IAnalysisNode ownerNamespace = node.Parent.Parent;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.Parent.Parent2.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddClassGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (var node in AnalysisNodes)
            {
                var ownerClass = node.Parent;
                GraphNode classNode = dgmlGraph.Nodes.GetOrCreate(ownerClass.Identifier.ToString(), ownerClass.Name, null);
                classNode.IsGroup = true;
                classNode.SetValue("Name", ownerClass.Name);
                classNode.SetValue("FullName", ownerClass.Name);
                classNode.SetValue("Namespace", ownerClass.Parent.Name);
                classNode.SetValue("Assembly", ownerClass.Parent2.Name);

                GraphNode namespaceNode = dgmlGraph.Nodes.Get(ownerClass.Parent.Identifier.ToString());
                if (namespaceNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(namespaceNode, classNode, "", catContains);
                }
            }
        }

        private void AddNodes(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (var node in AnalysisNodes)
            {
                GraphNode objectNode = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                objectNode.SetValue("Name", node.Name);
                objectNode.SetValue("Class", node.Parent.Name);
                objectNode.SetValue("Degree", node.Degree);
                objectNode.SetValue("InDegree", node.InDegree);
                objectNode.SetValue("OutDegree", node.OutDegree);
                objectNode.SetValue("Tag", node.Tag);

                GraphNode classNode = dgmlGraph.Nodes.Get(node.Parent.Identifier.ToString());
                if (classNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(classNode, objectNode, "", catContains);
                }
            }
        }
    }
}
