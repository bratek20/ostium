using B20.Architecture.Contexts.Api;
using GamesManagement.Api;
using Ostium.Logic.Tests.GamesManagement.Fixtures;

namespace Ostium.Logic.Tests.GamesManagement.Context
{
    public class GameManagementMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<GamesManagementApi, GamesManagementApiMock>();
        }
    }
}