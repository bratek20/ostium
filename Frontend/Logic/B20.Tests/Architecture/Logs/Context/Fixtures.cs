using B20.Architecture.Contexts.Api;
using B20.Architecture.Logs.Api;
using B20.Architecture.Logs.Fixtures;

namespace B20.Tests.Architecture.Logs.Context
{
    public class LogsMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<Logger, LoggerMock>();
        }
    }
}