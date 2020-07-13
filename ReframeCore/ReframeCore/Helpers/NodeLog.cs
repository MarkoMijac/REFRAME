using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class NodeLog
    {
        #region Public methods

        public int Count
        {
            get => _loggedNodes.Count;
        }

        public NodeLog()
        {
            _loggedNodes = new List<string>();
        }

        public void Log(INode node)
        {
            if (node != null)
            {
                _loggedNodes.Add(ExtractData(node));
                _loggedNodesDetails.Add(ExtractData(node, true));
            }
        }

        public void Log(KeyValuePair<INode, bool> node)
        {
            if (!node.Equals(default(KeyValuePair<INode, bool>)))
            {
                Log(node.Key);
            }
        }

        /// <summary>
        /// Logs nodes.
        /// </summary>
        /// <param name="nodesToLog">Nodes to be logged.</param>
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
            _loggedNodesDetails.Clear();
        }

        public override string ToString()
        {
            return PrintLoggedNodes();
        }

        public override bool Equals(object obj)
        {
            bool equal = false;
            var item = obj as NodeLog;

            if (item != null)
            {
                equal = PrintLoggedNodes() == item.PrintLoggedNodes();
            }

            return equal;
        }

        public override int GetHashCode()
        {
            return PrintLoggedNodes().GetHashCode();
        }

        #endregion

        #region Private methods

        private List<string> _loggedNodes = new List<string>();
        private List<string> _loggedNodesDetails = new List<string>();

        private string ExtractData(INode node, bool detailedView = false)
        {
            string data = "";

            if (detailedView == false)
            {
                data += node.Identifier + ";";
                data += node.MemberName + ";";
                data += node.OwnerObject.GetType().ToString() + ";";
                data += node.OwnerObject.GetHashCode().ToString() + ";";
            }
            else
            {
                data +=  node.Identifier+";";
                data += node.MemberName+";";
                data += node.OwnerObject.GetType().ToString() + ";";
                data += node.OwnerObject.GetHashCode().ToString()+";";
                data += node.Layer+";";

                NodeUpdateInfo updateInfo = (node as IUpdateInfoProvider).UpdateInfo;

                DateTime start = updateInfo.UpdateStartedAt;
                data += string.Format("{0}:{1}:{2}:{3};", start.Hour, start.Minute, start.Second, start.Millisecond);
                DateTime finish = updateInfo.UpdateCompletedAt;
                data += string.Format("{0}:{1}:{2}:{3};", finish.Hour, finish.Minute, finish.Second, finish.Millisecond);
                data += string.Format("{0};", updateInfo.UpdateDuration);
            }
            return data;
        }

        private string PrintLoggedNodes()
        {
            string data = "";

            foreach (var d in _loggedNodes)
            {
                data += d + Environment.NewLine;
            }

            return data;
        }

        private string PrintLoggedNodesDetails()
        {
            string data = "";

            foreach (var d in _loggedNodesDetails)
            {
                data += d + Environment.NewLine + Environment.NewLine;
            }

            return data;
        }

        public IReadOnlyCollection<string> GetLoggedNodes()
        {
            return _loggedNodes.AsReadOnly();
        }

        public IReadOnlyCollection<string> GetLoggedNodesDetails()
        {
            return _loggedNodesDetails.AsReadOnly();
        }

        #endregion
    }
}
