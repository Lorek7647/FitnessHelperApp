using FitnessHelper.Code;

namespace FitnessHelper.Pages
{
public partial class YOU : ContentPage
{
    LocalDbService db;
    List<Gender> genderList = new List<Gender>();
    List<Age> ageList = new List<Age>();
    List<Height> heightList = new List<Height>();
    public YOU()
    {
        InitializeComponent();
        db = new LocalDbService();
        UpdateFieldsOnStart();
    }
    private async void UpdateFieldsOnStart() 
    {
        try
        {
            genderList = await db.GetGenderList();
            foreach (Gender gender in genderList)
            {
                if (Gender.SelectedIndex == -1)
                {
                    Gender.SelectedIndex = gender.SelectedIndex;
                }
            }
        }
        catch (Exception)
        {
            Gender.Title = "Gender: ";
        }
        try
        {
            ageList = await db.GetAgeList();
            foreach (Age age in ageList)
            {
                AgeEntry.Placeholder = "Age: " + age.AgeNum;
            }
        }
        catch (Exception)
        {
            AgeEntry.Placeholder = "Age: ";
        }
        try
        {
            heightList = await db.GetHeightList();
            foreach (Height height in heightList)
            {
                HeightEntry.Placeholder = "Height: " + height.HeightNum;
            }
        }
        catch (Exception)
        {
            HeightEntry.Placeholder = "Height: ";
        } 
    }
    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        string selectedValue = (string)picker.Items[selectedIndex];
        Gender gender = new Gender();
        gender.GenderType = selectedValue;
        gender.SelectedIndex = selectedIndex;
        try
        {
            await db.SaveGender(gender);  
        }
        catch (Exception eee)
        {
            await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
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
                if (entry == AgeEntry)
                {
                    AgeEntry.Placeholder = "Age: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    Age age = new Age();
                    age.AgeNum = num;
                    try
                    {
                        await db.SaveAge(age);
                    }
                    catch (Exception eee)
                    {
                        await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                    }
                }
                if (entry == HeightEntry)
                {
                    HeightEntry.Placeholder = "Height: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    Height height = new Height();
                    height.HeightNum = num;
                    try
                    {
                        await db.SaveHeight(height);
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