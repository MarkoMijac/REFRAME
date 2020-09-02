using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using ReframeVisualizer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer.Graphs
{
    public class ObjectMemberDGMLVisualGraph : VisualGraph
    {
        public ObjectMemberDGMLVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {
            Options = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel, GroupingLevel.ObjectLevel }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            base.AddCustomProperties(dgmlGraph);
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OwnerObjectIdentifier", Type.GetType("System.String"));
        }

        protected override void AddNodesToGraph(Graph dgmlGraph)
        {
            AddGroupNodes(dgmlGraph);
            AddNodes(dgmlGraph);
        }

        private void AddGroupNodes(Graph dgmlGraph)
        {
            if (Options.ChosenGroupingLevel == GroupingLevel.NoGrouping)
            {

            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.ObjectLevel)
            {
                AddObjectGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.ClassLevel)
            {
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
            else if (Options.ChosenGroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (var node in AnalysisNodes)
            {
                IAnalysisNode ownerAssembly = node.Parent.Parent.Parent2;
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
                IAnalysisNode ownerNamespace = node.Parent.Parent.Parent;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.Parent.Parent.Parent2.Identifier.ToString());
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
                var ownerClass = node.Parent.Parent;
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

        private void AddObjectGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (var node in AnalysisNodes)
            {
                var ownerObject = node.Parent;
                GraphNode objectNode = dgmlGraph.Nodes.GetOrCreate(ownerObject.Identifier.ToString(), ownerObject.Name, null);
                objectNode.IsGroup = true;
                objectNode.SetValue("Name", ownerObject.Name);

                GraphNode classNode = dgmlGraph.Nodes.Get(ownerObject.Parent.Identifier.ToString());
                if (classNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(classNode, objectNode, "", catContains);
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
                g.SetValue("NodeType", (node as IHasType).NodeType);
                g.SetValue("OwnerObjectIdentifier", node.Parent.Identifier);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);

                GraphNode objectNode = dgmlGraph.Nodes.Get(node.Parent.Identifier.ToString());
                if (objectNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(objectNode, g, "", catContains);
                }
            }
        }
    }
}
