namespace FitnessHelper
{
    public partial class App : Application
    {
        public App()
        {
            //second entry point
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
