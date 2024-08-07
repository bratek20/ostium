using B20.Frontend.Elements;
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

        protected override void OnModelUpdate()
        {
            text.text = Model.Model;
        }
    }
}

