using SQLite;

namespace FitnessHelper.Code
{
    public class Age
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AgeNum { get; set; }
    }
}
