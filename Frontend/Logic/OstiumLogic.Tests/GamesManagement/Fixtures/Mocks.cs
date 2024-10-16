using System.Collections.Generic;
using B20.Ext;
using B20.Frontend.UiElements.Utils;
using B20.Tests.ExtraAsserts;
using GamesManagement.Api;
using User.Api;

namespace Ostium.Logic.Tests.GamesManagement.Fixtures
{
    public class GamesManagementApiMock: GamesManagementApi
    {
        private Username lastCreateCreator;
        public GameToken Create(Username creator)
        {
            lastCreateCreator = creator;
            return new GameToken(666, creator.Value);
        }
        
        public void AssertCreateCalled(Username creator)
        {
            AssertExt.Equal(creator, lastCreateCreator);
        }

        private Username lastJoinJoiner;
        private GameId lastJoinGameId;
        public GameToken Join(Username joiner, GameId gameId)
        {
            lastJoinJoiner = joiner;
            lastJoinGameId = gameId;
            return new GameToken(gameId.Value, joiner.Value);
        }

        private GameId lastDeleteGameId;
        public void Delete(GameId gameId)
        {
            lastDeleteGameId = gameId;
        }
        
        public void AssertDeleteCalled(int gameId)
        {
            AssertExt.Equal(gameId, lastDeleteGameId.Value);
        }

        public void AssertJoinCalled(string joiner, int gameId)
        {
            AssertExt.Equal(joiner, lastJoinJoiner.Value);
            AssertExt.Equal(gameId, lastJoinGameId.Value);
        }

        private int getCreatedCalls = 0;
        public List<CreatedGame> GetAllCreated()
        {
            getCreatedCalls++;
            return ListUtils.Of(
                CreatedGame.Create(
                    new GameId(69),
                    new Username("user1"),
                    Optional<Username>.Empty()
                )
            );
        }
        
        public void AssertGetAllCreatedCalled(int expectedCalls)
        {
            AssertExt.Equal(getCreatedCalls, expectedCalls);
        }
    }
}