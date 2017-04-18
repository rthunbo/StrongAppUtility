namespace StrongAppData.ExportedData
{
    using System;

    public class StrongAppTrainingData
    {
        public DateTime Date { get; set; }

        public string WorkoutName { get; set; }

        public string ExerciseName { get; set; }

        public int SetOrder { get; set; }

        public float Kg { get; set; }

        public int Reps { get; set; }

        public float Km { get; set; }

        public int Seconds { get; set; }

        public string Notes { get; set; }
    }
}
