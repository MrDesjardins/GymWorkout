using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class WorkoutSessionOrder
    {
        public int WorkoutId { get; set; }
        public List<int> OrderedWorkoutSessionList { get; set; }
    }
}
