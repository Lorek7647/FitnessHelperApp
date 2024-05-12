using FitnessHelper.Code;

namespace FitnessHelper.Pages
{
public partial class DailyMeal : ContentPage
{
    LocalDbService db;
    private readonly ApiServiceMeal _apiServiceMeal;
    List<DailyMealControl> dailyMealControlList = new List<DailyMealControl>();
    private List<DailyMealControl> _resultList;
    public List<DailyMealControl> ResultList
    {
        get { return _resultList; }
        set
        {
            _resultList = value;
            OnPropertyChanged(nameof(ResultList));
        }
    }
    public string TitleHolder { get; set; }
    public string ImageHolder { get; set; }
    public string RecipeHolder { get; set; }
    public DailyMeal()
    {
        InitializeComponent();
        db = new LocalDbService();
        HttpClient httpClientMeal = new HttpClient();
        _apiServiceMeal = new ApiServiceMeal(httpClientMeal);
        UpdateEverything();
    }
    private async void OnGenerateButtonClicked(object sender, EventArgs e)
    {
        await ProcessInformation();
    }
    public async Task ProcessInformation()
    {
        try
        {
            var rootobject = await _apiServiceMeal.GetInformation();
            if (rootobject != null)
            {
                foreach (Meal meal in rootobject.meals)
                {
                    TitleHolder = meal.strMeal;
                    MealAPITitle.Text = TitleHolder;
                    RecipeHolder = meal.strInstructions;
                    ImageHolder = meal.strMealThumb;
                    MealAPIPicture.Source = ImageHolder;
                }
            }
        }
        catch (Exception eee)
        {
            await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
        }
    }
    private void Page_Loaded(object sender, EventArgs e)
    {
        MealAPIPicture.Source = null;
        if (string.IsNullOrEmpty(ImageHolder))
        {
            MealAPIPicture.Source = "placeholder_img.jpg";
        }
        else
        {
            MealAPIPicture.Source = ImageHolder;
        }
    }
    public void UpdateEverything()
    {
        Task.Run(async () =>
        {
            await UpdateCollectionView();
        }).GetAwaiter().GetResult();
    }
    private async Task UpdateCollectionView()
    {
        ResultList = new List<DailyMealControl>
        {

        };
        BindingContext = this;
        try
        {
            dailyMealControlList = await db.GetDailyMealControlList();
            foreach (DailyMealControl dailyMealControl in dailyMealControlList)
            {
                ResultList.Add(dailyMealControl);
            }
        }
        catch (Exception eee)
        {
            await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
        }
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        string mealT = TitleHolder;
        string mealR = RecipeHolder;
        string mealP = ImageHolder;
        if (InputValidator.EmptyCheck3(mealT,mealR,mealP))
        {
            TitleHolder = "";
            MealAPITitle.Text = "";
            RecipeHolder = "";
            ImageHolder = "";
            MealAPIPicture.Source = "placeholder_img.jpg";
            DailyMealControl dailyMealControl = new DailyMealControl();
            dailyMealControl.MealTitle = mealT;
            dailyMealControl.MealRecipe = mealR;
            dailyMealControl.MealPicture = mealP;
            try
            {
                await db.SaveDailyMealControl(dailyMealControl);
                UpdateEverything();
            }
            catch (Exception eee)
            {
                await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Nothing to save - Generate something", "Ok");
        }
    }
    private async void OnShowRecipeButtonClicked1(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (InputValidator.EmptyCheck(RecipeHolder))
            {
                try
                {
                    await Shell.Current.DisplayAlert(TitleHolder, RecipeHolder, "Ok");
                }
                catch (Exception eee)
                {
                    await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Nothing to show - Generate something", "Ok");
            }
        }
    }
    private async void OnShowRecipeButtonClicked2(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is DailyMealControl item)
            {
                try
                {
                    await Shell.Current.DisplayAlert(item.MealTitle, item.MealRecipe, "Ok");
                }
                catch (Exception eee)
                {
                    await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                }
            }
        }
    }
    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is DailyMealControl item)
            {
                try
                {   
                    await db.DeleteDailyMealControl(item);
                    UpdateEverything();
                }
                catch (Exception eee)
                {
                    await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                }
            }
        }
    }
} 
}