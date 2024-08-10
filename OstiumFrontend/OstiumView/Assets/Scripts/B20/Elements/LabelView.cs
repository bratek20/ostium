using B20.Frontend.Element;
using TMPro;

namespace B20.View
{
    public class LabelView : ElementView<LabelVM>
    {
        private TextMeshProUGUI text;

        protected override void OnBind()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnViewModelUpdate()
        {
            text.text = ViewModel.Model;
        }
    }
}

