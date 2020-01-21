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
using ReframeExporter;

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
                
                XmlExporter xmlExporter = new XmlExporter();

                string identifier = parameters["GraphIdentifier"];
                var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                var xmlSource = xmlExporter.Export(reactor);

                switch (commandName)
                {
                    case "GetClassAnalysisGraph":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);
                            result = new XmlClassGraphExporter(analysisGraph).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSourceNodes":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);
                            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);
                            result = new XmlClassGraphExporter(analysisGraph, sourceNodes).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSinkNodes":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);
                            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
                            result = new XmlClassGraphExporter(analysisGraph, sinkNodes).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphLeafNodes":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);
                            var sinkNodes = analyzer.GetLeafNodes(analysisGraph);
                            result = new XmlClassGraphExporter(analysisGraph, sinkNodes).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphOrphanNodes":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);

                            var sinkNodes = analyzer.GetOrphanNodes(analysisGraph);
                            result = new XmlClassGraphExporter(analysisGraph, sinkNodes).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphIntermediaryNodes":
                        {
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);

                            var sinkNodes = analyzer.GetIntermediaryNodes(analysisGraph);
                            result = new XmlClassGraphExporter(analysisGraph, sinkNodes).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphPredecessorNodes":
                        {
                            string nodeIdentifier = parameters["NodeIdentifier"];
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);

                            var predecessors = analyzer.GetPredecessors(analysisGraph, nodeIdentifier);
                            result = new XmlClassGraphExporter(analysisGraph, predecessors).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphSuccessorNodes":
                        {
                            string nodeIdentifier = parameters["NodeIdentifier"];
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);

                            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier);
                            result = new XmlClassGraphExporter(analysisGraph, successors).Export();
                            break;
                        }
                    case "GetClassAnalysisGraphNeighbourNodes":
                        {
                            string nodeIdentifier = parameters["NodeIdentifier"];
                            var analyzer = new ClassLevelAnalyzer();
                            var analysisGraph = analyzer.CreateGraph(xmlSource);

                            var neighbours = analyzer.GetNeighbours(analysisGraph, nodeIdentifier);
                            result = new XmlClassGraphExporter(analysisGraph, neighbours).Export();
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
