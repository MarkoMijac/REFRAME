using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class UpdaterException : ReframeException
    {
        public UpdaterException()
            :base("Generic updater exception")
        {

        }

        public UpdaterException(string message)
            :base(message)
        {

        }
    }
}
