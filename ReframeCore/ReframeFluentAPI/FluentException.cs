using ReframeBaseExceptions;

namespace ReframeCoreFluentAPI
{
    public class FluentException : ReframeException
    {
        public FluentException(string message)
            :base(message)
        {

        }
    }
}
