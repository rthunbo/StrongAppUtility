namespace StrongAppData.Repositories
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using StrongAppData.ExportedData;
    using StrongAppData.Model;

    public class WorkoutRepository
    {
        public IEnumerable<Workout> GetWorkouts()
        {
            var textReader = File.OpenText("strong.csv");

            var csv = new CsvReader(textReader);
            csv.Configuration.RegisterClassMap<StrongAppTrainingDataMap>();

            var records = csv.GetRecords<StrongAppTrainingData>().ToList();

            var workouts = GetWorkouts(records);
            return workouts;
        }

        private static IEnumerable<Workout> GetWorkouts(List<StrongAppTrainingData> records)
        {
            return records
                .GroupBy(r => new { r.Date, r.WorkoutName })
                .Select(g => new Workout
                {
                    Date = g.Key.Date,
                    Name = g.Key.WorkoutName,
                    Work = g.Select(x => new Workout.WorkItem
                    {
                        ExerciseName = x.ExerciseName,
                        Reps = x.Reps,
                        Kg = x.Kg,
                        SetOrder = x.SetOrder,
                    }).ToList(),
                });
        }
    }
}
