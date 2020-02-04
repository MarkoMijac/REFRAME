using ReframeCore.Factories;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeExporter
{
    public class XmlUpdaterInfoExporter : Exporter
    {
        protected string ReactorIdentifier { get; set; }

        public XmlUpdaterInfoExporter(string reactorIdentifier) : base()
        {
            ReactorIdentifier = reactorIdentifier;
        }

        public override string Export()
        {
            IReadOnlyCollection<string> updateInfos = GetUpdateInfo();
            var reactor = ReactorRegistry.Instance.GetReactor(ReactorIdentifier);

            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, _defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("UpdateProcessInfo");

                xmlWriter.WriteStartElement("Reactor");
                WriteBasicReactorData(xmlWriter, reactor);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Graph");
                WriteBasicGraphData(xmlWriter, reactor.Graph);
                xmlWriter.WriteEndElement();

                WriteUpdateInfos(xmlWriter, updateInfos);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private void WriteUpdateInfos(XmlWriter xmlWriter, IReadOnlyCollection<string> updateInfos)
        {
            xmlWriter.WriteStartElement("UpdateInfos");

            foreach (var updateInfo in updateInfos)
            {
                WriteInfo(updateInfo, xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteInfo(string updateInfo, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("UpdateInfo");

            WriteBasicNodeData(updateInfo, xmlWriter);
            WriteBasicUpdateInfo(updateInfo, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteBasicUpdateInfo(string updateInfo, XmlWriter xmlWriter)
        {
            if (updateInfo != "")
            {
                string[] info = updateInfo.Split(';');
                xmlWriter.WriteStartElement("UpdateLayer");
                xmlWriter.WriteString(info[4]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateStartedAt");
                xmlWriter.WriteString(info[5]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateCompletedAt");
                xmlWriter.WriteString(info[6]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateDuration");
                xmlWriter.WriteString(info[7]);
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteBasicNodeData(string updateInfo, XmlWriter xmlWriter)
        {
            if (updateInfo != "")
            {
                string[] info = updateInfo.Split(';');
                xmlWriter.WriteStartElement("Identifier");
                xmlWriter.WriteString(info[0]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("MemberName");
                xmlWriter.WriteString(info[1]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("OwnerObject");
                xmlWriter.WriteString(info[2]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ObjectIdentifier");
                xmlWriter.WriteString(info[3]);
                xmlWriter.WriteEndElement();
            }
        }

        private IReadOnlyCollection<string> GetUpdateInfo()
        {
            IReadOnlyCollection<string> updateInfo = null;

            var reactor = ReactorRegistry.Instance.GetReactor(ReactorIdentifier);
            ILoggable loggable = reactor.Updater as ILoggable;
            if (loggable != null)
            {
                updateInfo = loggable.NodeLog.GetLoggedNodesDetails();
            }

            return updateInfo;
        }
    }
}
