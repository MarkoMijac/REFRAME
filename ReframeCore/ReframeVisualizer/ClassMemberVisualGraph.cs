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

        protected override void AddCustomProperties()
        {
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassIdentifier", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("ClassName", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Degree", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("InDegree", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("OutDegree", System.Type.GetType("System.String"));

            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
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

        private void AddGroupNodes()
        {
            if (VisualizationOptions.GroupingLevel == GroupingLevel.NoGrouping)
            {

            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.ClassLevel)
            {
                AddClassGroups();
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups();
                AddClassGroups();
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups();
                AddNamespaceGroups();
                AddClassGroups();
            }
        }

        private void AddAssemblyGroups()
        {
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                AssemblyAnalysisNode ownerAssembly = node.OwnerClass.OwnerAssembly;
                GraphNode groupNode = _dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups()
        {
            GraphCategory catContains = _dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                NamespaceAnalysisNode ownerNamespace = node.OwnerClass.OwnerNamespace;
                GraphNode namespaceNode = _dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = _dgmlGraph.Nodes.Get(node.OwnerClass.OwnerAssembly.Identifier.ToString());
                if (assembyNode != null)
                {
                    _dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddClassGroups()
        {
            GraphCategory catContains = _dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                ClassAnalysisNode ownerClass = node.OwnerClass;
                GraphNode classNode = _dgmlGraph.Nodes.GetOrCreate(ownerClass.Identifier.ToString(), ownerClass.Name, null);
                classNode.IsGroup = true;
                classNode.SetValue("Name", ownerClass.Name);
                classNode.SetValue("FullName", ownerClass.FullName);
                classNode.SetValue("Namespace", ownerClass.OwnerNamespace.Name);
                classNode.SetValue("Assembly", ownerClass.OwnerAssembly.Name);

                GraphNode namespaceNode = _dgmlGraph.Nodes.Get(ownerClass.OwnerNamespace.Identifier.ToString());
                if (namespaceNode != null)
                {
                    _dgmlGraph.Links.GetOrCreate(namespaceNode, classNode, "", catContains);
                }
            }
        }

        private void AddNodes()
        {
            GraphCategory catContains = _dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                GraphNode classMemberNode = _dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                classMemberNode.SetValue("Name", node.Name);
                classMemberNode.SetValue("NodeType", node.NodeType);
                classMemberNode.SetValue("ClassIdentifier", node.OwnerClass.Identifier);
                classMemberNode.SetValue("ClassName", node.OwnerClass.Name);
                classMemberNode.SetValue("Degree", node.Degree);
                classMemberNode.SetValue("InDegree", node.InDegree);
                classMemberNode.SetValue("OutDegree", node.OutDegree);

                GraphNode classNode = _dgmlGraph.Nodes.Get(node.OwnerClass.Identifier.ToString());
                if (classNode != null)
                {
                    _dgmlGraph.Links.GetOrCreate(classNode, classMemberNode, "", catContains);
                }
            }
        }

        protected override void AddNodesToGraph()
        {
            AddGroupNodes();
            AddNodes();
        }
    }
}
