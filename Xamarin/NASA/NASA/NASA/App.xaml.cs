using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml;
using System.Reflection;
using System.Resources;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NASA
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "astronauts.db";
        public static DataBaseRepository database;

        public static DataBaseRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataBaseRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
