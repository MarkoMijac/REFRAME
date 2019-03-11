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

        public UpdateLogger()
        {
            _nodesToUpdate = new List<string>();
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
            ClearNodesToUpdate();
            foreach (var n in nodesToUpdate)
            {
                Log(n);
            }
        }


        private void ClearNodesToUpdate()
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

        private string GetNodesToUpdate()
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

        public override bool Equals(object obj)
        {
            bool equal = false;
            var item = obj as UpdateLogger;

            if (item != null)
            {
                equal = GetNodesToUpdate() == item.GetNodesToUpdate();
            }

            return equal;
        }

        public override int GetHashCode()
        {
            return GetNodesToUpdate().GetHashCode();
        }
    }
}
