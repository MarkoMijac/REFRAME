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
        public ClassAnalysisNode OwnerClass { get; set; }

        public ClassMemberAnalysisNode(ObjectMemberAnalysisNode objectMemberNode)
        {
            string memberName = objectMemberNode.Name;
            uint classIdentifier = objectMemberNode.OwnerObject.OwnerClass.Identifier;
            Identifier = GenerateIdentifier(memberName, classIdentifier);
            Name = memberName;
            NodeType = objectMemberNode.NodeType;
            OwnerClass = objectMemberNode.OwnerObject.OwnerClass;
        }

        public static  uint GenerateIdentifier(string memberName, uint classIdentifier)
        {
            return (uint)(memberName.GetHashCode() ^ classIdentifier);
        }
    }
}
