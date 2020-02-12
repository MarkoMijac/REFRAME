using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ObjectMemberAnalysisNode : AnalysisNode, IHasType, IHasValues
    {
        public string NodeType { get; set; }
        public string CurrentValue { get; set; }
        public string PreviousValue { get; set; }

        public ObjectMemberAnalysisNode(XElement xNode)
        {
            Level = AnalysisLevel.ObjectMemberLevel;

            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = xNode.Element("MemberName").Value;
            NodeType = xNode.Element("NodeType").Value;
            CurrentValue = xNode.Element("CurrentValue").Value;
            PreviousValue = xNode.Element("PreviousValue").Value;

            Parent = NodeFactory.CreateNode(xNode.Element("OwnerObject"), AnalysisLevel.ObjectLevel);
        }
    }
}
