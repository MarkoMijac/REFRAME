using ReframeCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeAnalyzer.Xml
{
    public class XmlReactorExporter : IXmlExporter
    {
        protected XmlWriterSettings _defaultXmlSettings;
        private IReadOnlyList<IReactor> _reactors = null;

        public XmlReactorExporter(IReadOnlyList<IReactor> reactors)
        {
            SetDefaultSettings();
            _reactors = reactors;
        }

        private void SetDefaultSettings()
        {
            _defaultXmlSettings = new XmlWriterSettings()
            {
                Indent = true
            };
        }

        public string Export()
        {
            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, _defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Reactors");

                foreach (var reactor in _reactors)
                {
                    var graph = reactor.Graph;

                    xmlWriter.WriteStartElement("Reactor");

                    xmlWriter.WriteStartElement("Identifier");
                    xmlWriter.WriteString(reactor.Identifier.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("GraphIdentifier");
                    xmlWriter.WriteString(graph.Identifier.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("GraphNodeCount");
                    xmlWriter.WriteString(graph.Nodes.Count.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }
    }
}
