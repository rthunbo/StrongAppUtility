namespace StrongAppData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StrongAppData.Model;
    using StrongAppData.Repositories;
    using StrongAppData.Extensions;
    using StrongAppData.Services;

    static class Program
    {
        static WorkoutRepository workoutRepository = new WorkoutRepository();
        static VolumeCalculator volumeCalculator = new VolumeCalculator(new ExerciseRepository(), new MeasurementRepository());

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
                    .SelectMany(w => volumeCalculator.GetWorkoutVolume(w))
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

    }
}
