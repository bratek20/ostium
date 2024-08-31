using System;
using System.Collections.Generic;
using B20.Frontend.Element;

namespace B20.Frontend.Elements
{
    public class ElementListVm<TViewModel, TModel>: 
        ElementVm<List<TModel>> where TViewModel: ElementVm<TModel>
    {
        public List<TViewModel> Elements { get; private set; } = new List<TViewModel>();

        private Func<TViewModel> elementFactory;
        public ElementListVm(Func<TViewModel> elementFactory)
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