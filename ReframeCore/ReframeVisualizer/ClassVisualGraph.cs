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

        }

        protected override void AddCustomProperties()
        {
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("PredecessorsCount", System.Type.GetType("System.String"));
            _dgmlGraph.DocumentSchema.Properties.AddNewProperty("SuccessorsCount", System.Type.GetType("System.String"));
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

        protected override void AddNodesToGraph()
        {
            foreach (ClassAnalysisNode node in _analysisNodes)
            {
                GraphNode g = _dgmlGraph.Nodes.GetOrCreate(node.Identifier.ToString(), node.Name, null);
                g.SetValue("Name", node.Name);
                g.SetValue("FullName", node.FullName);
                g.SetValue("Namespace", node.Namespace);
                g.SetValue("Assembly", node.Assembly);
                g.SetValue("PredecessorsCount", node.Predecessors.Count);
                g.SetValue("SuccessorsCount", node.Successors.Count);
            }
        }
    }
}
