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
        public List<ICommandRouter> CommandRouters { get; set; } = new List<ICommandRouter>();

        public string communicationLog="";

        private Thread server = null;
        private bool continueListening = true;

        public PipeServer()
        {
            
        }

        public void StartServer()
        {
            server = new Thread(Listening);
            server.Start();
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
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances);
            pipeServer.WaitForConnection();
            StreamString stream = new StreamString(pipeServer);
            string incomingStream;
            try
            {
                incomingStream = stream.ReadString();
                WriteLogEntry($"IncomingStream: {incomingStream}");
                ICommandRouter activeRouter = GetActivatedRouter(incomingStream);
                WriteLogEntry($"ActiveRouter: {activeRouter.Identifier}");
                string result = activeRouter.RouteCommand(incomingStream);
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

        private ICommandRouter GetActivatedRouter(string commandXml)
        {
            ICommandRouter sender = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(commandXml);
            XmlNode routerIdentifier = doc.GetElementsByTagName("RouterIdentifier").Item(0);
            string identifier =  routerIdentifier.InnerText;

            sender = CommandRouters.FirstOrDefault(x => x.Identifier == identifier);

            return sender;
        }

        private void WriteLogEntry(string entry)
        {
            communicationLog += entry + Environment.NewLine;
        }

        private void ClearLog()
        {
            communicationLog = "";
        }
    }
}
