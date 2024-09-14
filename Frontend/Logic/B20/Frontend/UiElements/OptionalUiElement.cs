using B20.Ext;

namespace B20.Frontend.UiElements
{
    public class OptionalUiElement<TViewModel, TModel> :
        UiElement<Optional<TModel>> where TViewModel : UiElement<TModel>
    {
        public TViewModel Element { get; }

        public OptionalUiElement(TViewModel element)
        {
            Element = element;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Model.Let(m => Element.Update(m));
        }
    }
}