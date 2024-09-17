using System.Collections.Generic;
using GamesManagement.Api;
using User.Api;

namespace Ostium.Logic.Tests.GamesManagement.Fixtures
{
    public class GamesManagementApiMock: GamesManagementApi
    {
        public GameId Create(Username creator)
        {
            throw new System.NotImplementedException();
        }

        public void Join(Username joiner, GameId gameId)
        {
            throw new System.NotImplementedException();
        }

        public List<CreatedGame> GetAllCreated()
        {
            throw new System.NotImplementedException();
        }
    }
}