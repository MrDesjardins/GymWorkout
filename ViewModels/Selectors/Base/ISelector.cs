namespace ViewModels.Selectors.Base
{
    public interface ISelector
    {
        string Value { get; set;  }
        string DisplayText { get; set; }
        bool IsSelected { get; set; }
    }
}