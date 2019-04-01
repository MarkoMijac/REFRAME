using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class UpdateLogger
    {
        private Stopwatch _stopwatch = new Stopwatch();

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

        /// <summary>
        /// Logs nodes.
        /// </summary>
        /// <param name="nodesToLog">Nodes to be logged.</param>
        public void Log(Dictionary<INode, bool> nodesToLog)
        {
            if (nodesToLog != null)
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
                data += string.Format("Node identifier: {0}"+Environment.NewLine, node.Identifier);
                data += string.Format("Node member name: {0}" + Environment.NewLine, node.MemberName);
                data += string.Format("Owner object type: {0}" + Environment.NewLine, node.OwnerObject.GetType().ToString());
                data += string.Format("Owner object hash: {0}" + Environment.NewLine, node.OwnerObject.GetHashCode().ToString());
                data += string.Format("Level: {0}" + Environment.NewLine, node.Level);

                DateTime start = (node as ITimeInfoProvider).UpdateStartedAt;
                data += string.Format("Update started at: {0}:{1}:{2}:{3}" + Environment.NewLine, start.Hour, start.Minute, start.Second, start.Millisecond);
                DateTime finish = (node as ITimeInfoProvider).UpdateCompletedAt;
                data += string.Format("Update completed: {0}:{1}:{2}:{3}" + Environment.NewLine, finish.Hour, finish.Minute, finish.Second, finish.Millisecond);
                data += string.Format("Update duration: {0}" + Environment.NewLine, (node as ITimeInfoProvider).UpdateDuration);
            }
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

        public string GetLoggedNodesDetails()
        {
            string data = "";

            foreach (var d in _loggedNodesDetails)
            {
                data += d + Environment.NewLine + Environment.NewLine;
            }

            return data;
        }

        #endregion
    }
}
