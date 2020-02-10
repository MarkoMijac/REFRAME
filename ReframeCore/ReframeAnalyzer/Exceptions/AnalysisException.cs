using ReframeBaseExceptions;

namespace ReframeAnalyzer.Exceptions
{
    public class AnalysisException : ReframeException
    {
        public AnalysisException()
            :base("Generic analysis exception")
        {

        }

        public AnalysisException(string message)
            :base(message)
        {

        }
    }
}
