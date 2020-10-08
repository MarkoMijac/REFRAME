using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Nodes
{
    public class ObjectAnalysisNode : AnalysisNode
    {
        public ObjectAnalysisNode(uint identifier) : base(identifier, AnalysisLevel.ObjectLevel)
        {

        }
    }
}
