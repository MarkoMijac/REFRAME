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
            XmlExporter exporter = new XmlExporter();
            xml = exporter.ExportGraphs(registeredGraphs);

            return xml;
        }

    }
}
