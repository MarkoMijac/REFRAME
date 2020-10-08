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
    public class ClassAnalysisNodeFactory : AnalysisNodeFactory
    {
        public override IAnalysisNode CreateNode(XElement xNode)
        {
            try
            {
                uint identifier = uint.Parse(xNode.Element("Identifier").Value);
                var node = new ClassAnalysisNode(identifier);
                node.Name = xNode.Element("Name").Value;
                node.FullName = xNode.Element("FullName").Value;
                node.Source = xNode.ToString();

                var namespaceFactory = new NamespaceAnalysisNodeFactory();
                node.Parent = namespaceFactory.CreateNode(xNode.Element("OwnerNamespace"));

                var assemblyFactory = new AssemblyAnalysisNodeFactory();
                node.Parent2 = assemblyFactory.CreateNode(xNode.Element("OwnerAssembly"));

                return node;
            }
            catch (Exception e)
            {
                throw new AnalysisException("Error parsing ClassNode XML! Source error message: " + e.Message);
            }
            
        }
    }
}
