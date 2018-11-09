using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static HIIT.HTimer;

namespace HIIT
{
    public partial class TimerPage : ContentPage
    {
        private HTimer Schedule;
        private IEnumerator<(Cycle Type, int Sec)> cycles;
        private bool CountingDown = false;
        private bool Stopped = false;
        private int Seconds = 0;

        public TimerPage()
        {
            InitializeComponent();
        }

        public TimerPage(int WarmUp, int Work, int Rest, int CoolDown, int Rounds)
        {
            InitializeComponent();
            Schedule = new HTimer(WarmUp, Work, Rest, CoolDown, Rounds);
            cycles = Schedule.Run().GetEnumerator();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if(!CountingDown)
                    return true;
                Time.Text = new TimeSpan(0, 0, Seconds++).ToString(@"hh\:mm\:ss");
                ProgressBar.Opacity = 1;
                if(cycles.MoveNext())
                {
                    Card.BackgroundColor = (Color) Application.Current.Resources[cycles.Current.Type.ToString()];

                    switch(cycles.Current.Type)
                    {
                        case Cycle.WarmUp:
                            CurrentCycle.Text = "Warm Up";
                            break;
                        case Cycle.CoolDown:
                            CurrentCycle.Text = "Cool Down";
                            break;
                        default:
                            CurrentCycle.Text = cycles.Current.Type.ToString();
                            break;
                    }

                    TimeLeft.Text = new TimeSpan(0, 0, cycles.Current.Sec + 1).ToString(@"mm\:ss");

                    double calculatedProgress = (double) cycles.Current.Sec / Schedule.Lengths[cycles.Current.Type];

                    if(calculatedProgress > ProgressBar.Progress)
                        ProgressBar.Progress = 1.0;

                    ProgressBar.ProgressTo(calculatedProgress, 1000, Easing.Linear);

                }
                else // End of last timer
                {
                    TimeLeft.Text = "00:00";
                    CurrentCycle.Text = "Finished";
                    ProgressBar.Opacity = 0;
                    ProgressBar.Progress = 0;
                    Card.BackgroundColor = (Color) Application.Current.Resources["DarkBG"];
                    PauseResume.IsEnabled = false;
                    PauseResume.BackgroundColor = (Color) Application.Current.Resources["BG"];
                }
                return !Stopped;
            });
        }

        private void PauseResume_Clicked(object sender, EventArgs e)
        {
            CountingDown = !CountingDown;

            PauseResume.Text = CountingDown ? "Pause" : "Resume";
            PauseResume.BackgroundColor = (Color) Application.Current.Resources["DarkBG"];
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            Stopped = true;
            Navigation.PopModalAsync();
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            Seconds = 0;
            CountingDown = false;
            cycles = Schedule.Run().GetEnumerator();

            Time.Text = "00:00:00";
            TimeLeft.Text = "00:00";
            CurrentCycle.Text = "Press Start";
            ProgressBar.Opacity = 0;
            Card.BackgroundColor = (Color) Application.Current.Resources["DarkBG"];
            PauseResume.Text = "Start";
            PauseResume.IsEnabled = true;
            PauseResume.BackgroundColor = (Color) Application.Current.Resources["DarkBG"];
        }

    }
}
