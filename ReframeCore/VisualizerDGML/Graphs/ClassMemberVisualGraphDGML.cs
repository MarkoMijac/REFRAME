using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Nodes;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VisualizerDGML.Graphs
{
    public class ClassMemberVisualGraphDGML : VisualGraphDGML
    {
        public ClassMemberVisualGraphDGML(string reactorIdentifier, IEnumerable<IAnalysisNode> analysisNodes) : base(reactorIdentifier, analysisNodes)
        {
            Options = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            base.AddCustomProperties(dgmlGraph);
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassIdentifier", Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassName", Type.GetType("System.String"));

            dgmlGraph.DocumentSchema.Properties.AddNewProperty("FullName", Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Assembly", Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Namespace", Type.GetType("System.String"));
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
                string label = $"[{node.Parent.Name}].{node.Name}";
                GraphNode classMemberNode = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), label, null);
                classMemberNode.SetValue("Name", node.Name);
                classMemberNode.SetValue("NodeType", (node as IHasType).NodeType);
                classMemberNode.SetValue("ClassIdentifier", node.Parent.Identifier);
                classMemberNode.SetValue("ClassName", node.Parent.Name);
                classMemberNode.SetValue("Namespace", node.Parent.Parent.Name);
                classMemberNode.SetValue("Assembly", node.Parent.Parent2.Name);
                classMemberNode.SetValue("Degree", node.Degree);
                classMemberNode.SetValue("InDegree", node.InDegree);
                classMemberNode.SetValue("OutDegree", node.OutDegree);
                classMemberNode.SetValue("Tag", node.Tag);

                GraphNode classNode = dgmlGraph.Nodes.Get(node.Parent.Identifier.ToString());
                if (classNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(classNode, classMemberNode, "", catContains);
                }
            }
        }

        protected override void AddNodesToGraph(Graph dgmlGraph)
        {
            AddGroupNodes(dgmlGraph);
            AddNodes(dgmlGraph);
        }
    }
}
