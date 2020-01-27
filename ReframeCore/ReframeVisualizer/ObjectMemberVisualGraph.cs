﻿using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class ObjectMemberVisualGraph : VisualGraph
    {
        public ObjectMemberVisualGraph(IEnumerable<IAnalysisNode> analysisNodes) : base(analysisNodes)
        {
            VisualizationOptions = new VisualizationOptions
            {
                AllowedGroupingLevels = new List<GroupingLevel>() { GroupingLevel.NoGrouping, GroupingLevel.AssemblyLevel, GroupingLevel.NamespaceLevel, GroupingLevel.ClassLevel, GroupingLevel.ObjectLevel }
            };
        }

        protected override void AddCustomProperties(Graph dgmlGraph)
        {
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("NodeType", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OwnerObjectIdentifier", System.Type.GetType("System.String"));
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
            foreach (ObjectMemberAnalysisNode node in _analysisNodes)
            {
                AssemblyAnalysisNode ownerAssembly = node.OwnerObject.OwnerClass.OwnerAssembly;
                GraphNode groupNode = dgmlGraph.Nodes.GetOrCreate(ownerAssembly.Identifier.ToString(), ownerAssembly.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", ownerAssembly.Name);
            }
        }

        private void AddNamespaceGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ObjectMemberAnalysisNode node in _analysisNodes)
            {
                NamespaceAnalysisNode ownerNamespace = node.OwnerObject.OwnerClass.OwnerNamespace;
                GraphNode namespaceNode = dgmlGraph.Nodes.GetOrCreate(ownerNamespace.Identifier.ToString(), ownerNamespace.Name, null);
                namespaceNode.IsGroup = true;
                namespaceNode.SetValue("Name", ownerNamespace.Name);

                GraphNode assembyNode = dgmlGraph.Nodes.Get(node.OwnerObject.OwnerClass.OwnerAssembly.Identifier.ToString());
                if (assembyNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(assembyNode, namespaceNode, "", catContains);
                }
            }
        }

        private void AddClassGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ObjectMemberAnalysisNode node in _analysisNodes)
            {
                ClassAnalysisNode ownerClass = node.OwnerObject.OwnerClass;
                GraphNode classNode = dgmlGraph.Nodes.GetOrCreate(ownerClass.Identifier.ToString(), ownerClass.Name, null);
                classNode.IsGroup = true;
                classNode.SetValue("Name", ownerClass.Name);
                classNode.SetValue("FullName", ownerClass.FullName);
                classNode.SetValue("Namespace", ownerClass.OwnerNamespace.Name);
                classNode.SetValue("Assembly", ownerClass.OwnerAssembly.Name);

                GraphNode namespaceNode = dgmlGraph.Nodes.Get(ownerClass.OwnerNamespace.Identifier.ToString());
                if (namespaceNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(namespaceNode, classNode, "", catContains);
                }
            }
        }

        private void AddObjectGroups(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");

            foreach (ObjectMemberAnalysisNode node in _analysisNodes)
            {
                ObjectAnalysisNode ownerObject = node.OwnerObject;
                GraphNode objectNode = dgmlGraph.Nodes.GetOrCreate(ownerObject.Identifier.ToString(), ownerObject.Name, null);
                objectNode.IsGroup = true;
                objectNode.SetValue("Name", ownerObject.Name);

                GraphNode classNode = dgmlGraph.Nodes.Get(ownerObject.OwnerClass.Identifier.ToString());
                if (classNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(classNode, objectNode, "", catContains);
                }
            }
        }

        private void AddNodes(Graph dgmlGraph)
        {
            GraphCategory catContains = dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (ObjectMemberAnalysisNode node in _analysisNodes)
            {
                GraphNode g = dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("NodeType", node.NodeType);
                g.SetValue("OwnerObjectIdentifier", node.OwnerObject.Identifier);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);

                GraphNode objectNode = dgmlGraph.Nodes.Get(node.OwnerObject.Identifier.ToString());
                if (objectNode != null)
                {
                    dgmlGraph.Links.GetOrCreate(objectNode, g, "", catContains);
                }
            }
        }
    }
}
