using IPCServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeServer
{
    public class ReframePipeServer : PipeServer
    {
        protected override void InitializeHandlers()
        {
            CommandHandlers.Add(new CoreHandler());
        }
    }
}
