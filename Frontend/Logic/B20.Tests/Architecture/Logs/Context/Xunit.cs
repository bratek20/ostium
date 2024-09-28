using B20.Architecture.Contexts.Api;
using B20.Architecture.Logs.Api;
using B20.Tests.Architecture.Logs.Integrations;
using Xunit.Abstractions;

namespace B20.Tests.Architecture.Logs.Context
{
    public class XunitLogsImpl: ContextModule
    {
        private readonly ITestOutputHelper output;

        public XunitLogsImpl(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Apply(ContextBuilder builder)
        {
            builder.SetImplObject<Logger>(new XunitLogger(output));
        }
    }
}