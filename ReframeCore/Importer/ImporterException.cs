using ReframeBaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeImporter
{
    public class ImporterException : ReframeException
    {
        public ImporterException(string message) : base(message)
        {

        }
    }
}
