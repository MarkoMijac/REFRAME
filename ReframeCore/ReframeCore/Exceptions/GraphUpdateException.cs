using ReframeBaseExceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class GraphUpdateException : ReframeException
    {
        public UpdateError ErrorData { get; private set; }

        public GraphUpdateException(UpdateError error, string message)
            :base(message)
        {
            ErrorData = error;
        }
    }
}
