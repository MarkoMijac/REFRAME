using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReframeVisualizer
{
    public class ClassMemberVisualGraph : VisualGraph
    {
        public ClassMemberVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            base.AddCustomProperties(dgmlGraph);
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassIdentifier", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassName", System.Type.GetType("System.String"));

            dgmlGraph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
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

        private void AddGroupNodes(Graph dgmlGraph)
        {
            if (VisualizationOptions.GroupingLevel == GroupingLevel.NoGrouping)
            {

            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.ClassLevel)
            {
                AddClassGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                AssemblyAnalysisNode ownerAssembly = node.OwnerClass.OwnerAssembly;
                GraphNode groupNode = dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                IAnalysisNode ownerNamespace = node.OwnerClass.Parent;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.OwnerClass.OwnerAssembly.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddClassGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                ClassAnalysisNode ownerClass = node.OwnerClass;
                GraphNode classNode = dgmlGraph.Nodes.GetOrCreate(ownerClass.Identifier.ToString(), ownerClass.Name, null);
                classNode.IsGroup = true;
                classNode.SetValue("Name", ownerClass.Name);
                classNode.SetValue("FullName", ownerClass.FullName);
                classNode.SetValue("Namespace", ownerClass.Parent.Name);
                classNode.SetValue("Assembly", ownerClass.OwnerAssembly.Name);

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
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                string label = $"[{node.OwnerClass.Name}].{node.Name}";
                GraphNode classMemberNode = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), label, null);
                classMemberNode.SetValue("Name", node.Name);
                classMemberNode.SetValue("NodeType", node.NodeType);
                classMemberNode.SetValue("ClassIdentifier", node.OwnerClass.Identifier);
                classMemberNode.SetValue("ClassName", node.OwnerClass.Name);
                classMemberNode.SetValue("Namespace", node.OwnerClass.Parent.Name);
                classMemberNode.SetValue("Assembly", node.OwnerClass.OwnerAssembly.Name);
                classMemberNode.SetValue("Degree", node.Degree);
                classMemberNode.SetValue("InDegree", node.InDegree);
                classMemberNode.SetValue("OutDegree", node.OutDegree);
                classMemberNode.SetValue("Tag", node.Tag);

                GraphNode classNode = dgmlGraph.Nodes.Get(node.OwnerClass.Identifier.ToString());
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
