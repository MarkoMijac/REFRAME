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
                            var xmlExporter = new XmlExporter();
                            result = xmlExporter.ExportReactors();
                            break;
                        }
                    case "ExportReactor":
                        {
                            var xmlExporter = new XmlExporter();
                            string identifier = parameters["ReactorIdentifier"];
                            result = xmlExporter.ExportReactor(identifier);
                            break;
                        }
                    case "ExportUpdateInfo":
                        {
                            var xmlExporter = new XmlExporter();
                            string identifier = parameters["ReactorIdentifier"];
                            result = xmlExporter.ExportUpdateInfo(identifier);
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
