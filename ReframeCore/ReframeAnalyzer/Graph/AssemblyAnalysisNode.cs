using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class AssemblyAnalysisNode : AnalysisNode
    {
        public AssemblyAnalysisNode(XElement xNode)
        {
            Level = AnalysisLevel.AssemblyLevel;

            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = xNode.Element("Name").Value;

            Source = xNode.ToString();
        }
    }
}
