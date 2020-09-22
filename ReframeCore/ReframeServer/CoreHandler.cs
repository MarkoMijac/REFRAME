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
    internal class CoreHandler : CommandHandler
    {
        public CoreHandler()
        {
            Identifier = "CoreHandler";
        }

        protected override IExporter GetExporter(string command)
        {
            IExporter exporter = null;

            XmlDocument doc = GetCommandXmlDocument(command);
            string commandName = GetCommandName(doc);
            Dictionary<string, string> parameters = GetCommandParameters(doc);

            switch (commandName)
            {
                case "ExportRegisteredReactors": 
                    { 
                        exporter = new XmlReactorsExporter();
                        break;
                    }
                case "ExportReactor":
                    {
                        string identifier = parameters["ReactorIdentifier"];
                        exporter = new XmlReactorDetailExporter(identifier);
                        break;
                    }
                case "ExportUpdateInfo":
                    {
                        string identifier = parameters["ReactorIdentifier"];
                        exporter = new XmlUpdaterInfoExporter(identifier);
                        break;
                    }
                default:
                    exporter = null;
                    break;
            }

            return exporter;
        }
    }
}
