using SQLite;

namespace FitnessHelper.Code
{
    public class Height
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int HeightNum { get; set; }
    }
}
