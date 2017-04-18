namespace StrongAppData.Repositories
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using StrongAppData.ExportedData;
    using StrongAppData.Model;

    public class ExerciseRepository
    {
        private static IEnumerable<Exercise> exercises = GetExercises();

        public Exercise GetExercise(string name)
        {
            return exercises.Where(x => x.Name == name).FirstOrDefault();
        }

        private static IEnumerable<Exercise> GetExercises()
        {
            var textReader = File.OpenText("exercises.csv");

            var csv = new CsvReader(textReader);
            csv.Configuration.RegisterClassMap<StrongAppExerciseDataMap>();

            var records = csv.GetRecords<StrongAppExerciseData>().ToList();

            var exercises = GetExercises(records);
            return exercises;
        }

        private static IEnumerable<Exercise> GetExercises(List<StrongAppExerciseData> records)
        {
            return records
                .Select(x => new Exercise
                {
                    Name = x.Name,
                    BodyPart = x.BodyPart,
                    Category = x.Category,
                });
        }
    }
}
