using GameComponents.Api;
using JetBrains.Annotations;
using UnityEngine;

namespace Ostium.View
{
    public class RowView: MonoBehaviour
    {
        [SerializeField]
        private CreateCardView card;

        public void Init(CreatureCard card)
        {
            if (card != null)
            {
                this.card.SetVisible(true);
            }
            else
            {
                this.card.SetVisible(false);
            }
        }
    }
}