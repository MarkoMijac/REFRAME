﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            uint identifier = uint.Parse(xNode.Element("Identifier").Value);
            var node = new UpdateAnalysisNode(identifier, AnalysisLevel.UpdateAnalysisLevel);

            node.Name = xNode.Element("MemberName").Value;
            node.NodeType = xNode.Element("NodeType").Value;
            node.UpdateOrder = int.Parse(xNode.Element("UpdateOrder").Value);
            node.UpdateLayer = int.Parse(xNode.Element("UpdateLayer").Value);
            node.UpdateStartedAt = xNode.Element("UpdateStartedAt").Value;
            node.UpdateCompletedAt = xNode.Element("UpdateCompletedAt").Value;
            node.UpdateDuration = xNode.Element("UpdateDuration").Value;
            node.CurrentValue = xNode.Element("CurrentValue").Value;
            node.PreviousValue = xNode.Element("PreviousValue").Value;

            XAttribute isInitialAttribute = xNode.Attribute("IsInitialNode");
            node.IsInitialNode = isInitialAttribute != null;

            node.Source = xNode.ToString();

            return node;
        }

        public IAnalysisNode CreateNode(XElement xNode, IAnalysisNode objectMemberNode)
        {
            var node = CreateNode(xNode) as UpdateAnalysisNode;
            node.Parent = objectMemberNode;

            return node;
        }
    }
}