using B20.Tests.ExtraAsserts;
using Translations.Api;
using Translations.ViewModel;
using Xunit;

namespace B20.Frontend.Translations.Tests
{
    public class ViewModelTest
    {
        [Fact]
        public void ShouldTranslateKeyToItsValue()
        {
            var viewModel = new TranslatableLabel();

            viewModel.Update(new TranslationKey("some-key"));
            
            AssertExt.Equal(viewModel.GetTranslationValue(), "some-key");
        }
    }
}