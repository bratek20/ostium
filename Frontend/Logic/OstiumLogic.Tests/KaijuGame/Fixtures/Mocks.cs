using System;
using B20.Architecture.Contexts.Api;
using GamesManagement.Api;
using KaijuGame;
using KaijuGame.Api;

namespace Ostium.Logic.Tests.KaijuGame.Fixtures
{
    class GameApiMock : GameApi
    {
        public GameState GetState(GameToken token)
        {
            return KaijuGameBuilders.BuildGameState(i =>
            {
                i.Turn = 1;
            });
        }

        public GameState EndPhase(GameToken token)
        {
            throw new NotImplementedException();
        }

        public GameState PlayCard(GameToken token, int handCardIdx)
        {
            throw new NotImplementedException();
        }

        public GameState AssignDamage(GameToken token, HitZonePosition zone, DamageType damageType)
        {
            throw new NotImplementedException();
        }

        public GameState AssignGuard(GameToken token, HitZonePosition zone, DamageType damageType)
        {
            throw new NotImplementedException();
        }
    }

    class KaijuGameMocks : ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<GameApi, GameApiMock>();
        }
    }
}