using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace NASA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{

        public SettingsPage ()
		{
            int a=App.Database.GetItems().ToList<Astronaut>().Count;

            List<Entry> entries = new List<Entry>
            {
                new Entry(a)
                {
                    Color=SKColor.Parse("#00FF00"),
                    ValueLabel=a.ToString()
                },

                new Entry(20-a)
                {
                    Color=SKColor.Parse("#FF0000"),
                    ValueLabel=(20-a).ToString()
                }
            };

            InitializeComponent ();

            this.BackgroundImage = "Space.png";
            chart.Chart = new DonutChart() { Entries = entries, LabelTextSize = 30, BackgroundColor = SKColor.Parse("#000000") };
            Device.StartTimer(TimeSpan.FromSeconds(0.25), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            img.Rotation+=5;
            if (img.Rotation > 360)
                img.Rotation -= 360;
            return true;
        }

        void OnInfoClick(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://en.wikipedia.org/wiki/SpaceX_Mars_transportation_infrastructure"));
        }
    }
}