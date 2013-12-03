using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Dto;
using WorkoutPlanner.Services.Attributes;

namespace WorkoutPlanner.Services.Contracts.Services
{
    
    [ServiceContract]
    public interface IExerciseService
    {
        [OperationContract]
        IEnumerable<FlattenExercise> GetAllExercise();

        [OperationContract]
        FlattenExercise GetExercise(int uniqueIdentifier);
    }
}
