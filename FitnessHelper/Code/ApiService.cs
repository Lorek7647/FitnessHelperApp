namespace FitnessHelper.Code
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_BASE_URL);
        }
        public const string API_BASE_URL = "https://zenquotes.io/api/";
        public async Task<Class1[]> GetInformation()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
            var response = await _httpClient.GetAsync("random/[your_key]");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            Class1[] result = System.Text.Json.JsonSerializer.Deserialize<Class1[]>(content);
            return result;
        }
    }
    public class Class1
    {
        public string q { get; set; }
        public string a { get; set; }
        public string h { get; set; }
    }
}
