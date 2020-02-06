using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NASA
{
    public partial class AstroPage : ContentPage
    {
        public List<Astronaut> AstroList { get; set; }

        private void Labels()
        {
            Search.Placeholder = Resource.Search;
            ButtonAdd.Text = Resource.ButtonAdd;
        }

        public AstroPage()
        {
            InitializeComponent();
            Labels();
        }

        protected override void OnAppearing()
        {
            AstroList = App.Database.GetItems().ToList<Astronaut>();
            listView.ItemsSource = AstroList;
            base.OnAppearing();
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new PersonPage(e.Item as Astronaut));
        }

        public async void OnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RedactorPage());
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            if(searchText != null)
                searchText = searchText.Trim('\n', ' ');
            if (searchText == "" || searchText == null)
                listView.ItemsSource = AstroList;
            else
                if (AstroList != null)
                    listView.ItemsSource = AstroList.Where(x => x.Name.ToLower().Contains(searchText));
        }
    }
}