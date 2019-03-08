using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class ReactiveCollectionException : ReframeException
    {
        public ReactiveCollectionException()
            : base("Generic ReactiveCollection exception")
        {

        }

        public ReactiveCollectionException(string message)
            :base(message)
        {

        }
    }
}
