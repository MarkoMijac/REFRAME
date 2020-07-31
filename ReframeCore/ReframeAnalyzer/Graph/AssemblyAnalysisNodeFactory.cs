using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class AssemblyAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            uint identifier = uint.Parse(xNode.Element("Identifier").Value);
            var node = new AssemblyAnalysisNode(identifier, AnalysisLevel.AssemblyLevel);
            node.Name = xNode.Element("Name").Value;
            node.Source = xNode.ToString();

            return node;
        }
    }
}
