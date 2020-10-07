using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.NodeFactories
{
    public class ObjectMemberAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            uint identifier = uint.Parse(xNode.Element("Identifier").Value);
            var node = new ObjectMemberAnalysisNode(identifier, AnalysisLevel.ObjectMemberLevel);

            node.Name = xNode.Element("MemberName").Value;
            node.NodeType = xNode.Element("NodeType").Value;
            node.CurrentValue = xNode.Element("CurrentValue").Value;
            node.PreviousValue = xNode.Element("PreviousValue").Value;

            var objectFactory = new ObjectAnalysisNodeFactory();
            node.Parent = objectFactory.CreateNode(xNode.Element("OwnerObject"));
            node.Source = xNode.ToString();

            return node;
        }
    }
}
