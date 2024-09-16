using B20.Architecture.Contexts.Api;
using SingleGame;
using SingleGame.Api;

namespace Ostium.Logic.Tests.GameModule.Context
{
    public class SingleGameMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<SingleGameApi, SingleGameApiMock>();
        }
    }
}