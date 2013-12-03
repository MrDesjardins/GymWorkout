using ViewModels.Selectors.Base;

namespace ViewModels.Selectors.Implementations
{
    public class Selector : ISelector
    {
        #region Implementation of ISelector

        public string Value { get; set; }
        public string DisplayText { get; set; }
        public bool IsSelected { get; set; }

        #endregion
        public Selector()
        {
        }

        public Selector(string value, string displayText, bool isSelected=false)
        {
            this.Value = value;
            this.DisplayText = displayText;
            this.IsSelected = isSelected;
        }
        public Selector(ISelector selector)
        {
            this.Value = selector.Value;
            this.DisplayText = selector.DisplayText;
            this.IsSelected = selector.IsSelected;

        }
    }
}