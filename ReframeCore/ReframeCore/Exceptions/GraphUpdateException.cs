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
        public GraphUpdateException()
            : base("Generic exception in graph update process.")
        {

        }

        public GraphUpdateException(string message)
            :base(message)
        {

        }
    }
}
