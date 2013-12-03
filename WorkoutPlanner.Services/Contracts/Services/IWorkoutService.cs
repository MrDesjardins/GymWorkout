using System.Collections.Generic;
using System.ServiceModel;
using Dto;

namespace WorkoutPlanner.Services.Contracts.Services
{
    [ServiceContract]
    public interface IWorkoutService
    {
        [OperationContract]
        IEnumerable<FlattenWorkout> GetAllWorkout();

        [OperationContract]
        FlattenWorkout GetWorkout(int uniqueIdentifier);
    }
}