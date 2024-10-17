using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Tests.ExtraAsserts;
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
                i.Hand = hi =>
                {
                    hi.Cards = new List<Action<KaijuGameBuilders.CardDef>>
                    {
                        c => c.FocusCost = 1
                    };
                };
            });
        }

        private bool endPhaseCalled = false;
        public GameState EndPhase(GameToken token)
        {
            endPhaseCalled = true;
            return GetState(token);
        }
        
        public void AssertEndPhaseCalled()
        {
            AssertExt.Equal(endPhaseCalled, true);
        }

        private int playedCardIdx = -1;
        public GameState PlayCard(GameToken token, int handCardIdx)
        {
            playedCardIdx = handCardIdx;
            return GetState(token);
        }
        
        public void AssertPlayCardLastCall(int expectedIdx)
        {
            AssertExt.Equal(playedCardIdx, expectedIdx);
        }
        
        public void AssertPlayCardNotCalled()
        {
            AssertExt.Equal(playedCardIdx, -1);
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