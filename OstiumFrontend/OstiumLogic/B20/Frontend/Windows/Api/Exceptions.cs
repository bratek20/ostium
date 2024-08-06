using B20.Architecture.Exceptions;

namespace B20.Frontend.Windows.Api
{
    public class WindowNotFoundException: ApiException
    {
        public WindowNotFoundException(string message): base(message)
        {
        }
    }
}