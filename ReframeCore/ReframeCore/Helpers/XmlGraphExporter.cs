using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeCore.Helpers
{
    public static class XmlGraphExporter
    {
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
