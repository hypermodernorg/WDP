using WordDivisionPuzzles.Services;
using WordDivisionPuzzles.Views;
using Xamarin.Forms;

namespace WordDivisionPuzzles
{
    public partial class App : Application
    {

        static WDPDB database;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        public static WDPDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new WDPDB();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
