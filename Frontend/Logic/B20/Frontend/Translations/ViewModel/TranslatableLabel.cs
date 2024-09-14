using B20.Frontend.UiElements;
using Translations.Api;

namespace Translations.ViewModel
{
    public class TranslatableLabel: UiElement<TranslationKey>
    {
        public string GetTranslationValue()
        {
            return Model.Value;
        }
    }
}