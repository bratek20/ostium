using B20.Frontend.Elements.View;
using B20.Frontend.UiElements;
using TMPro;
using Translations.ViewModel;

namespace B20.Frontend.Translations.View
{
    public class TranslatableLabelView : ElementView<TranslatableLabel>
    {
        private TextMeshProUGUI _text;

        protected override void OnBind()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnViewModelUpdate()
        {
            _text.text = ViewModel.GetTranslationValue();
        }
    }
}

