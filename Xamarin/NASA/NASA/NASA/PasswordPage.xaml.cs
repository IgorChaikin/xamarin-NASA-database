using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NASA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordPage : ContentPage
	{
        private Astronaut person;
        private string mode;
        private string ErrorMes;
        private string WrongPassword;

        public PasswordPage(Astronaut p, string m)
        {
            ErrorMes = Resource.ErrorMes;
            WrongPassword = Resource.WrongPassword;
            person = p;
            mode = m;
            InitializeComponent();
        }

        public async void OnClickEnter(object sender, EventArgs e)
        {
            if (PasswordField.Text == person.Password)
            {
                switch(mode)
                {
                    case "C":
                    {
                        await Navigation.PushAsync(new RedactorPage(person));
                        NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                        var stack = navPage.Navigation.NavigationStack;
                        navPage.Navigation.RemovePage(stack[stack.Count - 2]);
                    }
                    break;
                    case "D":
                    {
                        App.Database.DeleteItem(person.Id);

                        await Navigation.PushAsync(new AstroPage());
                        
                        NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                        var stack = navPage.Navigation.NavigationStack;
                        while (stack.Count > 2)
                        {
                            navPage = (NavigationPage)App.Current.MainPage;
                            stack = navPage.Navigation.NavigationStack;
                            navPage.Navigation.RemovePage(stack[stack.Count - 2]);
                        }
                    }
                    break;
                    default:break;
                }
            }
            else
            {
                await Navigation.PopAsync();
                await DisplayAlert(ErrorMes, WrongPassword, "OK");
            }
        }
    }
}