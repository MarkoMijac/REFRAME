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
        public ReframePipeServer() : base()
        {
            CommandRouters.Add(new CoreRouter());
        }
    }
}
