using IPCClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeClient
{
    public class ReframePipeClient : PipeClient
    {
        public string GetRegisteredReactors()
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "CoreRouter",
                CommandName = "ExportRegisteredReactors"
            };

            return ExecuteServerCommand(command);
        }

        public string GetReactor(string identifier)
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "CoreRouter",
                CommandName = "ExportReactor"
            };

            command.Parameters.Add("ReactorIdentifier", identifier);

            return ExecuteServerCommand(command);
        }

        public string GetUpdateInfo(string identifier)
        {
            ServerCommand command = new ServerCommand()
            {
                RouterIdentifier = "CoreRouter",
                CommandName = "ExportUpdateInfo"
            };

            command.Parameters.Add("ReactorIdentifier", identifier);

            return ExecuteServerCommand(command);
        }
    }
}
