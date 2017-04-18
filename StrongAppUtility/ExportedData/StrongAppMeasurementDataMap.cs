namespace StrongAppData.ExportedData
{
    using System.Globalization;
    using CsvHelper.Configuration;

    public class StrongAppMeasurementDataMap : CsvClassMap<StrongAppMeasurementData>
    {
        public StrongAppMeasurementDataMap()
        {
            Map(m => m.Date).Index(0).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(m => m.Weight).Index(1).TypeConverterOption(CultureInfo.InvariantCulture);
        }
    }
}
