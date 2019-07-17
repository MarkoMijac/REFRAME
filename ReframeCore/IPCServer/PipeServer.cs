using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPCServer
{
    public static class PipeServer
    {
        public static List<ICommandRouter> CommandRouters { get; set; } = new List<ICommandRouter>();

        public static string communicationLog="";

        private static Thread server = null;
        private static bool continueListening = true;

        public static void StartServer()
        {
            server = new Thread(Listening);
            server.Start();
        }

        private static void Listening()
        {
            WriteLogEntry("Server started on Thread number " + server.ManagedThreadId);
            while (true)
            {
                if (continueListening == true)
                {
                    Thread listener = CreateListener();
                    listener.Join();
                }
            }
        }

        private static Thread CreateListener()
        {
            Thread listener = new Thread(CreatePipeServer);
            listener.Start();
            return listener;
        }

        private static void CreatePipeServer()
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances);
            pipeServer.WaitForConnection();
            StreamString stream = new StreamString(pipeServer);
            string incomingStream;
            try
            {
                incomingStream = stream.ReadString();
                ICommandRouter activeRouter = GetActivatedRouter(incomingStream);
                string result = activeRouter.RouteCommand(incomingStream);
                stream.WriteString(result);
            }
            catch (Exception e)
            {
                stream.WriteString("Došlo je do greške:"+e.Message+"; StackTrace:"+e.StackTrace);
            }

            pipeServer.Close();
            WriteLogEntry("Server #" + Thread.CurrentThread.ManagedThreadId + " has terminated!");
        }

        private static ICommandRouter GetActivatedRouter(string commandXml)
        {
            ICommandRouter sender = null;

            string identifier = CommandRouter.GetRouterIdentifier(commandXml);
            sender = CommandRouters.FirstOrDefault(x => x.Identifier == identifier);

            return sender;
        }

        private static void WriteLogEntry(string entry)
        {
            communicationLog += entry + Environment.NewLine;
        }

        private static void ClearLog()
        {
            communicationLog = "";
        }
    }
}
