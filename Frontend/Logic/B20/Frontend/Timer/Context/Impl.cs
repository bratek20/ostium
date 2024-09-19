using B20.Architecture.Contexts.Api;
using B20.Frontend.Timer.Api;
using B20.Frontend.Timer.Impl;

namespace B20.Frontend.Timer.Context
{
    public class TimerImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<TimerApi, TimerApiLogic>();
        }
    }
}