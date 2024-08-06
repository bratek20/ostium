using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class HandView: ElementView<HandVM>
    {
        [SerializeField]
        private CreateCardView card1;
        [SerializeField]
        private CreateCardView card2;

        protected override void OnBind()
        {
            card1.Bind(Model.Card1);
            card2.Bind(Model.Card2);
        }
    }
}