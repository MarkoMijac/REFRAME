using ReframeBaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class CyclicReactiveDependencyException : ReframeException
    {
        public CyclicReactiveDependencyException()
            :base("There are cycles identified in dependency graph!")
        {

        }
    }
}
