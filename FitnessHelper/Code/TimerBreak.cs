using SQLite;

namespace FitnessHelper.Code
{
    public class TimerBreak
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int BreakTime { get; set; }
    }
}
