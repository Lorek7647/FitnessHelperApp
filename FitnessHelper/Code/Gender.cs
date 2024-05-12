using SQLite;

namespace FitnessHelper.Code
{
    public class Gender
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GenderType { get; set; }
        public int SelectedIndex { get; set; }
    }
}
