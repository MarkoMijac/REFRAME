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
            PipeCommand command = new PipeCommand()
            {
                HandlerIdentifier = "CoreHandler",
                CommandName = "ExportRegisteredReactors"
            };

            return SendCommand(command);
        }

        public string GetReactor(string identifier)
        {
            PipeCommand command = new PipeCommand()
            {
                HandlerIdentifier = "CoreHandler",
                CommandName = "ExportReactor"
            };

            command.Parameters.Add("ReactorIdentifier", identifier);

            return SendCommand(command);
        }

        public string GetUpdateInfo(string identifier)
        {
            PipeCommand command = new PipeCommand()
            {
                HandlerIdentifier = "CoreHandler",
                CommandName = "ExportUpdateInfo"
            };

            command.Parameters.Add("ReactorIdentifier", identifier);

            return SendCommand(command);
        }
    }
}
