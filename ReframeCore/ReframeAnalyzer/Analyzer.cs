using ReframeCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReframeAnalyzer
{
    public static class Analyzer
    {
        public static string GetRegisteredGraphs()
        {
            string xml = "";

            List<IDependencyGraph> registeredGraphs = GraphFactory.GetRegisteredGraphs();
            xml = XmlExporter.ExportGraphs(registeredGraphs);

            return xml;
        }

        public static string GetGraphNodes(IDependencyGraph graph)
        {
            string xml = "";

            xml = XmlExporter.ExportNodes(graph.Nodes);

            return xml;
        }

    }
}
