using System;
using System.Collections.Generic;
using B20.Frontend.Traits;
using B20.Frontend.UiElements;
using GamesManagement.Api;

namespace GamesManagement.ViewModel
{
    //CreatedGameVm (clickable)
    //main -> CreatedGame
    //id //(debug)
    //creator
    //joiner
    
    public partial class CreatedGameVm: UiElement<CreatedGame>
    {
        public Label Id { get; } = new Label();
        public Label Creator { get; } = new Label();
        public OptionalUiElement<Label, string> Joiner { get; } = new OptionalUiElement<Label, string>(new Label());

        protected override List<Type> GetTraitTypes()
        {
            return new List<Type>()
            {
                typeof(Clickable)
            };
        }

        protected override void OnUpdate()
        {
            Id.Update(Model.GetId().Value.ToString());
            Creator.Update(Model.GetCreator().Value);
            Joiner.Update(Model.GetJoiner().Map(it => it.Value));
        }
    }
    
    public class CreatedGameVmGroup: UiElementGroup<CreatedGameVm, CreatedGame>
    {
        public CreatedGameVmGroup(B20.Architecture.Contexts.Api.Context c) : base(() => c.Get<CreatedGameVm>())
        {
            
        }
    }
}