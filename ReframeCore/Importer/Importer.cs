using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ReframeImporter
{
    public class Importer
    {
        public XElement GetReactor(string source)
        {
            if (source == "")
            {
                throw new ImporterException("XML source is empty!");
            }

            try
            {
                XElement xReactor = null;
                var xRoot = XElement.Parse(source);
                if (xRoot.Name == "Reactor")
                {
                    xReactor = xRoot;
                }
                else
                {
                    xReactor = xRoot.Element("Reactor");
                }
                return xReactor;
            }
            catch (XmlException e)
            {
                throw new ImporterException("XML source not valid! "+e.Message);
            } 
        }

        public XElement GetGraph(string source)
        {
            XElement xReactor = GetReactor(source);
            return GetGraph(xReactor);
        }

        public XElement GetGraph(XElement xReactor)
        {
            return xReactor.Element("Graph");
        }

        public string GetIdentifier(XElement xElement)
        {
            try
            {
                return xElement.Element("Identifier").Value;
            }
            catch (NullReferenceException e)
            {
                throw new ImporterException("Provided element does not have identifier! " + e.Message);
            }
        }

        public string GetName(XElement xElement)
        {
            return xElement.Name.ToString();
        }

        public IEnumerable<XElement> GetNodes(XElement xGraph)
        {
            if (xGraph != null)
            {
                return xGraph.Descendants("Node");
            }
            return new List<XElement>();
        }

        public IEnumerable<XElement> GetSuccessors(XElement xNode)
        {
            if (xNode != null && GetName(xNode) == "Node")
            {
                return xNode.Descendants("Successor");
            }
            return new List<XElement>();
        }


    }
}
