using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IPCServer
{
    public abstract class CommandRouter : ICommandRouter
    {
        public string Identifier { get; set; }

        protected static XmlDocument GetCommandXmlDocument(string commandXml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(commandXml);
            return document;
        }

        protected string GetCommandName(XmlDocument serverCommandXml)
        {
            XmlNode commandName = serverCommandXml.GetElementsByTagName("CommandName").Item(0);
            return commandName.InnerText;
        }

        protected Dictionary<string, string> GetCommandParameters(XmlDocument serverCommandXml)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            XmlNode xmlNodeParameters = serverCommandXml.GetElementsByTagName("Parameters").Item(0);


            foreach (XmlNode xmlNode in xmlNodeParameters.ChildNodes)
            {
                string name = xmlNode.ChildNodes[0].InnerText;
                string value = xmlNode.ChildNodes[1].InnerText;

                parameters.Add(name, value);
            }

            return parameters;
        }

        public static string GetRouterIdentifier(string commandXml)
        {
            XmlDocument doc = GetCommandXmlDocument(commandXml);
            XmlNode routerIdentifier = doc.GetElementsByTagName("RouterIdentifier").Item(0);
            return routerIdentifier.InnerText;
        }

        public abstract string RouteCommand(string command);
    }
}
