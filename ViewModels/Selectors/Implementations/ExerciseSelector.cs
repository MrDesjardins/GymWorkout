using ViewModels.Selectors.Base;

namespace ViewModels.Selectors.Implementations
{
    public class ExerciseSelector:Selector
    {
        public ExerciseSelector():base()
        {
        }

        public ExerciseSelector(string value, string displayText, bool isSelected = false) : base(value, displayText, isSelected)
        {
        }

        public ExerciseSelector(ISelector selector) : base(selector)
        {
        }
        
    }
}