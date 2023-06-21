using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Button gymButton = new Button
            {
                Text = "Gym Stats"
            };
            gymButton.Clicked += NavigateToPage1;

            Button bmiButton = new Button
            {
                Text = "BMI Calculator"
            };
            bmiButton.Clicked += NavigateToPage2;

            Button pTrainerButton = new Button
            {
                Text = "Personal Trainer",
                BackgroundColor = Color.FromHex("#1184FF"),
                TextColor = Color.White

                
            };
            pTrainerButton.Clicked += NavigateToPTPage;

            Button exercisesButton = new Button
            {
                Text = "Exercise Center",
                BackgroundColor = Color.ForestGreen,
                TextColor = Color.White
            };
            exercisesButton.Clicked += NavigateToExPage;

            Button calButton = new Button
            {
                Text = "Calories"
            };
            calButton.Clicked += NavigateToCalPage;

            Button recButton = new Button
            {
                Text = "Recipes"
            };
            recButton.Clicked += NavigateToRecPage;

            var myBoxView = new BoxView
            {
                HeightRequest = 40
            };

            var myImage = new Image
            {
                Source = "hearth.png",
                WidthRequest = 300,
                HeightRequest = 300, 
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Children = { myImage, pTrainerButton, exercisesButton ,calButton, recButton, gymButton, bmiButton, myBoxView }
            };
        }

        void NavigateToPage1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }

        void NavigateToPage2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page2());
        }

        void NavigateToPTPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page3());
        }

        void NavigateToCalPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalPage());
        }

        void NavigateToRecPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecPage());
        }

        void NavigateToExPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExPage());
        }
    }
}
