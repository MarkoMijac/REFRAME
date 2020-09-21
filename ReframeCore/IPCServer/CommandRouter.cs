using ReframeExporter;
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

        protected XmlDocument GetCommandXmlDocument(string commandXml)
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

        public string RouteCommand(string command)
        {
            string result = "";

            try
            {
                var exporter = GetExporter(command);
                if (exporter != null)
                {
                    result = exporter.Export();
                }
                else
                {
                    result = "No such command!";
                }
            }
            catch (Exception e)
            {
                string errorMessage = $"Error: {e.Message}" + Environment.NewLine;
                errorMessage += $"StackTrace:{e.StackTrace}";
                return errorMessage;
            }
            return result;
        }

        protected abstract IExporter GetExporter(string command);
    }
}
