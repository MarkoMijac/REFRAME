using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ObjectAnalysisNode : AnalysisNode
    {
        public ClassAnalysisNode OwnerClass { get; set; }

        public ObjectAnalysisNode(XElement xNode)
        {
            Level = AnalysisLevel.ObjectLevel;

            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = xNode.Element("Name").Value;

            Parent = NodeFactory.CreateNode(xNode.Element("OwnerClass"), AnalysisLevel.ClassLevel);
            OwnerClass = NodeFactory.CreateNode(xNode.Element("OwnerClass"), AnalysisLevel.ClassLevel) as ClassAnalysisNode;
        }
    }
}
