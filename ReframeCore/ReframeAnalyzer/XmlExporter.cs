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
            xmlWriter.WriteString(node.GetPredecessors().Count.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("SuccessorsCount");
            xmlWriter.WriteString(node.GetSuccessors().Count.ToString());
            xmlWriter.WriteEndElement();
        }

        private static void WritePredecessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (INode predecessor in node.GetPredecessors())
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
            foreach (INode successor in node.GetSuccessors())
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

        public static string ExportClassNodes(IList<StaticNode> nodes)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("StaticNodes");

                foreach (var node in nodes)
                {
                    WriteClassNode(node, xmlWriter);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private static void WriteClassNode(StaticNode classNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("StaticNode");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(classNode.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(classNode.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("FullName");
            xmlWriter.WriteString(classNode.FullName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Namespace");
            xmlWriter.WriteString(classNode.Namespace);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Assembly");
            xmlWriter.WriteString(classNode.Assembly);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }
    }
}
