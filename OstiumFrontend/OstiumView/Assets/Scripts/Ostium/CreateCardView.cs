using B20.View;
using GameComponents.Api;
using UnityEngine;

namespace Ostium.View
{
    public class CreateCardView: MonoBehaviour
    {
        [SerializeField]
        private LabelView name;
        
        public void Init(CreatureCard card)
        {
            name.Init(card.GetId().Value);    
        }
    }
}