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
        #region Public methods

        public int Count
        {
            get => _loggedNodes.Count;
        }

        public UpdateLogger()
        {
            _loggedNodes = new List<string>();
        }

        public void Log(INode node)
        {
            if (node != null)
            {
                _loggedNodes.Add(ExtractData(node));
            }
        }

        /// <summary>
        /// Logs nodes to be updated.
        /// </summary>
        /// <param name="nodesToLog">Nodes to be updated.</param>
        public void Log(IList<INode> nodesToLog)
        {
            if (nodesToLog!=null)
            {
                foreach (var n in nodesToLog)
                {
                    Log(n);
                }
            }
        }

        public void ClearLog()
        {
            _loggedNodes.Clear();
        }

        public override string ToString()
        {
            return GetLoggedNodes();
        }

        public override bool Equals(object obj)
        {
            bool equal = false;
            var item = obj as UpdateLogger;

            if (item != null)
            {
                equal = GetLoggedNodes() == item.GetLoggedNodes();
            }

            return equal;
        }

        public override int GetHashCode()
        {
            return GetLoggedNodes().GetHashCode();
        }

        #endregion

        #region Private methods

        private List<string> _loggedNodes = new List<string>();

        private string ExtractData(INode node)
        {
            string data = "";

            data += node.Identifier + ";";
            data += node.MemberName + ";";
            data += node.OwnerObject.GetType().ToString() + ";";
            data += node.OwnerObject.GetHashCode().ToString() + ";";

            return data;
        }

        private string GetLoggedNodes()
        {
            string data = "";

            foreach (var d in _loggedNodes)
            {
                data += d + Environment.NewLine;
            }

            return data;
        }

        #endregion
    }
}
