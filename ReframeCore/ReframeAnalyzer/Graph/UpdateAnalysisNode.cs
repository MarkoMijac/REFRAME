using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisNode : AnalysisNode, IUpdateNode
    {
        public string NodeType { get; private set; }
        public int UpdateOrder { get; private set; }
        public int UpdateLayer { get; private set; }
        public string UpdateStartedAt { get; private set; }
        public string UpdateCompletedAt { get; private set; }
        public string UpdateDuration { get; private set; }
        public string CurrentValue { get; private set; }
        public string PreviousValue { get; private set; }
        public bool IsInitialNode { get; private set; } = false;

        public UpdateAnalysisNode(XElement xNode, IAnalysisNode objectMemberNode)
        {
            Level = AnalysisLevel.UpdateAnalysisLevel;

            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = xNode.Element("MemberName").Value;
            NodeType = xNode.Element("NodeType").Value;
            UpdateOrder = int.Parse(xNode.Element("UpdateOrder").Value);
            UpdateLayer = int.Parse(xNode.Element("UpdateLayer").Value);
            UpdateStartedAt = xNode.Element("UpdateStartedAt").Value;
            UpdateCompletedAt = xNode.Element("UpdateCompletedAt").Value;
            UpdateDuration = xNode.Element("UpdateDuration").Value;
            CurrentValue = xNode.Element("CurrentValue").Value;
            PreviousValue = xNode.Element("PreviousValue").Value;

            XAttribute isInitialAttribute = xNode.Attribute("IsInitialNode");
            IsInitialNode = isInitialAttribute != null;
            Parent = objectMemberNode;

            Source = xNode.ToString();
        }
    }
}
