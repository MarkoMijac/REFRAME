using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IPCClient
{
    public abstract class PipeClient
    {
        protected string SendCommand(PipeCommand command)
        {
            NamedPipeClientStream pipeClient = CreateClient();
            pipeClient.Connect(3000);

            StreamString stream = new StreamString(pipeClient);
            stream.WriteString(command.ToString());
            string result = stream.ReadString();

            pipeClient.Close();
            return result;
        }

        private NamedPipeClientStream CreateClient()
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);

            return pipeClient;
        }
    }
}
