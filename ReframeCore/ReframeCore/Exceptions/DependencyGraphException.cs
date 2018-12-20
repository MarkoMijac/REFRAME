using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class DependencyGraphException : ReframeException
    {
        public DependencyGraphException()
            :base("Generic dependency graph exception!")
        {

        }

        public DependencyGraphException(string message)
            :base(message)
        {

        }
    }
}
