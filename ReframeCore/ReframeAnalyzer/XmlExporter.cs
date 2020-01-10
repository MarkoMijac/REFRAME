using ReframeAnalyzer.Graph;
using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeAnalyzer
{
    public class XmlExporter
    {
        private static XmlWriterSettings defaultXmlSettings;

        static XmlExporter()
        {
            SetDefaultSettings();
        }

        private static void SetDefaultSettings()
        {
            defaultXmlSettings = new XmlWriterSettings()
            {
                Indent = true
            };
        }

        public static string ExportGraphs(IReadOnlyList<IReactor> reactors)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Graphs");

                foreach (var reactor in reactors)
                {
                    var graph = reactor.Graph;

                    xmlWriter.WriteStartElement("Graph");                  

                    xmlWriter.WriteStartElement("Identifier");         
                    xmlWriter.WriteString(graph.Identifier.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("NodeCount");             
                    xmlWriter.WriteString(graph.Nodes.Count.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        public static string ExportNodes(IList<INode> nodes)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Nodes");

                foreach (var node in nodes)
                {
                    WriteNode(node, xmlWriter);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private static void WriteNode(INode node, XmlWriter xmlWriter, bool includePredecessors = true, bool includeSuccessors = true)
        {
            xmlWriter.WriteStartElement("Node");

            WriteNodeBasicData(node, xmlWriter);
            WriteNodeDetails(node, xmlWriter);

            if (includePredecessors == true)
            {
                WritePredecessors(node, xmlWriter);
            }

            if (includeSuccessors == true)
            {
                WriteSuccessors(node, xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        private static void WriteNodeBasicData(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(node.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MemberName");
            xmlWriter.WriteString(node.MemberName);
            xmlWriter.WriteEndElement();
        }

        private static void WriteNodeDetails(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("OwnerClass");
            xmlWriter.WriteString(node.OwnerObject.GetType().ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("OwnerObject");
            xmlWriter.WriteString(node.OwnerObject.GetHashCode().ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Layer");
            xmlWriter.WriteString(node.Layer.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NodeType");
            xmlWriter.WriteString(node.GetType().ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("PredecessorsCount");
            xmlWriter.WriteString(node.Predecessors.Count.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("SuccessorsCount");
            xmlWriter.WriteString(node.Successors.Count.ToString());
            xmlWriter.WriteEndElement();
        }

        private static void WritePredecessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (INode predecessor in node.Predecessors)
            {
                WritePredecessor(predecessor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void WritePredecessor(INode predecessor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessor");

            WriteNodeBasicData(predecessor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private static void WriteSuccessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successors");
            foreach (INode successor in node.Successors)
            {
                WriteSuccessor(successor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void WriteSuccessor(INode successor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successor");

            WriteNodeBasicData(successor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        #region Methods

        public static string ExportGraph(IAnalysisGraph analysisGraph)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("AnalysisGraph");

                WriteGraphBasicData(analysisGraph, xmlWriter);
                WriteAnalysisNodes(analysisGraph, xmlWriter);
                

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private static void WriteGraphBasicData(IAnalysisGraph analysisGraph, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("GraphIdentifier");
            xmlWriter.WriteString(analysisGraph.DependencyGraph.Identifier);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NodeCount");
            xmlWriter.WriteString(analysisGraph.DependencyGraph.Nodes.Count.ToString());
            xmlWriter.WriteEndElement();
        }

        private static void WriteAnalysisNodes(IAnalysisGraph analysisGraph, XmlWriter xmlWriter)
        {
            foreach (var node in analysisGraph.Nodes)
            {
                WriteAnalysisNode(node, xmlWriter);
            }
        }

        private static void WriteAnalysisNode(IAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("StaticNode");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(analysisNode.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(analysisNode.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("PredecessorsCount");
            xmlWriter.WriteString(analysisNode.Predecessors.Count.ToString());
            xmlWriter.WriteEndElement();

            WritePredecessors(analysisNode, xmlWriter);

            xmlWriter.WriteStartElement("SuccessorsCount");
            xmlWriter.WriteString(analysisNode.Successors.Count.ToString());
            xmlWriter.WriteEndElement();

            WriteSuccessors(analysisNode, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private static void WritePredecessors(IAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (IAnalysisNode predecessor in analysisNode.Predecessors)
            {
                WritePredecessor(predecessor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void WritePredecessor(IAnalysisNode predecessor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessor");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(predecessor.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(predecessor.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteSuccessors(IAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successors");
            foreach (IAnalysisNode successor in analysisNode.Successors)
            {
                WriteSuccessor(successor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void WriteSuccessor(IAnalysisNode successor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successor");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(successor.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(successor.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
