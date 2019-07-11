using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public static class PipeServer
    {
        private static string communicationLog="";

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
                string incomingCommand = stream.ReadString();
                string result = RouteCommand(incomingCommand);
                stream.WriteString(result);
            }
            catch (IOException e)
            {
                Debug.WriteLine("ERROR: {0}", e.Message);
            }

            pipeServer.Close();
            WriteLogEntry("Server #" + Thread.CurrentThread.ManagedThreadId + " has terminated!");
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
