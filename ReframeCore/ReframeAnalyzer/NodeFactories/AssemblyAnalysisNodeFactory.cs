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
    public class AssemblyAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            try
            {
                uint identifier = uint.Parse(xNode.Element("Identifier").Value);
                var node = new AssemblyAnalysisNode(identifier);
                node.Name = xNode.Element("Name").Value;
                node.Source = xNode.ToString();

                return node;
            }
            catch (Exception e)
            {
                throw new AnalysisException("Error parsing AssemblyNode XML! Source error message: " + e.Message);
            }
        }
    }
}
