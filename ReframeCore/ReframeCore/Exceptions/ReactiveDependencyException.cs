using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class ReactiveDependencyException : ReframeException
    {
        public ReactiveDependencyException()
            :base("Generic reactive dependency exception")
        {

        }

        public ReactiveDependencyException(string message)
            :base(message)
        {

        }
    }
}
