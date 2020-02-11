using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisNode : AnalysisNode
    {
        public string FullName { get; set; }
        public AssemblyAnalysisNode OwnerAssembly { get; set; }

        public ClassAnalysisNode(XElement xNode)
        {
            Level = AnalysisLevel.ClassLevel;

            Identifier = uint.Parse(xNode.Element("Identifier").Value);
            Name = xNode.Element("Name").Value;
            FullName = xNode.Element("FullName").Value;

            Parent = NodeFactory.CreateNode(xNode.Element("OwnerNamespace"), AnalysisLevel.NamespaceLevel);
            OwnerAssembly = new AssemblyAnalysisNode(xNode.Element("OwnerAssembly"));
        }

        
    }
}
