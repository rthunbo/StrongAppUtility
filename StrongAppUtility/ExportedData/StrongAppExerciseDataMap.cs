namespace StrongAppData.ExportedData
{
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using StrongAppData.Model;
    using System.Globalization;

    public class StrongAppExerciseDataMap : CsvClassMap<StrongAppExerciseData>
    {
        public StrongAppExerciseDataMap()
        {
            Map(m => m.Name).Index(0).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.BodyPart).Index(1).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Category).Index(2).TypeConverterOption(CultureInfo.InvariantCulture);
        }
    }
}
