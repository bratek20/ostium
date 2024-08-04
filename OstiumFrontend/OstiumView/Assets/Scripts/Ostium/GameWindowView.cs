using B20.View;
using GameSetup.Api;
using UnityEngine;

namespace Ostium.View
{
    public class GameWindowView: WindowView
    {
        [SerializeField]
        private HandView hand;
        
        public void Init2(Game game)
        {
            hand.Init(game.GetHand());    
        }
    }
}