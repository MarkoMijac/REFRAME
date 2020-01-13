using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCClient
{
    public static class ClientQueries
    {
        public static string GetRegisteredReactors()
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetRegisteredReactors"
            };

            string result = PipeClient.ExecuteServerCommand(command);
            return result;
        }

        public static string GetRegisteredGraphs()
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetRegisteredGraphs"
            };

            string result = PipeClient.ExecuteServerCommand(command);
            return result;
        }

        public static string GetGraphNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetGraphNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            string result = PipeClient.ExecuteServerCommand(command);
            return result;
        }
    }
}
