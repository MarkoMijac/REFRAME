using ReframeBaseExceptions;

namespace ReframeCoreFluentAPI
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
