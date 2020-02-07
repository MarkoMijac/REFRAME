using IPCServer;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeServer
{
    public class CoreRouter : CommandRouter
    {
        public CoreRouter()
        {
            Identifier = "CoreRouter";
        }

        public override string RouteCommand(string command)
        {
            string result = "";

            try
            {
                XmlDocument doc = GetCommandXmlDocument(command);
                string commandName = GetCommandName(doc);
                Dictionary<string, string> parameters = GetCommandParameters(doc);

                switch (commandName)
                {
                    case "ExportRegisteredReactors":
                        {
                            var xmlExporter = new XmlReactorsExporter();
                            result = xmlExporter.Export();
                            break;
                        }
                    case "ExportReactor":
                        {
                            string identifier = parameters["ReactorIdentifier"];
                            var xmlExporter = new XmlReactorDetailExporter(identifier);
                            result = xmlExporter.Export();
                            break;
                        }
                    case "ExportUpdateInfo":
                        {
                            string identifier = parameters["ReactorIdentifier"];
                            var xmlExporter = new XmlUpdaterInfoExporter(identifier);
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
                string errorMessage = $"Error: {e.Message}"+Environment.NewLine;
                errorMessage += $"StackTrace:{e.StackTrace}";
                return errorMessage;
            }
            return result;
        }
    }
}
