using SQLite;

namespace FitnessHelper.Code
{
    public class TimerExercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciseTime { get; set; }
    }
}
