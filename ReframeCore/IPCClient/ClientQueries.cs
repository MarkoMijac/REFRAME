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

        public static string GetClassAnalysisGraph(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraph"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphSourceNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphSourceNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphSinkNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphSinkNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphLeafNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphLeafNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphOrphanNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphOrphanNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphIntermediaryNodes(string graphIdentifier)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphIntermediaryNodes"
            };

            command.Parameters.Add("GraphIdentifier", graphIdentifier);

            return PipeClient.ExecuteServerCommand(command);
        }
    }
}
