using System;
using System.Collections.Generic;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;
using B20.Frontend.UiElements;
using KaijuGame.Api;
using GamesManagement.Api;
using SingleGame.ViewModel;

namespace KaijuGame.ViewModel {
    class GameElementDragEndedListener: EventListener<ElementDragEndedEvent>
    {
        private GameWindow window;
        public GameElementDragEndedListener(GameWindow window)
        {
            this.window = window;
        }
        
        public void HandleEvent(ElementDragEndedEvent e)
        {
            window.HandleElementDragEnded(e);
        }
    }
    
    public partial class GameWindow
    {
        private GameApi api;

        public GameWindow(GameApi api, EventPublisher eventPublisher)
        {
            this.api = api;
            
            eventPublisher.AddListener(new GameElementDragEndedListener(this));
        }

        protected override void OnOpen()
        {
            EndPhaseButton.OnClick(OnEndPhaseButtonClick);
            
            GameState.Update(api.GetState(State.Token));
        }
        
        private void OnEndPhaseButtonClick()
        {
            GameState.Update(api.EndPhase(State.Token));
        }
        
        public void HandleElementDragEnded(ElementDragEndedEvent e)
        {
            if(e.Element is CardVm card)
            {
                var handIdx = GameState.Hand.Cards.Elements.IndexOf(card);
                if (GameState.Table.MySide.GetTrait<WithRect>().IsInside(e.Position))
                {
                    GameState.Update(api.PlayCard(State.Token, handIdx));    
                }
            }
        }
    }
}