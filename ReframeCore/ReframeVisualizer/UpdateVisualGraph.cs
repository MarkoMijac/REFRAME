using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class UpdateVisualGraph : VisualGraph
    {
        public UpdateVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel, GroupingLevel.ObjectLevel }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            base.AddCustomProperties(dgmlGraph);
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OwnerObjectIdentifier", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("IsInitialNode", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("UpdateOrder", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("UpdateLayer", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("UpdateStartedAt", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("UpdateCompletedAt", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("UpdateDuration", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("CurrentValue", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("PreviousValue", System.Type.GetType("System.String"));
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

        private void PaintInitialNode(Graph dgmlGraph)
        {
            GraphNode initialNode = dgmlGraph.Nodes.FirstOrDefault(n => n.GetValue("IsInitialNode") != null && n.GetValue("IsInitialNode").ToString().ToLower() == "true");
            if (initialNode != null)
            {
                var painter = new GraphPainter();
                painter.Paint(dgmlGraph, initialNode, "#FF339933");
            }
        }

        protected override void PaintGraph(Graph dgmlGraph)
        {
            base.PaintGraph(dgmlGraph);
            PaintInitialNode(dgmlGraph);
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
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.ObjectLevel)
            {
                AddObjectGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.ClassLevel)
            {
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.NamespaceLevel)
            {
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
            else if (VisualizationOptions.GroupingLevel == GroupingLevel.AssemblyLevel)
            {
                AddAssemblyGroups(dgmlGraph);
                AddNamespaceGroups(dgmlGraph);
                AddClassGroups(dgmlGraph);
                AddObjectGroups(dgmlGraph);
            }
        }

        private void AddAssemblyGroups(Graph dgmlGraph)
        {
            foreach (UpdateAnalysisNode node in _analysisNodes)
            {
                var objectMemberNode = node.Parent;
                IAnalysisNode ownerAssembly = objectMemberNode.Parent.Parent.Parent2;
                GraphNode groupNode = dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (UpdateAnalysisNode node in _analysisNodes)
            {
                var objectMemberNode = node.Parent;
                IAnalysisNode ownerNamespace = objectMemberNode.Parent.Parent.Parent;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(objectMemberNode.Parent.Parent.Parent2.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddClassGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (UpdateAnalysisNode node in _analysisNodes)
            {
                var objectMemberNode = node.Parent;
                var ownerClass = objectMemberNode.Parent.Parent;
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

            foreach (UpdateAnalysisNode node in _analysisNodes)
            {
                var objectMemberNode = node.Parent;
                var ownerObject = objectMemberNode.Parent;
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
            foreach (UpdateAnalysisNode node in _analysisNodes)
            {
                var objectMemberNode = node.Parent;

                GraphNode g = dgmlGraph.Nodes.GetOrCreate(objectMemberNode.Identifier.ToString(), $"[{node.UpdateOrder}] {node.Name}", null);
                g.SetValue("Name", node.Name);
                g.SetValue("NodeType", node.NodeType);
                g.SetValue("OwnerObjectIdentifier", objectMemberNode.Parent.Identifier);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);
                g.SetValue("Tag", node.Tag);
                g.SetValue("IsInitialNode", node.IsInitialNode.ToString());
                g.SetValue("UpdateOrder", node.UpdateOrder);
                g.SetValue("UpdateLayer", node.UpdateLayer);
                g.SetValue("UpdateStartedAt", node.UpdateStartedAt);
                g.SetValue("UpdateCompletedAt", node.UpdateCompletedAt);
                g.SetValue("UpdateDuration", node.UpdateDuration);
                g.SetValue("CurrentValue", node.CurrentValue);
                g.SetValue("PreviousValue", node.CurrentValue);

                GraphNode objectNode = dgmlGraph.Nodes.Get(objectMemberNode.Parent.Identifier.ToString());
                if (objectNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(objectNode, g, "", catContains);
                }
            }
        }
    }
}
