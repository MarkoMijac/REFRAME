﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ReframeAnalyzer.Graph;

namespace ReframeAnalyzer.Xml
{
    public class XmlClassGraphExporter : XmlGraphExporter
    {
        protected override void WriteGraphBasicData(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("GraphIdentifier");
            xmlWriter.WriteString(_analysisGraph.DependencyGraph.Identifier);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NodeCount");
            xmlWriter.WriteString(_analysisGraph.DependencyGraph.Nodes.Count.ToString());
            xmlWriter.WriteEndElement();
        }

        protected override void WriteAnalysisNodes(XmlWriter xmlWriter)
        {
            foreach (ClassAnalysisNode node in _analysisGraph.Nodes)
            {
                WriteAnalysisNode(node, xmlWriter);
            }
        }

        private void WriteAnalysisNode(ClassAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("StaticNode");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(analysisNode.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(analysisNode.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("PredecessorsCount");
            xmlWriter.WriteString(analysisNode.Predecessors.Count.ToString());
            xmlWriter.WriteEndElement();

            WritePredecessors(analysisNode, xmlWriter);

            xmlWriter.WriteStartElement("SuccessorsCount");
            xmlWriter.WriteString(analysisNode.Successors.Count.ToString());
            xmlWriter.WriteEndElement();

            WriteSuccessors(analysisNode, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WritePredecessors(ClassAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessors");
            foreach (ClassAnalysisNode predecessor in analysisNode.Predecessors)
            {
                WritePredecessor(predecessor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WritePredecessor(ClassAnalysisNode predecessor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Predecessor");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(predecessor.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(predecessor.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private void WriteSuccessors(ClassAnalysisNode analysisNode, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successors");
            foreach (ClassAnalysisNode successor in analysisNode.Successors)
            {
                WriteSuccessor(successor, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private void WriteSuccessor(ClassAnalysisNode successor, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Successor");

            xmlWriter.WriteStartElement("Identifier");
            xmlWriter.WriteString(successor.Identifier.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteString(successor.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        public XmlClassGraphExporter(ClassAnalysisGraph graph)
            : base(graph)
        {

        }
    }
}
