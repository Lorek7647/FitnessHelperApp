using FitnessHelper.Code;

namespace FitnessHelper.Pages
{
public partial class Timer : ContentPage
{
    LocalDbService db;
    List<TimerBreak> breakList = new List<TimerBreak>();
    List<TimerExercise> exerciseList = new List<TimerExercise>();
    List<TimerSets> setsList = new List<TimerSets>();
    public int BreakTime { get; set; }
    public int ExerciseTime { get; set; }
    public int TotalSets { get; set; }
    private TimeOnly time = new();
    private bool isRunning;
    private int counter = 1;
    private int exerciseCounter = 1;
    public Timer()
	{
		InitializeComponent();
        db = new LocalDbService();
        UpdateFieldsOnStart();
        TitleLabel.Text = "Ready?";
    }
    private async void OnStartClicked(object sender, EventArgs e) 
    {
        if (!isRunning)
        {
            if (BreakTime != 0 && ExerciseTime != 0 && TotalSets != 0)
            {
                if (exerciseCounter == (TotalSets + 1))
                {
                    TitleLabel.Text = "Finished";
                    isRunning = true; 
                    return;
                }
                if (time.Minute == BreakTime && time.Second == 0 || time.Minute == ExerciseTime && time.Second == 0 || time.Minute == 0 && time.Second == 0)
                {
                    time = new TimeOnly();
                    UpdateTimeLabel();
                }
                isRunning = true;
                while (isRunning)
                {
                    if (counter % 2 == 0)
                    {
                        TitleLabel.Text = "Break";
                        time = time.Add(TimeSpan.FromSeconds(1));
                        UpdateTimeLabel();
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        if (time.Minute == BreakTime)
                        {
                            isRunning = false;
                            counter++;    
                        }
                    }
                    else
                    {
                        TitleLabel.Text = $"Exercise {exerciseCounter}/{TotalSets}";
                        time = time.Add(TimeSpan.FromSeconds(1));
                        UpdateTimeLabel();
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        if (time.Minute == ExerciseTime)
                        {
                            isRunning = false;
                            counter++;
                            exerciseCounter++;
                            if (exerciseCounter == (TotalSets + 1))
                            {
                                TitleLabel.Text = "Finished";
                            }
                        }
                    } 
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Fill out the information", "Ok");
            }
        }
    }
    private void OnStopClicked(object sender, EventArgs e)
    {
        isRunning = false;
    }
    private void OnResetClicked(object sender, EventArgs e) 
    {
        time = new TimeOnly();
        UpdateTimeLabel();
        counter = 1;
        exerciseCounter = 1;
        isRunning = false;
        TitleLabel.Text = "Ready?";
    }
    private void UpdateTimeLabel() 
    {
        TimerLabel.Text = string.Format("{0}:{1}", time.Minute.ToString().PadLeft(2, '0'), time.Second.ToString().PadLeft(2, '0'));
    }
    private async void UpdateFieldsOnStart()
    {
        try
        {
            breakList = await db.GetTimerBreakList();
            foreach (TimerBreak timerBreak in breakList)
            {
                BreakEntry.Placeholder = "Break Time: " + timerBreak.BreakTime;
                BreakTime = timerBreak.BreakTime;
            }
        }
        catch (Exception)
        {
            BreakEntry.Placeholder = "Break Time: ";
        }
        try
        {
            exerciseList = await db.GetTimerExerciseList();
            foreach (TimerExercise timerExercise in exerciseList)
            {
                ExerciseEntry.Placeholder = "Exercise Time: " + timerExercise.ExerciseTime;
                ExerciseTime = timerExercise.ExerciseTime;
            }
        }
        catch (Exception)
        {
            ExerciseEntry.Placeholder = "Exercise Time: ";
        }
        try
        {
            setsList = await db.GetTimerSetsList();
            foreach (TimerSets timerSets in setsList)
            {
                SetsEntry.Placeholder = "Total Sets: " + timerSets.TotalSets;
                TotalSets = timerSets.TotalSets;
            }
        }
        catch (Exception)
        {
            SetsEntry.Placeholder = "Total Sets: ";
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
                if (entry == BreakEntry)
                {
                    BreakEntry.Placeholder = "Break Time: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    TimerBreak timerBreak = new TimerBreak();
                    timerBreak.BreakTime = num;
                    BreakTime = num;
                    try
                    {
                        await db.SaveTimerBreak(timerBreak);
                    }
                    catch (Exception eee)
                    {
                        await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                    }
                }
                if (entry == ExerciseEntry)
                {
                    ExerciseEntry.Placeholder = "Exercise Time: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    TimerExercise timerExercise = new TimerExercise();
                    timerExercise.ExerciseTime = num;
                    ExerciseTime = num;
                    try
                    {
                        await db.SaveTimerExercise(timerExercise);
                    }
                    catch (Exception eee)
                    {
                        await Shell.Current.DisplayAlert("Error", eee.ToString(), "Ok");
                    }
                }
                if (entry == SetsEntry)
                {
                    SetsEntry.Placeholder = "Total Sets: " + enteredText;
                    entry.Text = "";
                    num = Convert.ToInt32(enteredText);
                    TimerSets timerSets = new TimerSets();
                    timerSets.TotalSets = num;
                    TotalSets = num;
                    try
                    {
                        await db.SaveTimerSets(timerSets);
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