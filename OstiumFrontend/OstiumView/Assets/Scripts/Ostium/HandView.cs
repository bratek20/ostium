using B20.View;
using GameComponents.Api;
using GameSetup.Api;
using UnityEngine;

namespace Ostium.View
{
    public class HandView: MonoBehaviour
    {
        [SerializeField]
        private CreateCardView card1;
        [SerializeField]
        private CreateCardView card2;
        
        public void Init(Hand hand)
        {
            card1.Init(hand.GetCards()[0]);
            card2.Init(hand.GetCards()[1]);  
        }
    }
}