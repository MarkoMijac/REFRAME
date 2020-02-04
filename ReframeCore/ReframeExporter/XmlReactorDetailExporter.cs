using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
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
    public class XmlReactorDetailExporter : Exporter
    {
        protected string ReactorIdentifier { get; set; }

        public XmlReactorDetailExporter(string reactorIdentifier) : base()
        {
            ReactorIdentifier = reactorIdentifier;
        }

        private string Export(IReactor reactor)
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

        private void WriteGraph(XmlWriter xmlWriter, IDependencyGraph graph)
        {
            xmlWriter.WriteStartElement("Graph");

            WriteBasicGraphData(xmlWriter, graph);
            WriteNodes(xmlWriter, graph.Nodes);

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
            if (node != null)
            {
                xmlWriter.WriteStartElement("Identifier");
                xmlWriter.WriteString(node.Identifier.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("MemberName");
                xmlWriter.WriteString(node.MemberName);
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteNodeDetails(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("NodeType");
            xmlWriter.WriteString(node.GetType().Name);
            xmlWriter.WriteEndElement();

            WriteObjectDetails(node.OwnerObject, xmlWriter);
        }

        private void WriteObjectDetails(object ownerObject, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("OwnerObject");

            xmlWriter.WriteStartElement("Identifier");
            uint objectIdentifier = (uint)ownerObject.GetHashCode();
            xmlWriter.WriteString(objectIdentifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(ownerObject.ToString());
            xmlWriter.WriteEndElement();

            WriteClassDetails(ownerObject.GetType(), xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteClassDetails(Type type, XmlWriter xmlWriter)
        {
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

            WriteNamespaceDetails(type, xmlWriter);
            WriterAssemblyDetails(type, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriterAssemblyDetails(Type type, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("OwnerAssembly");

            xmlWriter.WriteStartElement("Identifier");
            uint assemblyIdentifier = (uint)type.Assembly.GetHashCode();
            xmlWriter.WriteString(assemblyIdentifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(type.Assembly.ManifestModule.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private void WriteNamespaceDetails(Type type, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("OwnerNamespace");

            xmlWriter.WriteStartElement("Identifier");
            uint namespaceIdentifier = (uint)type.Namespace.GetHashCode();
            xmlWriter.WriteString(namespaceIdentifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(type.Namespace);
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

        public override string Export()
        {
            var reactor = ReactorRegistry.Instance.GetReactor(ReactorIdentifier);
            return Export(reactor);
        }
    }
}
