using System.Net.Http.Json;

namespace FitnessHelper.Code
{
    public class ApiServiceMeal
    {
        private readonly HttpClient _httpClientMeal;
        public ApiServiceMeal(HttpClient httpClientMeal)
        {
            _httpClientMeal = httpClientMeal;
            _httpClientMeal.BaseAddress = new Uri(API_BASE_URL);
        }
        public const string API_BASE_URL = "https://www.themealdb.com/api/";
        public async Task<Rootobject> GetInformation()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
            return await _httpClientMeal.GetFromJsonAsync<Rootobject>("json/v1/1/random.php");
        }
    }
    public class Rootobject
    {
        public Meal[] meals { get; set; }
    }
    public class Meal 
    {
        public string idMeal { get; set; }
        public string strMeal { get; set; }
        public object strDrinkAlternate { get; set; }
        public string strCategory { get; set; }
        public string strArea { get; set; }
        public string strInstructions { get; set; }
        public string strMealThumb { get; set; }
        public string strTags { get; set; }
        public string strYoutube { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        public object strIngredient16 { get; set; }
        public object strIngredient17 { get; set; }
        public object strIngredient18 { get; set; }
        public object strIngredient19 { get; set; }
        public object strIngredient20 { get; set; }
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }
        public object strMeasure16 { get; set; }
        public object strMeasure17 { get; set; }
        public object strMeasure18 { get; set; }
        public object strMeasure19 { get; set; }
        public object strMeasure20 { get; set; }
        public object strSource { get; set; }
        public object strImageSource { get; set; }
        public object strCreativeCommonsConfirmed { get; set; }
        public object dateModified { get; set; }
    }
}
