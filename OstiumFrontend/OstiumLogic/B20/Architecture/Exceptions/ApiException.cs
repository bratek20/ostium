using System;

namespace B20.Architecture.Exceptions
{
    public class ApiException: Exception
    {
        public ApiException(string message): base(message)
        {
        }
        
        public ApiException()
        {
        }
    }
}