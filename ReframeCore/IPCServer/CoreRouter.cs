using ReframeCore;
using ReframeCore.Factories;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IPCServer
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
                            IReadOnlyList<IReactor> reactors = ReactorRegistry.Instance.GetReactors();
                            result = xmlExporter.Export(reactors);
                            break;
                        }
                    case "ExportReactor":
                        {
                            var xmlExporter = new XmlExporter();
                            string identifier = parameters["ReactorIdentifier"];
                            var reactor = ReactorRegistry.Instance.GetReactor(identifier);
                            result = xmlExporter.Export(reactor);
                            break;
                        }
                    default:
                        result = "No such command!";
                        break;
                }
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
            return result;
        }
    }
}
