using SQLite;

namespace FitnessHelper.Code
{
    public class Weight
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int WeightNum { get; set; }
    }
}
