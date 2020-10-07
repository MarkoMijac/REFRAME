using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.NodeFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ReframeAnalyzer.GraphFactories
{
    public abstract class AnalysisGraphFactory
    {
        protected string XmlSource { get; private set; }
        protected AnalysisNodeFactory NodeFactory { get; set; }

        public virtual IAnalysisGraph CreateGraph(string xmlSource)
        {
            XmlSource = xmlSource;
            ValidateXmlSource();
            return CreateGraph();
        }

        protected abstract IAnalysisGraph CreateGraph();

        private void ValidateXmlSource()
        {
            if (XmlSource == "")
            {
                throw new AnalysisException("XML source is empty!");
            }

            try
            {
                XElement.Parse(XmlSource);
            }
            catch (XmlException e)
            {
                throw new AnalysisException("XML Source is not valid! " + e.Message);
            }
        }
    }
}
