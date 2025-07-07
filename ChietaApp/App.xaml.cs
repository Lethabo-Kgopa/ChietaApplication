using ChietaApp.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;

namespace ChietaApp
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;
        private Page _mainPage;

        public App(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            LoadDataAndPrepareMainPage();
        }

        private async void LoadDataAndPrepareMainPage()
        {
            await _databaseService.InitializeAsync();

            _mainPage = new MainPage(); // Or new AppShell() if you're using Shell
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Ensure _mainPage is ready; fallback if somehow not initialized
            return new Window(_mainPage ?? new MainPage()) { Title = "ChietaApp" };
        }
    }
}
