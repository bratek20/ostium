using B20.Frontend.UiElements;
using TMPro;

namespace B20.Frontend.Elements.View
{
    public class LabelView : ElementView<Label>
    {
        private TextMeshProUGUI _text;

        protected override void OnBind()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnViewModelUpdate()
        {
            _text.text = ViewModel.Model;
        }
    }
}

