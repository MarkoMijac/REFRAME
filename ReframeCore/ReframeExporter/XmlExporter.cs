using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeExporter
{
    public class XmlExporter : IExporter
    {
        protected XmlWriterSettings _defaultXmlSettings;

        private void SetDefaultSettings()
        {
            _defaultXmlSettings = new XmlWriterSettings()
            {
                Indent = true
            };
        }

        public XmlExporter()
        {
            SetDefaultSettings();
        }

        public string Export(IReadOnlyList<IReactor> reactors)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, _defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Reactors");

                foreach (var reactor in reactors)
                {
                    var graph = reactor.Graph;

                    xmlWriter.WriteStartElement("Reactor");
                    WriteBasicReactorData(xmlWriter, reactor);

                    xmlWriter.WriteStartElement("Graph");
                    WriteBasicGraphData(xmlWriter, reactor.Graph);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        public string Export(IReactor reactor)
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, _defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Reactor");

                WriteBasicReactorData(xmlWriter, reactor);
                WriteGraph(xmlWriter, reactor.Graph);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private void WriteBasicReactorData(XmlWriter xmlWriter, IReactor reactor)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(reactor.Identifier.ToString());
            xmlWriter.WriteEndElement();
        }

        private void WriteGraph(XmlWriter xmlWriter, IDependencyGraph graph)
        {
            xmlWriter.WriteStartElement("Graph");

            WriteBasicGraphData(xmlWriter, graph);
            WriteNodes(xmlWriter, graph.Nodes);

            xmlWriter.WriteEndElement();
        }

        private void WriteBasicGraphData(XmlWriter xmlWriter, IDependencyGraph graph)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(graph.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NodeCount");
            xmlWriter.WriteString(graph.Nodes.Count.ToString());
            xmlWriter.WriteEndElement();
        }

        private void WriteNodes(XmlWriter xmlWriter, IList<INode> nodes)
        {
            xmlWriter.WriteStartElement("Nodes");

            foreach (var node in nodes)
            {
                WriteNode(node, xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteNode(INode node, XmlWriter xmlWriter, bool includePredecessors = true, bool includeSuccessors = true)
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

        private void WriteNodeBasicData(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(node.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MemberName");
            xmlWriter.WriteString(node.MemberName);
            xmlWriter.WriteEndElement();
        }

        private void WriteNodeDetails(INode node, XmlWriter xmlWriter)
        {
            WriteClassDetails(node, xmlWriter);

            xmlWriter.WriteStartElement("OwnerObject");
            uint objectIdentifier = (uint)node.OwnerObject.GetHashCode();
            xmlWriter.WriteString(objectIdentifier.ToString());
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

        private void WriteClassDetails(INode node, XmlWriter xmlWriter)
        {
            Type type = node.OwnerObject.GetType();

            xmlWriter.WriteStartElement("OwnerClass");

            xmlWriter.WriteStartElement("Identifier");
            uint classIdentifier = (uint)type.GUID.GetHashCode();
            xmlWriter.WriteString(classIdentifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(type.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("FullName");
            xmlWriter.WriteString(type.FullName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Namespace");
            xmlWriter.WriteString(type.Namespace);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Assembly");
            xmlWriter.WriteString(type.Assembly.ManifestModule.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private void WriteSuccessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successors");
            foreach (INode successor in node.Successors)
            {
                WriteSuccessor(successor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WriteSuccessor(INode successor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successor");

            WriteNodeBasicData(successor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WritePredecessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (INode predecessor in node.Predecessors)
            {
                WritePredecessor(predecessor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WritePredecessor(INode predecessor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessor");

            WriteNodeBasicData(predecessor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        

        
    }
}
