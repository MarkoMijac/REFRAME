using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Nodes
{
    public class NamespaceAnalysisNode : AnalysisNode
    {
        public NamespaceAnalysisNode(uint identifier) : base(identifier, AnalysisLevel.NamespaceLevel)
        {

        }
    }
}
