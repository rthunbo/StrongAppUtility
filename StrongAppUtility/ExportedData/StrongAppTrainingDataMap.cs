namespace StrongAppData.ExportedData
{
    using CsvHelper.Configuration;
    using System.Globalization;

    public class StrongAppTrainingDataMap : CsvClassMap<StrongAppTrainingData>
    {
        public StrongAppTrainingDataMap()
        {
            Map(m => m.Date).Index(0).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.WorkoutName).Index(1).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.ExerciseName).Index(2).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.SetOrder).Index(3).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Kg).Index(4).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Reps).Index(5).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Km).Index(6).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Seconds).Index(7).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Notes).Index(8).TypeConverterOption(CultureInfo.InvariantCulture);
        }
    }
}
