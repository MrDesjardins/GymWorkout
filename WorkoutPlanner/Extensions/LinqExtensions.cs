using System.Collections.Generic;
using System.Linq;
using ViewModels.Selectors.Base;
using ViewModels.Selectors.Implementations;

namespace WorkoutPlanner.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<ISelector> AsSelector<T>(this IQueryable<T> query) where T: ISelector
        {
            foreach (var item in query)
            {
                yield return new Selector(item);
            }
        }
    }
}