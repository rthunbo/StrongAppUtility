namespace StrongAppData.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using StrongAppData.ExportedData;
    using StrongAppData.Model;

    public class MeasurementRepository
    {
        private static IEnumerable<Measurement> measurements = GetMeasurements().OrderByDescending(x => x.Date);

        public Measurement GetMeasurement(DateTime date)
        {
            foreach (var measurement in measurements)
            {
                if (measurement.Date.CompareTo(date) < 0)
                {
                    return measurement;
                }
            }
            return measurements.LastOrDefault();
        }

        private static IEnumerable<Measurement> GetMeasurements()
        {
            var textReader = File.OpenText("measurements.csv");

            var csv = new CsvReader(textReader);
            csv.Configuration.RegisterClassMap<StrongAppMeasurementDataMap>();

            var records = csv.GetRecords<StrongAppMeasurementData>().ToList();

            var measurements = GetMeasurements(records);
            return measurements;
        }

        private static IEnumerable<Measurement> GetMeasurements(List<StrongAppMeasurementData> records)
        {
            return records
                .Select(x => new Measurement
                {
                    Date = x.Date,
                    Weight = x.Weight,
                });
        }
    }
}
