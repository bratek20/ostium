using B20.Architecture.Contexts.Api;
using GameModule;
using GameModule.Api;

namespace Ostium.Logic.Tests.GameModule.Context
{
    public class GameModuleMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<GameApi, GameApiMock>();
        }
    }
}