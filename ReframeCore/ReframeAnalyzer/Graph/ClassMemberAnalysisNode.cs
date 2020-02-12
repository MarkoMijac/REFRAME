using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisNode : AnalysisNode
    {
        public string NodeType { get; set; }

        public ClassMemberAnalysisNode(IAnalysisNode objectMemberNode)
        {
            Level = AnalysisLevel.ClassMemberLevel;

            string memberName = objectMemberNode.Name;
            uint classIdentifier = objectMemberNode.Parent.Parent.Identifier;
            Identifier = GenerateIdentifier(memberName, classIdentifier);
            Name = memberName;
            NodeType = (objectMemberNode as IHasType).NodeType;
            Parent = objectMemberNode.Parent.Parent;
        }

        public ClassMemberAnalysisNode(XElement xNode)
        {

        }

        public static  uint GenerateIdentifier(string memberName, uint classIdentifier)
        {
            return (uint)(memberName.GetHashCode() ^ classIdentifier);
        }
    }
}
