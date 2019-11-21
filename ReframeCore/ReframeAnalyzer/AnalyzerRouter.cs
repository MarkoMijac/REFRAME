using IPCServer;
using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ReframeCore.Factories;

namespace ReframeAnalyzer
{
    public class AnalyzerRouter : CommandRouter
    {
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
                    case "GetRegisteredGraphs": result = Analyzer.GetRegisteredGraphs(); break;
                    case "GetGraphNodes":
                        {
                            string graphIdentifier = parameters["GraphIdentifier"];
                            var graph = GraphRegistry.Instance.GetGraph(graphIdentifier);
                            result = Analyzer.GetGraphNodes(graph); break;
                        }
                    default:
                        result = "No such command!";
                        break;
                }
            }
            catch (Exception e)
            {

                return "Error" + e.Message;
            }

            

            

            return result;
        }
    }
}
