using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class NodeNullReferenceException : ReframeException
    {
        public NodeNullReferenceException(string message) : base(message)
        {
            
        }

        public NodeNullReferenceException()
            :base("Reactive node should not be null!")
        {

        }
    }
}
