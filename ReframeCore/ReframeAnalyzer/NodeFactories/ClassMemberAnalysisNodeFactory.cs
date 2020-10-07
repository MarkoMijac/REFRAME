using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.NodeFactories
{
    public class ClassMemberAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            string memberName = xNode.Element("MemberName").Value;
            uint classIdentifier = uint.Parse(xNode.Element("OwnerObject").Element("OwnerClass").Element("Identifier").Value);

            uint identifier = GenerateIdentifier(memberName, classIdentifier);
            var node = new ClassMemberAnalysisNode(identifier, AnalysisLevel.ClassMemberLevel);
            node.Name = memberName;
            node.NodeType = xNode.Element("NodeType").Value;

            var classFactory = new ClassAnalysisNodeFactory();
            node.Parent = classFactory.CreateNode(xNode.Element("OwnerObject").Element("OwnerClass"));
            node.Source = xNode.ToString();

            return node;
        }

        private uint GenerateIdentifier(string memberName, uint classIdentifier)
        {
            return (uint)(memberName.GetHashCode() ^ classIdentifier);
        }
    }
}
