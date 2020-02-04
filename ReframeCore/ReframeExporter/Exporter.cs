using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeExporter
{
    public abstract class Exporter : IExporter
    {
        protected XmlWriterSettings _defaultXmlSettings;

        private void SetDefaultSettings()
        {
            _defaultXmlSettings = new XmlWriterSettings()
            {
                Indent = true
            };
        }

        public Exporter()
        {
            SetDefaultSettings();
        }

        public abstract string Export();

        protected void WriteBasicReactorData(XmlWriter xmlWriter, IReactor reactor)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(reactor.Identifier.ToString());
            xmlWriter.WriteEndElement();
        }

        protected void WriteBasicGraphData(XmlWriter xmlWriter, IDependencyGraph graph)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(graph.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NodeCount");
            xmlWriter.WriteString(graph.Nodes.Count.ToString());
            xmlWriter.WriteEndElement();
        }
    }
}
