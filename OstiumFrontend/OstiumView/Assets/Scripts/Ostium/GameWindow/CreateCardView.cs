using B20.View;
using GameComponents.Api;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class CreateCardView: PanelView
    {
        [SerializeField]
        private LabelView name;

        protected override void OnBind()
        {
            name.Bind((Model as CreatureCardVM).Name);
        }
    }
}