using B20.Architecture.Contexts.Api;
using B20.Architecture.Logs.Api;
using B20.Architecture.Logs.Integrations;

namespace B20.Architecture.Logs.Context
{
    public class UnityLogsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<Logger, UnityLogger>();
        }
    }
}