using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class ReactiveNodeException : ReframeException
    {
        public ReactiveNodeException() 
            : base("Generic reactive node exception!")
        {

        }

        public ReactiveNodeException(string message) 
            : base(message)
        {

        }
    }
}
