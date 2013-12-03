using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model;
using Services.Base;
using ViewModels;

namespace Services.Definitions
{
    public interface IWorkoutSessionService : IService<WorkoutSession>
    {
        IEnumerable<WorkoutSessionViewModel> GetAllForWorkout(int workoutId);
        WorkoutViewModel GetWorkoutWithWorkoutSession(int workoutId);

        
    }
}