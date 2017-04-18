using System;
using System.Collections.Generic;
using System.Linq;
using StrongAppData.Model;
using StrongAppData.Repositories;
using StrongAppData.Extensions;

namespace StrongAppData
{
    static class Program
    {
        static ExerciseRepository exerciseRepository = new ExerciseRepository();
        static WorkoutRepository workoutRepository = new WorkoutRepository();
        static MeasurementRepository measurementRepository = new MeasurementRepository();

        static void Main(string[] args)
        {
            var workouts = workoutRepository.GetWorkouts();
            PrintWeeklyVolume(workouts);

            Console.ReadLine();
        }

        private static void PrintWeeklyVolume(IEnumerable<Workout> workouts)
        {
            var weeklyWorkoutsMap = workouts
                .GroupBy(w => $"{w.Date.GetWeekNumber().Year}-{w.Date.GetWeekNumber().Week}")
                .Select(g => new { YearAndWeek = g.Key, Workouts = g.ToList() });

            foreach (var weeklyWorkouts in weeklyWorkoutsMap)
            {
                Console.WriteLine($"Week: {weeklyWorkouts.YearAndWeek}");

                var totalWeeklyVolumePerBodyPart = weeklyWorkouts.Workouts
                    .SelectMany(w => GetWorkoutVolume(w))
                    .GroupBy(x => x.BodyPart)
                    .Select(g => new Volume { BodyPart = g.Key, Value = g.Sum(x => x.Value) })
                    .OrderByDescending(x => x.Value);

                foreach (var volume in totalWeeklyVolumePerBodyPart)
                {
                    Console.WriteLine($"{volume.BodyPart}: {volume.Value} kgs");
                }

                Console.WriteLine($"Total volume: {totalWeeklyVolumePerBodyPart.Sum(x => x.Value)} kgs");
                Console.WriteLine();
            }
        }

        private static IEnumerable<Volume> GetWorkoutVolume(Workout w)
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

        private static float GetWorkItemVolume(Workout workout, Workout.WorkItem workItem)
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
            else if (exercise.Category == Category.Dumbbell)
            {
                kg *= 2;
            }
            return kg * workItem.Reps;
        }
    }
}
