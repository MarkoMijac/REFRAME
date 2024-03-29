﻿using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.NodeFactories
{
    public abstract class AnalysisNodeFactory
    {
        public abstract IAnalysisNode CreateNode(XElement xNode);
    }
}
