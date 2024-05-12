using SQLite;

namespace FitnessHelper.Code
{
    public class TimerSets
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TotalSets { get; set; }
    }
}
