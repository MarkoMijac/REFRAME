using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class UpdateInfo
    {
        public DateTime UpdateStartedAt { get; private set; }
        public DateTime UpdateEndedAt { get; private set; }
        public TimeSpan UpdateDuration { get; private set; }

        public bool UpdateSuccessfull { get; private set; }
        public Exception ThrownException { get; private set; }

        public UpdateError ErrorData { get; private set; }
        public INode InitialNode { get; set; }
        public string CauseMessage {
            get
            {
                if (InitialNode == null)
                {
                    return "Complete graph update requested!";
                }
                else
                {
                    return "Update caused by triggering individual node!";
                }
            }
        }

        public UpdateInfo()
        {

        }

        public void StartUpdate()
        {
            UpdateStartedAt = DateTime.Now;
        }

        public void EndUpdate()
        {
            UpdateEndedAt = DateTime.Now;
            UpdateDuration = UpdateEndedAt.Subtract(UpdateStartedAt);
            UpdateSuccessfull = true;
        }

        public void SaveErrorData(Exception sourceException, IDependencyGraph graph, INode failedNode)
        {
            ErrorData = new UpdateError(sourceException, graph, failedNode);
        }
    }
}
