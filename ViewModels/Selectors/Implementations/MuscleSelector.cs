using ViewModels.Selectors.Base;

namespace ViewModels.Selectors.Implementations
{
    public class MuscleSelector:Selector
    {
        public MuscleSelector():base()
        {
        }

        public MuscleSelector(string value, string displayText, bool isSelected = false) : base(value, displayText, isSelected)
        {
        }

        public MuscleSelector(ISelector selector)
            : base(selector)
        {
        }
        
    }
}