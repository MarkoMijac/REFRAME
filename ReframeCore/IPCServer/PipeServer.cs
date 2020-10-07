using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace IPCServer
{
    public abstract class PipeServer
    {
        public List<ICommandHandler> CommandHandlers { get; set; } = new List<ICommandHandler>();

        public string communicationLog="";

        private Thread server = null;
        private bool continueListening = true;

        public PipeServer()
        {
            InitializeHandlers();
        }

        protected abstract void InitializeHandlers();

        public void StartServer()
        {
            server = new Thread(Listening);
            server.Start();
        }

        public void StopServer()
        {
            if (server.IsAlive)
            {
                server.Join();
            }
        }

        private void Listening()
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

        private Thread CreateListener()
        {
            Thread listener = new Thread(CreatePipeServer);
            listener.Start();
            return listener;
        }

        private void CreatePipeServer()
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("reframePipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances);
            pipeServer.WaitForConnection();
            StreamString stream = new StreamString(pipeServer);
            string incomingStream;
            try
            {
                incomingStream = stream.ReadString();
                WriteLogEntry($"IncomingStream: {incomingStream}");
                ICommandHandler activeHandler = GetActivatedHandler(incomingStream);
                WriteLogEntry($"ActiveHandler: {activeHandler.Identifier}");
                string result = activeHandler.HandleCommand(incomingStream);
                WriteLogEntry($"Result: {result}");
                stream.WriteString(result);
            }
            catch (Exception e)
            {
                stream.WriteString("Došlo je do greške:"+e.Message+"; StackTrace:"+e.StackTrace);
            }

            pipeServer.Close();
            WriteLogEntry("Server #" + Thread.CurrentThread.ManagedThreadId + " has terminated!");
        }

        private ICommandHandler GetActivatedHandler(string commandXml)
        {
            string identifier = GetHandlerIdentifier(commandXml);
            return CommandHandlers.FirstOrDefault(x => x.Identifier == identifier);
        }

        private void WriteLogEntry(string entry)
        {
            communicationLog += entry + Environment.NewLine;
        }

        private string GetHandlerIdentifier(string commandXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(commandXml);
            XmlNode handlerIdentifier = doc.GetElementsByTagName("HandlerIdentifier").Item(0);
            return handlerIdentifier.InnerText;
        }
    }
}
