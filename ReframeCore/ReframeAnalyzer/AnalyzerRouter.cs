using IPCServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public class AnalyzerRouter : ICommandRouter
    {
        public string Identifier => "AnalyzerRouter";

        public string RouteCommand(string command)
        {
            string result = "";

            switch (command)
            {
                case "GetRegisteredGraphs": result = Analyzer.GetRegisteredGraphs(); break;
                default:
                    result = "No such command!";
                    break;
            }

            return result;
        }
    }
}
