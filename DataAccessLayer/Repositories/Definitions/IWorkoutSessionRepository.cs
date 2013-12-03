using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Repositories.Base;
using Model;

namespace DataAccessLayer.Repositories.Definitions
{
    public interface IWorkoutSessionRepository:IRepository<WorkoutSession>
    {
        IQueryable<WorkoutSession> GetAllForkWorkout(int workoutId);

    }
}