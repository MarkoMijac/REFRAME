using Microsoft.VisualStudio.GraphModel;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeTools.Helpers
{
    public static class Visualizer
    {
        public static Graph GenerateDGMLGraph(string xmlData)
        {
            Graph dgmlGraph = null;

            try
            {
                XElement doc = XElement.Parse(xmlData);
                IEnumerable<XElement> nodes = from n in doc.Descendants("Node") select n;

                dgmlGraph = new Graph();
                AddCustomProperties(dgmlGraph);
                AddNodes(dgmlGraph, nodes);
                AddDependencies(dgmlGraph, nodes);

                PaintGraph(dgmlGraph);
            }
            catch (Exception e)
            {


            }


            return dgmlGraph;
        }

        public static Graph GenerateDGMLGraph(IVisualGraph visualGraph)
        {
            Graph graph = visualGraph.GetDGML();
            PaintGraph(graph);

            return graph;
        }

        private static void AddNodes(Graph dgmlGraph, IEnumerable<XElement> nodes)
        {
            foreach (var node in nodes)
            {
                string identifier = node.Element("Identifier").Value;
                string memberName = node.Element("MemberName").Value;
                string ownerClass = node.Element("OwnerClass").Value;
                string ownerObject = node.Element("OwnerObject").Value;
                string level = node.Element("Layer").Value;
                string predecessorsCount = node.Element("PredecessorsCount").Value;
                string successorsCount = node.Element("SuccessorsCount").Value;

                GraphNode g = dgmlGraph.Nodes.GetOrCreate(identifier, memberName, null);
                g.SetValue("MemberName", memberName);
                g.SetValue("OwnerClass", ownerClass);
                g.SetValue("OwnerObject", ownerObject);
                g.SetValue("Layer", level);
                g.SetValue("PredecessorsCount", predecessorsCount);
                g.SetValue("IsInputNode", (predecessorsCount == "0").ToString());
                g.SetValue("SuccessorsCount", successorsCount);
                g.SetValue("IsOutputNode", (successorsCount == "0").ToString());
                g.SetValue("Description", level + " " + memberName);
            }
        }

        private static void AddDependencies(Graph dgmlGraph, IEnumerable<XElement> nodes)
        {
            GraphNode predecessor;
            GraphNode successor;
            foreach (var node in nodes)
            {
                predecessor = dgmlGraph.Nodes.Get(node.Element("Identifier").Value);
                IEnumerable<XElement> successors = node.Descendants("Successor");
                foreach (var s in successors)
                {
                    successor = dgmlGraph.Nodes.Get(s.Element("Identifier").Value);
                    GraphLink dependency = dgmlGraph.Links.GetOrCreate(predecessor, successor);
                }
            }
        }

        private static void AddCustomProperties(Graph dgmlGraph)
        {
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("MemberName", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OwnerClass", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("OwnerObject", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Background", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Description", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("Level", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("PredecessorsCount", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("IsInputNode", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("SuccessorsCount", System.Type.GetType("System.String"));
            dgmlGraph.DocumentSchema.Properties.AddNewProperty("IsOutputNode", System.Type.GetType("System.String"));
        }

        private static void PaintGraph(Graph dgmlGraph)
        {

            return;
            bool isInputNode = false;
            bool isOutputNode = false;

            foreach (GraphNode g in dgmlGraph.Nodes)
            {
                isInputNode = bool.Parse(g.GetValue("IsInputNode").ToString());
                isOutputNode = bool.Parse(g.GetValue("IsOutputNode").ToString());

                if (isInputNode == true)
                {
                    g.SetValue("Background", "#F4FA58");
                }
                else if (isOutputNode == true)
                {
                    g.SetValue("Background", "#FF0000");
                }
            }
        }

        //public static void CreateDgmlFile(string fileName, IDependencyGraph dependencyGraph)
        //{
        //    Graph dgmlGraph = GenerateDGMLGraph(dependencyGraph);
        //    SolutionServices.CreateNewDgmlFile(fileName, dgmlGraph);
        //}
    }
}
