using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.GraphModel;

namespace ReframeVisualizer
{
    public class ClassVisualGraph : VisualGraph
    {
        public ClassVisualGraph(string xml)
            :base(xml)
        {

        }

        protected override void AddCustomProperties(Graph graph)
        {
            graph.DocumentSchema.Properties.AddNewProperty("Name", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("FullName", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Namespace", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("Assembly", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("PredecessorsCount", System.Type.GetType("System.String"));
            graph.DocumentSchema.Properties.AddNewProperty("SuccessorsCount", System.Type.GetType("System.String"));
        }

        protected override void AddDependenciesToGraph(Graph graph, IEnumerable<XElement> nodes)
        {
            GraphNode predecessor;
            GraphNode successor;
            foreach (var node in nodes)
            {
                predecessor = graph.Nodes.Get(node.Element("Identifier").Value);
                IEnumerable<XElement> successors = node.Descendants("Successor");
                foreach (var s in successors)
                {
                    successor = graph.Nodes.Get(s.Element("Identifier").Value);
                    GraphLink dependency = graph.Links.GetOrCreate(predecessor, successor);
                }
            }
        }

        protected override void AddNodesToGraph(Graph graph, IEnumerable<XElement> nodes)
        {
            foreach (var node in nodes)
            {
                string identifier = node.Element("Identifier").Value;
                string name = node.Element("Name").Value;
                string fullName = node.Element("FullName").Value;
                string namespc = node.Element("Namespace").Value;
                string assembly = node.Element("Assembly").Value;
                string predecessorsCount = node.Element("PredecessorsCount").Value;
                string successorsCount = node.Element("SuccessorsCount").Value;

                GraphNode g = graph.Nodes.GetOrCreate(identifier, name, null);
                g.SetValue("Name", name);
                g.SetValue("FullName", fullName);
                g.SetValue("Namespace", namespc);
                g.SetValue("Assembly", assembly);
                g.SetValue("PredecessorsCount", predecessorsCount);
                g.SetValue("SuccessorsCount", successorsCount);
            }
        }

        protected override IEnumerable<XElement> FetchNodes(XElement document)
        {
            IEnumerable<XElement> nodes = from n in document.Descendants("Node") select n;

            return nodes;
        }
    }
}
