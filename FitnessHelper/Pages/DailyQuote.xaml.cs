using FitnessHelper.Code;

namespace FitnessHelper.Pages
{
public partial class DailyQuote : ContentPage
{
    LocalDbService db;
    private readonly ApiService _apiService;
    List<DailyQuoteControl> dailyQuoteControlList = new List<DailyQuoteControl>();
    private List<string> _resultList;
    public List<string> ResultList
    {
        get { return _resultList; }
        set
        {
            _resultList = value;
            OnPropertyChanged(nameof(ResultList));
        }
    }
    public DailyQuote()
	{
		InitializeComponent();
        db = new LocalDbService();
        HttpClient httpClient = new HttpClient();
        _apiService = new ApiService(httpClient);
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
            Class1[] data = await _apiService.GetInformation();
            if (data != null)
            {
                foreach (Class1 class1 in data)
                {
                    QuoteAPILabel.Text = $"\"{class1.q}\" - {class1.a}";
                }
            }
        }
        catch (Exception eee)
        {
            await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
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
        ResultList = new List<string>
        {

        };
        BindingContext = this;
        try
        {
            dailyQuoteControlList = await db.GetDailyQuoteControlList();
            foreach (DailyQuoteControl dailyQuoteControl in dailyQuoteControlList)
            {
                ResultList.Add(dailyQuoteControl.DailyQuote);
            }
        }
        catch (Exception eee)
        {
            await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
        }
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        string generatedQuote = QuoteAPILabel.Text;
        if (InputValidator.EmptyCheck(generatedQuote))
        {
            QuoteAPILabel.Text = "";
            DailyQuoteControl dailyQuoteControl = new DailyQuoteControl();
            dailyQuoteControl.DailyQuote = generatedQuote;
            try
            {
                await db.SaveDailyQuoteControl(dailyQuoteControl);
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
    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is string item)
            {
                try
                {
                    await db.DeleteDailyQuoteControl(item);   
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