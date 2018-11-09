using System;

using Xamarin.Forms;

namespace HIIT
{
    public class NumberStepper : StackLayout
    {
        private const int MAX = 99;


        private Button IncrementButton;
        private Button DecrementButton;
        private Label ValueLabel;

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(NumberStepper), 1);
        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                ValueLabel.Text = value.ToString("00");
            }
        }

        private void Increment(object sender, EventArgs e)
        {
            if(Value + 1 < MAX)
                Value++;
            else
                Value = 0;

            ValueLabel.Text = Value.ToString("00");
        }

        private void Decrement(object sender, EventArgs e)
        {
            if(Value - 1 >= 0)
                Value--;
            else
                Value = 99;

            ValueLabel.Text = Value.ToString("00");
        }

        public NumberStepper()
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


            ValueLabel = new Label
            {
                Text = Value.ToString("00"),
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
            Children.Add(ValueLabel);
            Children.Add(IncrementButton);
        }
    }
}