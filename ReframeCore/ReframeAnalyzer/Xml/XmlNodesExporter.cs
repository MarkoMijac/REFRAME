using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Xml
{
    public class XmlNodesExporter : IXmlExporter
    {
        private IEnumerable<IAnalysisNode> _nodes;
        public XmlNodesExporter(IEnumerable<IAnalysisNode> nodes)
        {
            _nodes = nodes;
        }

        public string Export()
        {
            return "";
        }
    }
}
