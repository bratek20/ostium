using System;
using System.Collections.Generic;

namespace B20.Frontend.UiElements
{
    public class UiElementGroup<TViewModel, TModel>: 
        UiElement<List<TModel>> where TViewModel: UiElement<TModel>
    {
        public List<TViewModel> Elements { get; private set; } = new List<TViewModel>();

        private Func<TViewModel> elementFactory;
        public UiElementGroup(Func<TViewModel> elementFactory)
        {
            this.elementFactory = elementFactory;
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            Elements = Model.ConvertAll(m => {
                var element = elementFactory();
                element.Update(m);
                return element;
            });
        }
    }
}