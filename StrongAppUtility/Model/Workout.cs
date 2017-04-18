namespace StrongAppData.Model
{
    using System;
    using System.Collections.Generic;

    public class Workout
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public List<WorkItem> Work { get; set; }

        public class WorkItem
        {
            public string ExerciseName { get; set; }

            public int SetOrder { get; set; }

            public int Reps { get; set; }

            public float Kg { get; set; }
        }
    }
}
