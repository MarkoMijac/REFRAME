using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisNode : AnalysisNode, IHasType
    {
        public string NodeType { get; set; }

        public ClassMemberAnalysisNode(XElement xNode)
        {
            Level = AnalysisLevel.ClassMemberLevel;
            string memberName = xNode.Element("MemberName").Value;
            uint classIdentifier = uint.Parse(xNode.Element("OwnerObject").Element("OwnerClass").Element("Identifier").Value);

            Identifier = GenerateIdentifier(memberName, classIdentifier);
            Name = memberName;
            NodeType = xNode.Element("NodeType").Value;
            Parent = NodeFactory.CreateNode(xNode.Element("OwnerObject").Element("OwnerClass"), AnalysisLevel.ClassLevel);

            Source = xNode.ToString();
        }

        private uint GenerateIdentifier(string memberName, uint classIdentifier)
        {
            return (uint)(memberName.GetHashCode() ^ classIdentifier);
        }
    }
}
