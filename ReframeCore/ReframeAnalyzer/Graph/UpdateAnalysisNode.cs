using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisNode : AnalysisNode
    {
        public string NodeType { get; set; }
        public int UpdateOrder { get; set; }
        public int UpdateLayer { get; set; }
        public string UpdateStartedAt { get; set; }
        public string UpdateCompletedAt { get; set; }
        public string UpdateDuration { get; set; }
        public string CurrentValue { get; set; }
        public string PreviousValue { get; set; }
        public bool IsInitialNode { get; set; } = false;

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
