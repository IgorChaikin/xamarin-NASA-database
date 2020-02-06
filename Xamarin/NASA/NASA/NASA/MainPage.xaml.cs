using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using System.Globalization;
using System.IO;
using Resx.Resources;
using Plugin.Multilingual;
using Newtonsoft.Json;

namespace NASA
{
    public partial class MainPage : ContentPage
    {
        private double L, T, R, B, d;

        private void Labels()
        {
            buttonBegin.Text= Resource.buttonBegin;
            buttonLanguage.Text = Resource.buttonLanguage;
            buttonInfo.Text = Resource.buttonInfo;
        }

        async void Exist(string s)
        {
            if (!await DependencyService.Get<IFileWorker>().ExistsAsync(s))
                await DependencyService.Get<IFileWorker>().SaveTextAsync(s, "ru");
        }

        public MainPage()
        {
            Exist("language.json");

            string l = DependencyService.Get<IFileWorker>().LoadTextAsync("language.json");
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(l);
            Resource.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            InitializeComponent();
            Labels();
            this.BackgroundImage = "Background.png";

            B = Logo.Margin.Bottom;
            T = Logo.Margin.Top;
            R = Logo.Margin.Right;
            L = Logo.Margin.Left;
            d = -3.3;
            Device.StartTimer(TimeSpan.FromSeconds(0.25), OnTimerTick);
        }

        async void BE_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AstroPage());
        }

        void BL_Click(object sender, EventArgs e)
        {
            string l = CrossMultilingual.Current.CurrentCultureInfo.ToString();
            if (l == "ru")
                l = "en";
            else
                l = "ru";

            DependencyService.Get<IFileWorker>().SaveTextAsync("language.json",l);

            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(l);
            Resource.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            Labels();
        }

        async void BI_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private bool OnTimerTick()
        {
            if(B<=-5||T <= -5)
                d *= -1;
            T -= d;
            B += d;
            Logo.Margin = new Thickness(L, T, R, B);
            return true;
        }
    }
}
