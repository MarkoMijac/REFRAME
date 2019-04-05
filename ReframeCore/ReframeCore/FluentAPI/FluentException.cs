using ReframeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public class FluentException : ReframeException
    {
        public FluentException()
            :base("Generic FluentAPI exception")
        {

        }

        public FluentException(string message)
            :base(message)
        {

        }
    }
}
