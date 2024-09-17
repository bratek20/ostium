using System.Collections.Generic;
using B20.Ext;
using B20.Logic.Utils;
using B20.Tests.ExtraAsserts;
using GamesManagement.Api;
using User.Api;

namespace Ostium.Logic.Tests.GamesManagement.Fixtures
{
    public class GamesManagementApiMock: GamesManagementApi
    {
        private Username lastCreateCreator;
        public GameId Create(Username creator)
        {
            lastCreateCreator = creator;
            return new GameId(1);
        }
        
        public void AssertCreateCalled(Username creator)
        {
            AssertExt.Equal(creator, lastCreateCreator);
        }

        public void Join(Username joiner, GameId gameId)
        {
            throw new System.NotImplementedException();
        }

        public List<CreatedGame> GetAllCreated()
        {
            return ListUtils.Of(
                CreatedGame.Create(
                    new GameId(1),
                    new Username("user1"),
                    Optional<Username>.Empty()
                )
            );
        }
    }
}