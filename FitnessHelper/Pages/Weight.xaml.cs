using FitnessHelper.Code;

namespace FitnessHelper.Pages
{
public partial class Weight : ContentPage
{
    LocalDbService db;
    List<Code.Weight> weightList = new List<Code.Weight>();
    List<Gender> genderList = new List<Gender>();
    List<Age> ageList = new List<Age>();
    List<Height> heightList = new List<Height>();
    public string Gender { get; set; }
    public int Age { get; set; }
    public int Height2 { get; set; }
    public int Weight2 { get; set; }
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
    public Weight()
	{
        //fourth entry point
		InitializeComponent();
        db = new LocalDbService();
        UpdateEverything();
    }
    public void UpdateEverything() 
    {
        Task.Run(async () =>
        {
            await UpdateFieldsOnStart();
            await GetData();

        }).GetAwaiter().GetResult();
        CalculateData();
    }
    private void Page_Loaded(object sender, EventArgs e)
    {
        UpdateEverything();
    }
    private async Task UpdateFieldsOnStart()
    {
        try
        {
            weightList = await db.GetWeightList();
            foreach (Code.Weight weight in weightList)
            {
                WeightEntry.Placeholder = "Weight: " + weight.WeightNum;
                Weight2 = weight.WeightNum;
            }
        }
        catch (Exception)
        {
            WeightEntry.Placeholder = "Weight: ";
        }
    }
    private void CalculateData()
    {
        ResultList = new List<string>
        {

        };
        BindingContext = this;
        if (Weight2 > 0)
        {
            ResultList = new List<string>
            { 
                
            };
            BindingContext = this;
            string baseWeight = string.Format("Your weight: {0}kg", Weight2);
            string normalWeight = string.Format("Normal weight: {0}kg", Height2-100);
            double heightM = (double)Height2 / 100;
            double bmi = Weight2 / Math.Pow(heightM, 2);
            string bmiWeight = string.Format("Your BMI: {0}", Math.Round(bmi, 1));
            string diagnosis = "";
            string hint = "";
            if (bmi < 18.5)
            {
                diagnosis = string.Format("Your diagnosis: Underweight");
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                diagnosis = string.Format("Your diagnosis: Normal weight");
            }
            else if (bmi >= 25 && bmi <= 29.9)
            {
                diagnosis = string.Format("Your diagnosis: Overweight");
            }
            else if (bmi >= 30 && bmi <= 34.9)
            {
                diagnosis = string.Format("Your diagnosis: Obesity grade I");
            }
            else if (bmi >= 35 && bmi <= 39.9)
            {
                diagnosis = string.Format("Your diagnosis: Obesity grade II");
            }
            else
            {
                diagnosis = string.Format("Your diagnosis: Obesity grade III");
            }
            if (Age < 18 || Age > 40)
            {
                hint = string.Format("Your diagnosis may vary because of age and gender");
            }
            else
            {
                hint = string.Format("Your diagnosis may vary because of gender");
            }
            ResultList = new List<string>
            {
                baseWeight,
                normalWeight,
                bmiWeight,
                diagnosis,
                hint
            };
            BindingContext = this;
        }
        else
        {
            ResultList = new List<string>
            {

            };
            BindingContext = this;
        }
    }
    private async Task GetData()
    {
        try
        {
            genderList = await db.GetGenderList();
            foreach (Gender gender in genderList)
            {
                Gender = gender.GenderType;
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Can't find Gender", "Ok");
        }
        try
        {
            ageList = await db.GetAgeList();
            foreach (Age age in ageList)
            {
                Age = age.AgeNum;
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Can't find Age", "Ok");
        }
        try
        {
            heightList = await db.GetHeightList();
            foreach (Height height in heightList)
            {
                Height2 = height.HeightNum;
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Can't find Height", "Ok");
        }
    }
    private async void OnEntryCompleted(object sender, EventArgs e)
    {
        var entry = (Entry)sender;
        string enteredText = entry.Text;
        int num;
        if (InputValidator.EmptyCheck(enteredText))
        {
            if (InputValidator.NumberCheck(enteredText) != 0)
            {
                if (entry == WeightEntry)
                {
                    WeightEntry.Placeholder = "Weight: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    Code.Weight weight = new Code.Weight();
                    weight.WeightNum = num;
                    Weight2 = num;
                    try
                    {
                        await db.SaveWeight(weight);
                        UpdateEverything();
                    }
                    catch (Exception eee)
                    {
                        await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                    }
                }       
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Only positive numbers are valid", "Ok");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Fill out everything for accurate results", "Ok");
        }
    }
}
}