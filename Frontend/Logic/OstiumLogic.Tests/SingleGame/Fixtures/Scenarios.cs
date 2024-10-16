using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Events.Context;
using B20.Ext;
using B20.Frontend.Postion;
using B20.Frontend.Timer.Api;
using B20.Frontend.Traits;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.Windows.Api;
using B20.Tests.Architecture.Logs.Context;
using B20.Tests.Frontend.TestHelpers;
using B20.Tests.Frontend.Windows.Context;
using GamesManagement.Api;
using Ostium.Logic.Tests.GameModule.Context;
using SingleGame;
using SingleGame.Context;
using SingleGame.ViewModel;
using User.Api;

namespace Ostium.Logic.Tests
{
    public class Scenarios
    {
        public class InGameWindowContext
        {
            private OldGameWindow OldGameWindow { get; }
            public SingleGameApiMock GameApiMock { get; }
            private Context Context { get; }

            public TimerApi TimerApi => Context.Get<TimerApi>();
            
            public InGameWindowContext(OldGameWindow gameWindow, SingleGameApiMock gameApiMock, Context c)
            {
                OldGameWindow = gameWindow;
                GameApiMock = gameApiMock;
                Context = c;
            }

            public RowVm AttackRow => OldGameWindow.GameState.Table.MySide.AttackRow;
            public RowVm DefenseRow => OldGameWindow.GameState.Table.MySide.DefenseRow;
            
            public CreatureCardVm CardInAttackRow => AttackRow.Card.Element;
            public CreatureCardVm CardInDefenseRow => DefenseRow.Card.Element;
            
            public List<CreatureCardVm> CardsInHand => OldGameWindow.GameState.MyHand.Cards.Elements;
            public CreatureCardVm FirstCardInHand => CardsInHand[0];
            public CreatureCardVm SecondCardInHand => CardsInHand[1];
            
            public Optional<CreatureCardVm> SelectedCard => OldGameWindow.SelectedCard;
        }

        public class InGameWindowArgs
        {
            public Action<SingleGameBuilders.GameDef> Game = null;
            public Rect AttackRowRect { get; set; } = new Rect(0, 0, 100, 100);
            public Rect DefenseRowRect { get; set; } = new Rect(0, 100, 100, 100);
        }
        public InGameWindowContext InGameWindow(Action<InGameWindowArgs> init = null)
        {
            var args = new InGameWindowArgs();
            init?.Invoke(args);
            
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new ViewModelTesting(),

                    //reasonable mock
                    new SingleGameMocks(),
                    
                    //tested module
                    new SingleGameViewModel()
                ).Build();
            
            var apiMock = c.Get<SingleGameApiMock>();
            apiMock.SetGame(args.Game);
            
            var window = c.Get<OldGameWindow>();
            window.Open(new OldGameWindowState(new Username("testUser"), new GameId(42)));
            
            var nc = new InGameWindowContext(window, apiMock, c);
            nc.AttackRow.GetTrait<WithRect>().SetRectProvider(() => args.AttackRowRect);
            nc.DefenseRow.GetTrait<WithRect>().SetRectProvider(() => args.DefenseRowRect);
            return nc;
        }
    }
}