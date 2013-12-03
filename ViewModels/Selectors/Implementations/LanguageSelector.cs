using ViewModels.Selectors.Base;

namespace ViewModels.Selectors.Implementations
{
    public class LanguageSelector:Selector
    {
          public LanguageSelector():base()
        {
        }

        public LanguageSelector(string value, string displayText, bool isSelected = false) : base(value, displayText, isSelected)
        {
        }

        public LanguageSelector(ISelector selector)
            : base(selector)
        {
        }
    }
}