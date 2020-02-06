using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NASA
{
	public partial class PersonPage : ContentPage
	{
        public Astronaut Person { get; set; }

        private void Labels()
        {
            Nationality.Text += ":" + Person.Nationality;

            string G;
            if (Person.Gender == 0)
                G = Resource.M;
            else
                G = Resource.F;

            Gender.Text += ":" + G;
            BornDate.Text += ":" + Person.BirthYear;

            Height.Text += ":" + Person.Height.ToString();
            Weight.Text+= ":" + Person.Weight.ToString();
            Eyes.Text+= ":" + Person.Eyes.ToString();
            FlyTime.Text+= ":" + Person.FlyTime.ToString();

            if (Person.Exams == 0)
                G = Resource.Y;
            else
                G = Resource.N;

            Exams.Text+= ":" + G;
            WorkYears.Text += ":" + Person.WorkYears.ToString();
        }

        public PersonPage (Astronaut p)
		{
            Person = p;

            this.BindingContext = this;
            InitializeComponent();

            Labels();

            Photo.Source = p.ImagePath;

            if (Person.Schools != null)
            {
                string[] list = Person.Schools.Split('$');
                for (int i = 0; i < list.Length; i++)
                    SchoolsPicker.Items.Add(list[i]);
            }
        }

        void OnSchoolSelect(object sender, EventArgs e)
        {
            if (SchoolsPicker.SelectedIndex > -1)
            {
                SchoolsPicker.SelectedIndex = -1;
                SchoolsPicker.IsTabStop = false;
            }
        }

        public async void OnClickChange(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordPage(Person, "C"));
        }

        public async void OnClickDelete(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordPage(Person, "D"));
        }
    }
}