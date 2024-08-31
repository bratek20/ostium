using B20.Frontend.Element;
using B20.Frontend.Elements;
using TMPro;

namespace B20.View
{
    public class LabelView : ElementView<LabelVm>
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

