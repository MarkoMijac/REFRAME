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
        private XmlWriterSettings defaultXmlSettings;

        public XmlExporter()
        {
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            defaultXmlSettings = new XmlWriterSettings()
            {
                Indent = true,
            };
        }

        public string ExportGraphs(IList<IDependencyGraph> graphs)
        {
            StringBuilder sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            using (var xw = XmlWriter.Create(sw, defaultXmlSettings))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("Graphs");

                foreach (var graph in graphs)
                {
                    xw.WriteStartElement("Graph");                  

                    xw.WriteStartElement("Identifier");         
                    xw.WriteString(graph.Identifier.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("Status");             
                    xw.WriteString(graph.Status.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("NodeCount");             
                    xw.WriteString(graph.Nodes.Count.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("Settings");             

                    xw.WriteStartElement("EnableLogging");
                    xw.WriteString(graph.Settings.EnableLogging.ToString());
                    xw.WriteEndElement();

                    xw.WriteEndElement();

                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
                xw.WriteEndDocument();
            }

            return sb.ToString();
        }

        private static XmlWriterSettings DefineDefaultSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Async = true;
            settings.Indent = true;

            return settings;
        }

        private static void WriteNode(INode node, XmlWriter writer, bool flag = true)
        {
            writer.WriteStartElement("node");

            WriteNodeAttributes(node, writer);
            if (flag == true)
            {
                WritePredecessors(node, writer);
                WriteSuccessors(node, writer);
            }

            writer.WriteEndElement();
        }

        private static void WriteNodeAttributes(INode node, XmlWriter writer)
        {
            writer.WriteAttributeString("identifier", node.Identifier.ToString());
            writer.WriteAttributeString("memberName", node.MemberName);
        }

        private static void WritePredecessors(INode node, XmlWriter writer)
        {
            writer.WriteStartElement("predecessors");
            foreach (INode predecessor in node.Predecessors)
            {
                WriteNode(predecessor, writer, false);
            }
            writer.WriteEndElement();
        }

        private static void WriteSuccessors(INode node, XmlWriter writer)
        {
            writer.WriteStartElement("successors");
            foreach (INode successor in node.Successors)
            {
                WriteNode(successor, writer, false);
            }
            writer.WriteEndElement();
        }

        public static void ExportGraph(IDependencyGraph graph)
        {
            XmlWriterSettings defaultSettings = DefineDefaultSettings();

            XmlWriter writer = XmlWriter.Create("text.xml", defaultSettings);
            writer.WriteStartDocument();

            writer.WriteStartElement("Graph");
            writer.WriteAttributeString("Identifier", graph.Identifier.ToString());

            foreach (INode node in graph.Nodes)
            {
                WriteNode(node, writer);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
