using ReframeBaseExceptions;

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
