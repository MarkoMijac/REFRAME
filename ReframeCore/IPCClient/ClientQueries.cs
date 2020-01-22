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
    }
}
