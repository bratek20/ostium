using B20.Architecture.Exceptions;

namespace B20.Architecture.Contexts.Api
{
    public class DependentClassNotFoundInContextException : ApiException
    {
        public DependentClassNotFoundInContextException(string message) : base(message)
        {
        }
    }
}