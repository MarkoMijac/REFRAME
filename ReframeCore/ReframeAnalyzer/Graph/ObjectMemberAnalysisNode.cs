﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ObjectMemberAnalysisNode : AnalysisNode, IHasType, IHasValues
    {
        public string NodeType { get; set; }
        public string CurrentValue { get; set; }
        public string PreviousValue { get; set; }

        public ObjectMemberAnalysisNode(uint identifier, AnalysisLevel level) : base(identifier, level)
        {

        }
    }
}
