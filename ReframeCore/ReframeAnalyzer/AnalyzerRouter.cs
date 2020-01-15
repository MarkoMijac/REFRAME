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
                    case "GetRegisteredReactors": result = Analyzer.GetRegisteredReactors(); break;
                    case "GetRegisteredGraphs": result = Analyzer.GetRegisteredGraphs(); break;
                    case "GetGraphNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            result = Analyzer.GetGraphNodes(reactor.Graph); break;
                        }
                    case "GetClassAnalysisGraph":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);
                            ClassAnalysisGraph analysisGraph = analyzer.GetAnalysisGraph() as ClassAnalysisGraph;
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSourceNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);

                            var analysisGraph = analyzer.GetAnalysisGraph() as ClassAnalysisGraph;
                            Log($"AnalysisGraph created->{analysisGraph!=null}");
                            var sourceNodes = analyzer.GetSourceNodes();
                            Log($"Source nodes created->{sourceNodes!=null}");
                            Log($"# of Source nodes created->{sourceNodes.Count()}");
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sourceNodes);
                            Log($"XmlExporter created->{xmlExporter!=null}");
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
