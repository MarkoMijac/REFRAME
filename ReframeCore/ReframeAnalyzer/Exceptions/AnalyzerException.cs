using ReframeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Exceptions
{
    public class AnalyzerException : ReframeException
    {
        public AnalyzerException()
            :base("Generic analyzer exception")
        {

        }

        public AnalyzerException(string message)
            :base(message)
        {

        }
    }
}
