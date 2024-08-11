using B20.Ext;
using B20.Frontend.Element;

namespace B20.Frontend.Elements
{
    public class OptionalElementVm<TViewModel, TModel> :
        ElementVm<Optional<TModel>> where TViewModel : ElementVm<TModel>
    {
        public TViewModel Element { get; }
        
        public OptionalElementVm(TViewModel element)
        {
            Element = element;
        }
    }
}