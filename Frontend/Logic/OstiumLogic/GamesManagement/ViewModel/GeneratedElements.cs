using System;
using System.Collections.Generic;
using B20.Frontend.Traits;
using B20.Frontend.UiElements;
using GamesManagement.Api;

namespace GamesManagement.ViewModel
{
    public partial class CreatedGameVm: UiElement<CreatedGame>
    {
        public Label Id { get; set;  }
        public Label Creator { get; set; }
        public OptionalLabel Joiner { get; set; }
        public Button Delete { get; set; }
        
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type>()
            {
                typeof(Clickable)
            };
        }

        protected override void OnUpdate()
        {
            Id.Update(Model.GetId().Value);
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