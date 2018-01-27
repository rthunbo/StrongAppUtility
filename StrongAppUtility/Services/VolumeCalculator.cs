namespace StrongAppData.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using StrongAppData.Model;
    using StrongAppData.Repositories;

    public class VolumeCalculator
    {
        private readonly ExerciseRepository exerciseRepository;
        private readonly MeasurementRepository measurementRepository;

        public VolumeCalculator(ExerciseRepository exerciseRepository, MeasurementRepository measurementRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.measurementRepository = measurementRepository;
        }

        public IEnumerable<Volume> GetWorkoutVolume(Workout w)
        {
            var exerciseVolumes = w.Work
                .GroupBy(x => x.ExerciseName)
                .Select(g => new
                {
                    BodyPart = exerciseRepository.GetExercise(g.Key).BodyPart,
                    Volume = g.Sum(x => GetWorkItemVolume(w, x))
                })
                .GroupBy(x => x.BodyPart)
                .Select(g => new Volume
                {
                    BodyPart = g.Key,
                    Value = g.Sum(x => x.Volume)
                });

            return exerciseVolumes;
        }

        private float GetWorkItemVolume(Workout workout, Workout.WorkItem workItem)
        {
            float kg = workItem.Kg;
            var exercise = exerciseRepository.GetExercise(workItem.ExerciseName);
            if (exercise.Category == Category.WeightedBodyweight)
            {
                var measurement = measurementRepository.GetMeasurement(workout.Date);
                if (measurement != null)
                {
                    kg += measurement.Weight;
                }
            }
            else if (exercise.Category == Category.AssistedBodyweight)
            {
                kg *= -1;
                var measurement = measurementRepository.GetMeasurement(workout.Date);
                if (measurement != null)
                {
                    kg += measurement.Weight;
                }
            }
            return kg * workItem.Reps;
        }

    }
}
