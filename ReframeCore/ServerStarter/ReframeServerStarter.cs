using IPCServer;
using ReframeAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStarter
{
    public static class ReframeServerStarter
    {
        public static void StartServer()
        {
            PipeServer.CommandRouters.Add(new AnalyzerRouter());
            PipeServer.StartServer();
        }
    }
}
