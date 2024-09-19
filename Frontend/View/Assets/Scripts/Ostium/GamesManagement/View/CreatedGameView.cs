using B20.Frontend.Elements.View;
using GamesManagement.ViewModel;
using UnityEngine;

namespace SingleGame.View
{
    public class CreatedGameView: ElementView<CreatedGameVm>
    {
        [SerializeField]
        private LabelView id;
        [SerializeField]
        private LabelView creator;

        protected override void OnBind()
        {
            base.OnBind();
            id.Bind(ViewModel.Id);
            creator.Bind(ViewModel.Creator);
        }
    }
}