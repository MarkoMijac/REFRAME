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

            try
            {
                StreamString stream = new StreamString(pipeServer);
                string incomingStream = stream.ReadString();
                ICommandRouter activeRouter = ExtractActiveRouter(incomingStream);
                string command = ExtractCommand(incomingStream);
                string result = activeRouter.RouteCommand(command);
                stream.WriteString(result);
            }
            catch (IOException e)
            {
                Debug.WriteLine("Došlo je do greške: {0}", e.Message);
            }

            pipeServer.Close();
            WriteLogEntry("Server #" + Thread.CurrentThread.ManagedThreadId + " has terminated!");
        }

        private static ICommandRouter ExtractActiveRouter(string incommingStream)
        {
            ICommandRouter sender = null;

            string identifier = incommingStream.Split(';')[0];
            sender = CommandRouters.FirstOrDefault(x => x.Identifier == identifier);

            return sender;
        }

        private static string ExtractCommand(string incommingStream)
        {
            string command = incommingStream.Split(';')[1];
            return command;
        }

        private static string RouteCommand(string incomingCommand)
        {
            string result = "";
            switch (incomingCommand)
            {
                case "WholeGraph": result = GenerateWholeGraph(); break;
                case "PartialGraph": result = GeneratePartialGraph(); break;
                default:
                    break;
            }
            return result;
        }

        private static string GeneratePartialGraph()
        {
            return "This is generated PARTIAL GRAPH";
        }

        private static string GenerateWholeGraph()
        {
            return "This is generated WHOLE GRAPH";
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
