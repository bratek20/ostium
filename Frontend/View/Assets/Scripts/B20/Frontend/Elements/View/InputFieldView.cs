using B20.Frontend.UiElements;
using TMPro;

namespace B20.Frontend.Elements.View
{
    public class InputFieldView: ElementView<InputField>
    {
        private TMP_InputField _inputField;

        protected override void OnBind()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnViewModelUpdate()
        {
            _inputField.text = ViewModel.Model;
        }
        
        private void OnValueChanged(string newText)
        {
            ViewModel.OnChange(newText);
        }
    }
}