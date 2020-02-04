using ReframeCore;
using ReframeCore.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeExporter
{
    public class XmlReactorsExporter : Exporter
    {
        public XmlReactorsExporter() : base()
        {
            
        }

        public override string Export()
        {
            var reactors = ReactorRegistry.Instance.GetReactors();
            return Export(reactors);
        }

        private string Export(IReadOnlyList<IReactor> reactors)
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
    }
}
