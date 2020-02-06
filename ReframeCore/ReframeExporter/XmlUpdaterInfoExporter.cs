using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReframeExporter
{
    public class XmlUpdaterInfoExporter : Exporter
    {
        protected string ReactorIdentifier { get; set; }

        public XmlUpdaterInfoExporter(string reactorIdentifier) : base()
        {
            ReactorIdentifier = reactorIdentifier;
        }

        public override string Export()
        {
            var reactor = ReactorRegistry.Instance.GetReactor(ReactorIdentifier);
            IReadOnlyCollection<string> logs = GetUpdateLog(reactor.Updater as ILoggable);
            UpdateInfo updateInfo = (reactor.Updater as Updater).LatestUpdateInfo;

            StringBuilder builder = new StringBuilder();

            using (var stringWriter = new StringWriter(builder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, _defaultXmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("UpdateProcess");

                WriteUpdateInfo(xmlWriter, updateInfo);

                xmlWriter.WriteStartElement("Reactor");

                WriteBasicReactorData(xmlWriter, reactor);
                xmlWriter.WriteStartElement("Graph");
                WriteBasicGraphData(xmlWriter, reactor.Graph);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                WriteUpdatedNodes(xmlWriter, logs, reactor);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return builder.ToString();
        }

        private void WriteUpdateInfo(XmlWriter xmlWriter, UpdateInfo updateInfo)
        {
            xmlWriter.WriteStartElement("UpdateSuccessful");
            xmlWriter.WriteString(updateInfo.UpdateSuccessfull.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("UpdateStartedAt");
            xmlWriter.WriteString(GetFormatedTime(updateInfo.UpdateStartedAt));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("UpdateEndedAt");
            xmlWriter.WriteString(GetFormatedTime(updateInfo.UpdateEndedAt));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("UpdateDuration");
            xmlWriter.WriteString(updateInfo.UpdateDuration.ToString());
            xmlWriter.WriteEndElement();

            WriteUpdateCauseInfo(xmlWriter, updateInfo);
            WriteUpdateErrorInfo(xmlWriter, updateInfo);
        }

        private void WriteUpdateCauseInfo(XmlWriter xmlWriter, UpdateInfo updateInfo)
        {
            xmlWriter.WriteStartElement("UpdateCause");

            xmlWriter.WriteStartElement("Message");
            xmlWriter.WriteString(updateInfo.CauseMessage);
            xmlWriter.WriteEndElement();

            if (updateInfo.InitialNode != null)
            {
                INode initialNode = updateInfo.InitialNode;
                xmlWriter.WriteStartElement("InitialNode");
                WriteNodeBasicData(initialNode, xmlWriter);
                WriteNodeValueData(initialNode, xmlWriter);

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteNodeValueData(INode initialNode, XmlWriter xmlWriter)
        {
            PropertyNode propertyNode = initialNode as PropertyNode;
            if (propertyNode != null)
            {
                xmlWriter.WriteStartElement("CurrentValue");
                xmlWriter.WriteString(propertyNode.CurrentValue.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PreviousValue");
                xmlWriter.WriteString(propertyNode.PreviousValue.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteUpdateErrorInfo(XmlWriter xmlWriter, UpdateInfo updateInfo)
        {
            if (updateInfo.UpdateSuccessfull == false && updateInfo.ErrorData != null)
            {
                UpdateError updateError = updateInfo.ErrorData;
                xmlWriter.WriteStartElement("UpdateError");

                xmlWriter.WriteStartElement("FailedNode");

                WriteNodeBasicData(updateError.FailedNode, xmlWriter);

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("SourceException");
                xmlWriter.WriteString(updateError.SourceException.Message);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("StackTrace");
                xmlWriter.WriteString(updateError.SourceException.Message);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
        }

        private void WriteUpdatedNodes(XmlWriter xmlWriter, IReadOnlyCollection<string> logs, IReactor reactor)
        {
            xmlWriter.WriteStartElement("Nodes");
            int orderNum = 1;
            foreach (var log in logs)
            {
                uint nodeIdentifier = GetNodeIdentifier(log);
                INode node = reactor.GetNode(nodeIdentifier);
                
                WriteUpdatedNode(orderNum, node, xmlWriter);
                orderNum++;
            }

            xmlWriter.WriteEndElement();
        }

        private uint GetNodeIdentifier(string log)
        {
            return uint.Parse(log.Split(';')[0]);
        }

        private void WriteUpdatedNode(int orderNum, INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Node");

            WriteNodeData(node, xmlWriter);
            WriteNodeUpdateInfo(orderNum, node, xmlWriter);

            WritePredecessors(node, xmlWriter);
            WriteSuccesors(node, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteSuccesors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successors");
            foreach (INode successor in node.Successors)
            {
                WriteSuccessor(successor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WriteSuccessor(INode successor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successor");

            WriteNodeBasicData(successor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WritePredecessors(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (INode predecessor in node.Predecessors)
            {
                WritePredecessor(predecessor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WritePredecessor(INode predecessor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessor");

            WriteNodeBasicData(predecessor, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private string GetFormatedTime(DateTime dateTime)
        {
            return string.Format("{0}:{1}:{2}:{3}", dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        private void WriteNodeUpdateInfo(int orderNum, INode node, XmlWriter xmlWriter)
        {
            if (node != null)
            {
                ITimeInfoProvider timeInfo = node as ITimeInfoProvider;
                xmlWriter.WriteStartElement("UpdateOrder");
                xmlWriter.WriteString(orderNum.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateLayer");
                xmlWriter.WriteString(node.Layer.ToString());
                xmlWriter.WriteEndElement();

                string updateStartedAt = "";
                string updateCompletedAt = "";
                string updateDuration = "";

                if (timeInfo != null)
                {
                    updateStartedAt = GetFormatedTime(timeInfo.UpdateStartedAt);
                    updateCompletedAt = GetFormatedTime(timeInfo.UpdateCompletedAt);
                    updateDuration = timeInfo.UpdateDuration.ToString();
                }

                xmlWriter.WriteStartElement("UpdateStartedAt");
                xmlWriter.WriteString(updateStartedAt);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateCompletedAt");
                xmlWriter.WriteString(updateCompletedAt);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("UpdateDuration");
                xmlWriter.WriteString(updateDuration);
                xmlWriter.WriteEndElement();
            }
        }

        private void WriteNodeData(INode node, XmlWriter xmlWriter)
        {
            if (node != null)
            {
                WriteNodeBasicData(node, xmlWriter);

                xmlWriter.WriteStartElement("NodeType");
                xmlWriter.WriteString(node.GetType().Name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ObjectIdentifier");
                xmlWriter.WriteString(node.OwnerObject.GetHashCode().ToString());
                xmlWriter.WriteEndElement();

                string currentValue = "";
                string previousValue = "";
                if (node is PropertyNode)
                {
                    PropertyNode propertyNode = node as PropertyNode;
                    try
                    {
                        currentValue = propertyNode.CurrentValue.ToString();
                        previousValue = propertyNode.PreviousValue.ToString();
                    }
                    catch (Exception)
                    {

                    }
                    
                }

                xmlWriter.WriteStartElement("CurrentValue");
                xmlWriter.WriteString(currentValue);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PreviousValue");
                xmlWriter.WriteString(previousValue);
                xmlWriter.WriteEndElement();
            }
        }

        private static void WriteNodeBasicData(INode node, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(node.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MemberName");
            xmlWriter.WriteString(node.MemberName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("OwnerObject");
            xmlWriter.WriteString(node.OwnerObject.GetType().Name);
            xmlWriter.WriteEndElement();
        }

        private IReadOnlyCollection<string> GetUpdateLog(ILoggable loggable)
        {
            IReadOnlyCollection<string> updateInfo = null;

            if (loggable != null)
            {
                updateInfo = loggable.NodeLog.GetLoggedNodesDetails();
            }

            return updateInfo;
        }
    }
}
