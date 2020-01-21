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
                RouterIdentifier = "CoreRouter",
                CommandName = "ExportRegisteredReactors"
            };

            string result = PipeClient.ExecuteServerCommand(command);
            return result;
        }

        public static string GetReactor(string identifier)
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "CoreRouter",
                CommandName = "ExportReactor"
            };

            command.Parameters.Add("ReactorIdentifier", identifier);

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

        public static string GetClassAnalysisGraphPredecessorNodes(string reactorIdentifier, string nodeId)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphPredecessorNodes"
            };

            command.Parameters.Add("GraphIdentifier", reactorIdentifier);
            command.Parameters.Add("NodeIdentifier", nodeId);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphNeighbourNodes(string reactorIdentifier, string nodeId)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphNeighbourNodes"
            };

            command.Parameters.Add("GraphIdentifier", reactorIdentifier);
            command.Parameters.Add("NodeIdentifier", nodeId);

            return PipeClient.ExecuteServerCommand(command);
        }

        public static string GetClassAnalysisGraphSuccessorNodes(string reactorIdentifier, string nodeId)
        {
            ServerCommand command = new ServerCommand
            {
                RouterIdentifier = "AnalyzerRouter",
                CommandName = "GetClassAnalysisGraphSuccessorNodes"
            };

            command.Parameters.Add("GraphIdentifier", reactorIdentifier);
            command.Parameters.Add("NodeIdentifier", nodeId);

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
