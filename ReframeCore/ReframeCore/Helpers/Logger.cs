using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class Logger
    {
        private List<string> _nodesToUpdate = new List<string>();
        private List<string> _updatedNodes = new List<string>();

        public Logger()
        {
            _nodesToUpdate = new List<string>();
            _updatedNodes = new List<string>();
        }

        public void LogNodeToUpdate(INode node)
        {
            _nodesToUpdate.Add(ExtractData(node));
        }

        public void LogUpdatedNode(INode node)
        {
            _updatedNodes.Add(ExtractData(node));
        }

        public void ClearNodesToUpdate()
        {
            _nodesToUpdate.Clear();
        }

        public void ClearUpdatedNodes()
        {
            _updatedNodes.Clear();
        }

        private string ExtractData(INode node)
        {
            string data = "";

            data += node.Identifier + ";";
            data += node.MemberName + ";";
            data += node.OwnerObject.GetType().ToString() + ";";

            return data;
        }

        public string GetNodesToUpdate()
        {
            string data = "";

            foreach (var d in _nodesToUpdate)
            {
                data += d + Environment.NewLine;
            }

            return data;
        }
    }
}
