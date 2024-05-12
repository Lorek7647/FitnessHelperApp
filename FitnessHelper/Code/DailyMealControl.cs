using SQLite;

namespace FitnessHelper.Code
{
    public class DailyMealControl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MealTitle { get; set; }
        public string MealRecipe { get; set; }
        public string MealPicture { get; set; }
    }
}
