using SQLite;

namespace FitnessHelper.Code
{
    public class DailyQuoteControl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DailyQuote { get; set; }
    }
}
