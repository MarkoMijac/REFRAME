using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IPCClient
{
    public class ServerCommand
    {
        public string RouterIdentifier { get; set; }
        public string CommandName { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        private string Serialize()
        {
            StringBuilder builder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ServerCommand");

                xmlWriter.WriteStartElement("RouterIdentifier");
                xmlWriter.WriteString(RouterIdentifier);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("CommandName");
                xmlWriter.WriteString(CommandName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Parameters");
                foreach (var p in Parameters)
                {
                    xmlWriter.WriteStartElement("Parameter");
                    xmlWriter.WriteStartElement("Name");
                    xmlWriter.WriteString(p.Key);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("Value");
                    xmlWriter.WriteString(p.Value);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return Serialize();
        }
    }
}
