using System;

using Xamarin.Forms;

namespace HIIT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            WarmUpTime.Seconds = 120;
            WorkTime.Seconds = 30;
            RestTime.Seconds = 90;
            CoolDownTime.Seconds = 120;
            NumberRounds.Value = 5;
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(
                new TimerPage(
                    WarmUpTime.Seconds,
                    WorkTime.Seconds,
                    RestTime.Seconds,
                    CoolDownTime.Seconds,
                    NumberRounds.Value
                ));
        }
    }
}