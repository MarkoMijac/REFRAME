using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisNode : AnalysisNode
    {
        public string NodeType { get; set; }
        public ObjectAnalysisNode OwnerObject { get; set; }
        public int UpdateLayer { get; set; }
        public string UpdateStartedAt { get; set; }
        public string UpdateCompletedAt { get; set; }
        public string UpdateDuration { get; set; }

        public UpdateAnalysisNode(XElement xNode, ObjectMemberAnalysisNode objectMemberNode)
        {
            Identifier = objectMemberNode.Identifier;
            Name = objectMemberNode.Name;
            NodeType = objectMemberNode.NodeType;
            UpdateLayer = int.Parse(xNode.Element("UpdateLayer").Value);
            UpdateStartedAt = xNode.Element("UpdateStartedAt").Value;
            UpdateCompletedAt = xNode.Element("UpdateCompletedAt").Value;
            UpdateDuration = xNode.Element("UpdateDuration").Value;

            OwnerObject = objectMemberNode.OwnerObject;
        }
    }
}
