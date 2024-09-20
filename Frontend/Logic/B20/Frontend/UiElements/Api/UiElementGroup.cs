using System;
using System.Collections.Generic;
using B20.Ext;

namespace B20.Frontend.UiElements
{
    public class UiElementGroup<TViewModel, TModel>: 
        UiElement<List<TModel>> where TViewModel: UiElement<TModel>
    {
        public List<TViewModel> Elements { get; private set; } = new List<TViewModel>();

        private Optional<Action<TViewModel>> onElementCreated = Optional<Action<TViewModel>>.Empty();
        public void OnElementCreated(Action<TViewModel> action)
        {
            onElementCreated = Optional<Action<TViewModel>>.Of(action);
        }
        
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
                onElementCreated.Let(m => m(element));
                
                element.Update(m);
                
                return element;
            });
        }
    }
}