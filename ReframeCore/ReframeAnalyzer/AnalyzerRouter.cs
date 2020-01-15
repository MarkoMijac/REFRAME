using IPCServer;
using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ReframeCore.Factories;
using ReframeAnalyzer.Xml;
using ReframeAnalyzer.Graph;

namespace ReframeAnalyzer
{
    public class AnalyzerRouter : CommandRouter
    {
        private static string _log;

        private static void ClearLog()
        {
            _log = "";
        }

        private static void Log(string entry)
        {
            _log += entry+";"+Environment.NewLine;
        }

        public AnalyzerRouter()
        {
            Identifier = "AnalyzerRouter";
        }

        public override string RouteCommand(string commandXml)
        {
            string result = "";

            try
            {
                XmlDocument doc = GetCommandXmlDocument(commandXml);
                string commandName = GetCommandName(doc);
                Dictionary<string, string> parameters = GetCommandParameters(doc);

                switch (commandName)
                {
                    case "GetRegisteredReactors": result = new ClassLevelAnalyzer().GetRegisteredReactors(); break;
                    case "GetRegisteredGraphs": result = new ClassLevelAnalyzer().GetRegisteredGraphs(); break;
                    case "GetGraphNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            result = new ClassLevelAnalyzer().GetGraphNodes(reactor.Graph); break;
                        }
                    case "GetClassAnalysisGraph":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer();
                            ClassAnalysisGraph analysisGraph = analyzer.GetAnalysisGraph(reactor.Graph) as ClassAnalysisGraph;
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSourceNodes":
                        {
                            ClearLog();
                            Log("Enter GetClassAnalysisGraphSourceNodes");
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            Log($"Reactor fetched -> {reactor.Identifier}");
                            Log($"Graph fetched -> {reactor.Graph!=null}");
                            var analyzer = new ClassLevelAnalyzer();
                            Log($"Analyzer is created -> {analyzer != null}");
                            ClassAnalysisGraph analysisGraph = analyzer.GetSourceNodes(reactor.Graph) as ClassAnalysisGraph;
                            Log($"Analysis graph created -> {analysisGraph != null}");
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph);
                            Log($"XML Exporter created -> {xmlExporter != null}");
                            result = xmlExporter.Export();
                            break;
                        }
                    default:
                        result = "No such command!";
                        break;
                }
            }
            catch (Exception e)
            {

                return "Error: " + e.Message + "; Log: " + _log;
            }

            return result;
        }
    }
}
