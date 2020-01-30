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
            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = Identifier.ToString();

            OwnerClass = new ClassAnalysisNode(xNode.Element("OwnerClass"));
        }
    }
}
