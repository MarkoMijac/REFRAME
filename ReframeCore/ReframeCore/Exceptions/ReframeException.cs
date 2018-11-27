using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class ReframeException : Exception
    {
        public ReframeException(string message) : base(message)
        {

        }
    }
}
