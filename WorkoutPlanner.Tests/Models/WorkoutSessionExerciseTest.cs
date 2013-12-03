using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class WorkoutSessionExerciseTest
    {
        private WorkoutSessionExercise _entity;
        private Mock<WorkoutSessionExercise> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new WorkoutSessionExercise();
            _mock = new Mock<WorkoutSessionExercise>(MockBehavior.Strict);
        }
    }
}