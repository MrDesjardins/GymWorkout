using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dto;
using MapperService.Base;
using MapperService.Implementations;
using Model;

namespace MapperService.Factory
{
    public class MapperFactory : IMapperFactory
    {
        private ModelDtoMapper<Exercise, FlattenExercise> _exercise;
        private ModelDtoMapper<Workout, FlattenWorkout> _workout;
 
        #region Implementation of IMapperFactory

        public List<ConcreteMapper> MapperProfiles { get; set; }

        public ModelDtoMapper<Exercise, FlattenExercise> Exercise
        {
            get { return _exercise ?? (_exercise = new ExerciseMapper()); }
        }

        public ModelDtoMapper<Workout, FlattenWorkout> Workout
        {
            get { return _workout ?? (_workout = new WorkoutMapper()); }
        }


        public TY Map<T, TY>(T source, TY destination)
        {
            return Mapper.Map(source, destination);
        }

        public TY Map<T, TY>(T source)
        {
            TY destination = default(TY);
            return this.Map(source, destination);
        }

        #endregion


        public ModelDtoMapper<TModel, TViewModel> GetMapper<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            var mapper = this.MapperProfiles.Single(d => d.Model == model.GetType() && d.ViewModel == viewModel.GetType());

            return mapper.Profile as ModelDtoMapper<TModel, TViewModel>;
        }



        public MapperFactory()
        {
            this.MapperProfiles = new List<ConcreteMapper>();
            this.MapperProfiles.Add(new ConcreteMapper(typeof(Exercise), typeof(FlattenExercise), this.Exercise));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(Workout), typeof(FlattenWorkout), this.Workout));
        }
    }
}