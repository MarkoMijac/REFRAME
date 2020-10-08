using ReframeAnalyzer.Exceptions;
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
            try
            {
                uint identifier = uint.Parse(xNode.Element("Identifier").Value);
                var node = new ObjectMemberAnalysisNode(identifier);

                node.Name = xNode.Element("MemberName").Value;
                node.NodeType = xNode.Element("NodeType").Value;
                node.CurrentValue = xNode.Element("CurrentValue").Value;
                node.PreviousValue = xNode.Element("PreviousValue").Value;

                var objectFactory = new ObjectAnalysisNodeFactory();
                node.Parent = objectFactory.CreateNode(xNode.Element("OwnerObject"));
                node.Source = xNode.ToString();

                return node;

            }
            catch (Exception e)
            {
                throw new AnalysisException("Error parsing ObjectMemberNode XML! Source error message: " + e.Message);
            }
            
        }
    }
}
