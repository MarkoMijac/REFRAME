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
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                GraphNode groupNode = _dgmlGraph.Nodes.GetOrCreate(node.OwnerClass.Identifier.ToString(), node.OwnerClass.Name, null);
                groupNode.IsGroup = true;
                groupNode.SetValue("Name", node.OwnerClass.Name);
                groupNode.SetValue("FullName", node.OwnerClass.FullName);
                groupNode.SetValue("Namespace", node.OwnerClass.OwnerNamespace.Name);
                groupNode.SetValue("Assembly", node.OwnerClass.OwnerAssembly.Name);
            }
        }

        private void AddNodes()
        {
            GraphCategory catContains = _dgmlGraph.DocumentSchema.FindCategory("Contains");
            foreach (ClassMemberAnalysisNode node in _analysisNodes)
            {
                GraphNode classNode = _dgmlGraph.Nodes.Get(node.OwnerClass.Identifier.ToString());

                GraphNode g = _dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("NodeType", node.NodeType);
                g.SetValue("ClassIdentifier", node.OwnerClass.Identifier);
                g.SetValue("ClassName", node.OwnerClass.Name);
                g.SetValue("Degree", node.Degree);
                g.SetValue("InDegree", node.InDegree);
                g.SetValue("OutDegree", node.OutDegree);

                _dgmlGraph.Links.GetOrCreate(classNode, g, "", catContains);
            }
        }

        protected override void AddNodesToGraph()
        {
            AddGroupNodes();
            AddNodes();
        }
    }
}
