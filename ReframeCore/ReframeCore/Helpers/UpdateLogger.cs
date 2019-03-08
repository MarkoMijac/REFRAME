using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class UpdateLogger
    {
        private List<string> _nodesToUpdate = new List<string>();
        private IDependencyGraph _dependencyGraph;

        public UpdateLogger(IDependencyGraph dependencyGraph)
        {
            _nodesToUpdate = new List<string>();
            _dependencyGraph = dependencyGraph;
        }

        public void Log(INode node)
        {
            _nodesToUpdate.Add(ExtractData(node));
        }

        /// <summary>
        /// Logs nodes to be updated.
        /// </summary>
        /// <param name="nodesToUpdate">Nodes to be updated.</param>
        public void Log(IList<INode> nodesToUpdate)
        {
            if (_dependencyGraph.Settings.LogUpdates == true)
            {
                ClearNodesToUpdate();
                foreach (var n in nodesToUpdate)
                {
                    Log(n);
                }
            }
        }


        public void ClearNodesToUpdate()
        {
            _nodesToUpdate.Clear();
        }

        private string ExtractData(INode node)
        {
            string data = "";

            data += node.Identifier + ";";
            data += node.MemberName + ";";
            data += node.OwnerObject.GetType().ToString() + ";";
            data += node.OwnerObject.GetHashCode().ToString() + ";";

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

        public override string ToString()
        {
            return GetNodesToUpdate();
        }
    }
}
