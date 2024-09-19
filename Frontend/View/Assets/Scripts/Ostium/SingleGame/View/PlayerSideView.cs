using B20.Frontend.Elements.View;
using SingleGame.Api;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class PlayerSideView: ElementView<PlayerSideVm>
    {
        [SerializeField]
        private RowView attackRow;
        [SerializeField]
        private RowView defenseRow;

        protected override void OnBind()
        {
            base.OnBind();
        
            attackRow.Bind(ViewModel.AttackRow);
            defenseRow.Bind(ViewModel.DefenseRow);
        }
    }
}