using B20.Architecture.Exceptions;

namespace B20.Architecture.Contexts.Api
{
    public class ClassNotFoundInContextException : ApiException
    {
        public ClassNotFoundInContextException(string message) : base(message)
        {
        }
    }
    
    public class DependentClassNotFoundInContextException : ApiException
    {
        public DependentClassNotFoundInContextException(string message) : base(message)
        {
        }
    }
}