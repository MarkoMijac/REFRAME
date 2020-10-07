using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.NodeFactories
{
    public class ObjectAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            uint identifier = uint.Parse(xNode.Element("Identifier").Value);
            var node = new ObjectAnalysisNode(identifier, AnalysisLevel.ObjectLevel);
            node.Name = xNode.Element("Name").Value;
            node.Source = xNode.ToString();

            var classFactory = new ClassAnalysisNodeFactory();
            node.Parent = classFactory.CreateNode(xNode.Element("OwnerClass"));

            return node;
        }
    }
}
