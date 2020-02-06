using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace NASA
{
    public partial class RedactorPage : ContentPage
	{
        public List<string> Answers { get; set; }
        public List<string> Genders { get; set; }
        public Astronaut Redacted { get; set; }
        public string PhotoPath { get; set; }

        private void Labels()
        {
            Answers = new List<string>() {Resource.Y, Resource.N };
            Genders = new List<string>() { Resource.M, Resource.F};
        }

        public RedactorPage()
        {
            Labels();
            this.BindingContext = this;
            PhotoPath = "NoPhoto.png";
            InitializeComponent();
            Labels();
            //Photo.Source = PhotoPath;
        }

        public RedactorPage(Astronaut A)
        {
            Redacted = A;
            Labels();

            this.BindingContext = this;
            PhotoPath = A.ImagePath;
            InitializeComponent();

            if (A != null)
            {
                //Photo.Source = PhotoPath;
                NameField.Text = A.Name;
                NationalityField.Text = A.Nationality;
                GenderPicker.SelectedIndex = A.Gender;
                BirthDatePicker.Date = DateTime.ParseExact(A.BirthYear, "dd.MM.yyyy", null);
                HeightField.Text = A.Height.ToString();
                WeightField.Text = A.Weight.ToString();
                EyesField.Text = A.Eyes.ToString();
                FlyTimeField.Text = A.FlyTime.ToString();
                ExamsPicker.SelectedIndex = A.Exams;
                DoctorField.Text = A.DoctorVerdict;

                if (A.Schools != null)
                {
                    string[] list = A.Schools.Split('$');
                    for (int i = 0; i < list.Length; i++)
                        SchoolsPicker.Items.Add(list[i]);
                }

                WorkYearsField.Text = A.WorkYears.ToString();
                PasswordField.Text = A.Password;
            }
        }

        public async void OnPBClick(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                PhotoPath= (photo.Path);
                Photo.Source = ImageSource.FromFile(PhotoPath);
            }
        }

        void OnGenderSelect(object sender, EventArgs e)
        {
            GenderPicker.IsTabStop=false;
        }

        void OnBirthDateSelect(object sender, EventArgs e)
        {
            BirthDatePicker.IsTabStop = false;
        }

        void OnAnswerSelect(object sender, EventArgs e)
        {
            ExamsPicker.IsTabStop = false;
        }

        void OnSchoolSelect(object sender, EventArgs e)
        {
            if (SchoolsPicker.SelectedIndex > -1)
            {
                SchoolsPicker.SelectedIndex = -1;
                SchoolsPicker.IsTabStop = false;
            }
        }

        void OnSchoolAdd(object sender, EventArgs e)
        {
            SchoolsPicker.Items.Add(SchoolField.Text);
            SchoolField.Text = "";
        }

        void OnSchoolClear(object sender, EventArgs e)
        {
            SchoolsPicker.SelectedIndex = -1;
            SchoolsPicker.Items.Clear();
        }

        public void OnHold(object sender, EventArgs e)
        {
           PasswordField.IsPassword = false;
           RepeatPassword.IsPassword = false;
        }
        public void OnRelease(object sender, EventArgs e)
        {
            PasswordField.IsPassword = true;
            RepeatPassword.IsPassword = true;
        }

        private bool ValidNum(params string[] s)
        {
            int n;
            for(int i=0; i<s.Length;i++)
            {
                if (!int.TryParse(s[i], out n))
                    return false;
                else
                    if(int.Parse(s[i])<0)
                        return false;
            }
            return true;
        }

        public bool Validation()
        {
            string ErrorMes = Resource.ErrorMes;
            string BadFormat = Resource.BadFormat;
            string NoRepeat = Resource.NoRepeat;
            string BadPassword = Resource.BadPassword;
            string SimplePassword = "Пароль ненадёжен(менее 8 символов).";

            int n;
            if (!ValidNum(HeightField.Text, WeightField.Text, FlyTimeField.Text, WorkYearsField.Text)||
                !int.TryParse(EyesField.Text, out n) ||
                GenderPicker.SelectedIndex == -1 || 
                ExamsPicker.SelectedIndex == -1)
            {
                DisplayAlert(ErrorMes, BadFormat, "OK");
                return false;
            }

            if (PasswordField.Text != RepeatPassword.Text)
            {
                DisplayAlert(ErrorMes, NoRepeat, "OK");
                return false;
            }

            if (PasswordField.Text == null)
            {
                DisplayAlert(ErrorMes, BadPassword, "OK");
                return false;
            }

            string symb = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            if (PasswordField.Text.IndexOfAny(symb.ToCharArray()) == -1)
            {
                DisplayAlert(ErrorMes, BadPassword, "OK");
                return false;
            }

            if (PasswordField.Text.Length < 8)
            {
                DisplayAlert(ErrorMes, SimplePassword, "OK");
                return false;
            }

            return true;
        }

        public async void OnClickEnter(object sender, EventArgs e)
        {
            if (Validation())
            {
                Astronaut A = new Astronaut()
                {
                    ImagePath = PhotoPath,
                    Name = NameField.Text,
                    Nationality = NationalityField.Text,
                    Gender = GenderPicker.SelectedIndex,
                    BirthYear = BirthDatePicker.Date.ToString("dd.MM.yyyy"),
                    Height = int.Parse(HeightField.Text),
                    Weight = int.Parse(WeightField.Text),
                    Eyes = int.Parse(EyesField.Text),
                    FlyTime = int.Parse(FlyTimeField.Text),
                    Exams = ExamsPicker.SelectedIndex,
                    DoctorVerdict = DoctorField.Text,
                    WorkYears = int.Parse(WorkYearsField.Text),
                    Password = PasswordField.Text
                };

                if (Redacted != null)
                    A.Id = Redacted.Id;

                if (SchoolsPicker.Items != null)
                    A.Schools = String.Join("$", SchoolsPicker.Items.ToArray());

                App.Database.SaveItem(A);

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
        }
    }
}