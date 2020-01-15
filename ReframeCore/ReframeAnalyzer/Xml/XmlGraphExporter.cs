using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeAnalyzer.Graph;
using System.Xml;
using System.IO;

namespace ReframeAnalyzer.Xml
{
    public abstract class XmlGraphExporter : IXmlExporter
    {
        protected XmlWriterSettings _defaultXmlSettings;
        protected IAnalysisGraph _analysisGraph;
        protected IEnumerable<IAnalysisNode> _nodes;

        protected XmlGraphExporter(IAnalysisGraph graph)
        {
            SetDefaultSettings();
            _analysisGraph = graph;
            _nodes = graph.Nodes;
        }

        protected XmlGraphExporter(IAnalysisGraph graph, IEnumerable<IAnalysisNode> nodes)
        {
            SetDefaultSettings();
            _analysisGraph = graph;
            _nodes = nodes;
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
                xmlWriter.WriteStartElement("AnalysisGraph");

                WriteGraphBasicData(xmlWriter);
                WriteAnalysisNodes(xmlWriter);


                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        protected abstract void WriteGraphBasicData(XmlWriter xmlWriter);

        protected abstract void WriteAnalysisNodes( XmlWriter xmlWriter);
    }
}
