using System;

using Xamarin.Forms;

namespace HIIT
{
    public class TimeStepper : StackLayout
    {
        private const int MAX = 1800;

        private Button IncrementButton;
        private Button DecrementButton;
        private Label CurrentTimeLabel;

        public static readonly BindableProperty SecondsProperty = BindableProperty.Create(nameof(Seconds), typeof(int), typeof(TimeStepper), 30);
        public int Seconds
        {
            get { return (int) GetValue(SecondsProperty); }
            set
            {
                SetValue(SecondsProperty, value);
                CurrentTimeLabel.Text = new TimeSpan(0, 0, value).ToString(@"mm\:ss");
            }
        }

        private void Increment(object sender, EventArgs e)
        {
            if(Seconds + 30 <= MAX)
                Seconds += 30;
            else
                Seconds = 0;
        }

        private void Decrement(object sender, EventArgs e)
        {
            if(Seconds - 30 >= 0)
                Seconds -= 30;
            else
                Seconds = 1800;
        }

        public TimeStepper()
        {
            Padding = 0;
            BackgroundColor = (Color) Application.Current.Resources["FG"];
            Orientation = StackOrientation.Horizontal;

            IncrementButton = new Button
            {
                Text = "+",
                WidthRequest = 55,
                HeightRequest = 55,
                CornerRadius = 0,
                FontSize = 30,
                BorderWidth = 2,
                BorderColor = (Color) Application.Current.Resources["FG"],
                TextColor = (Color) Application.Current.Resources["FG"],
                BackgroundColor = (Color) Application.Current.Resources["DarkBG"],
            };
            IncrementButton.Clicked += Increment;


            DecrementButton = new Button
            {
                Text = "-",
                WidthRequest = 55,
                HeightRequest = 55,
                CornerRadius = 0,
                FontSize = 30,
                BorderWidth = 2,
                BorderColor = (Color) Application.Current.Resources["FG"],
                TextColor = (Color) Application.Current.Resources["FG"],
                BackgroundColor = (Color) Application.Current.Resources["DarkBG"],
            };
            DecrementButton.Clicked += Decrement;


            CurrentTimeLabel = new Label
            {
                Text = new TimeSpan(0, 0, Seconds).ToString(@"mm\:ss"),
                VerticalOptions = LayoutOptions.Center,
                TextColor = (Color) Application.Current.Resources["DarkBG"],
                HeightRequest = 50,
                WidthRequest = 75,
                FontSize = 30,
                Margin = 0,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Children.Add(DecrementButton);
            Children.Add(CurrentTimeLabel);
            Children.Add(IncrementButton);
        }
    }
}