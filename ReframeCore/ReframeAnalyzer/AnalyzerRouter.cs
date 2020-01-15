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

                            var analysisGraph = analyzer.GetAnalysisGraph();
                            var sourceNodes = analyzer.GetSourceNodes();
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sourceNodes);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSinkNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);

                            var analysisGraph = analyzer.GetAnalysisGraph();
                            var sinkNodes = analyzer.GetSinkNodes();
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sinkNodes);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphLeafNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);

                            var analysisGraph = analyzer.GetAnalysisGraph();
                            var sinkNodes = analyzer.GetLeafNodes();
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sinkNodes);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphOrphanNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);

                            var analysisGraph = analyzer.GetAnalysisGraph();
                            var sinkNodes = analyzer.GetOrphanNodes();
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sinkNodes);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "GetClassAnalysisGraphIntermediaryNodes":
                        {
                            string identifier = parameters["GraphIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            var analyzer = new ClassLevelAnalyzer(reactor.Graph);

                            var analysisGraph = analyzer.GetAnalysisGraph();
                            var sinkNodes = analyzer.GetIntermediaryNodes();
                            var xmlExporter = new XmlClassGraphExporter(analysisGraph, sinkNodes);
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
