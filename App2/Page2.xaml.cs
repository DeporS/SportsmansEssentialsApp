using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {   
        public Page2()
        {
            InitializeComponent();
        }
        float bmi;
        private async void calButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kgEntry.Text) || string.IsNullOrEmpty(mEntry.Text) || string.IsNullOrEmpty(ageEntry.Text))
            {
                await DisplayAlert("Warning", "Please enter values in all fields", "OK");
            }
            else
            {
                bmi = float.Parse(kgEntry.Text) / (float)Math.Pow((float.Parse(mEntry.Text) / 100), 2);
                bmiOut.Text = "BMI = " + Math.Round(bmi, 1).ToString("F1");
                over20Box.IsVisible = true;
                if (int.Parse(ageEntry.Text) > 20)
                {
                    if (bmi < 15)
                    {
                        setBox.Margin = new Thickness(-300,-40,0,0);
                        bmiText.Text = "Severe thinness";
                        bmiText.TextColor = Color.Red;
                    }
                    else if(bmi < 16)
                    {
                        setBox.Margin = new Thickness(-285, -40, 0, 0);
                        bmiText.Text = "Severe thinness";
                        bmiText.TextColor = Color.Red;
                    }
                    else if(bmi < 17)
                    {
                        setBox.Margin = new Thickness(-240, -40, 0, 0);
                        bmiText.Text = "Moderate thinness";
                        bmiText.TextColor = Color.Orange;
                    }
                    else if (bmi < 18.5)
                    {
                        setBox.Margin = new Thickness(-195, -40, 0, 0);
                        bmiText.Text = "Mild thinness";
                        bmiText.TextColor = Color.Yellow;
                    }
                    else if (bmi < 25)
                    {
                        setBox.Margin = new Thickness(-105, -40, 0, 0);
                        bmiText.Text = "Normal";
                        bmiText.TextColor = Color.Green;
                    }
                    else if (bmi < 30)
                    {
                        setBox.Margin = new Thickness(15, -40, 0, 0);
                        bmiText.Text = "Overweight";
                        bmiText.TextColor = Color.Yellow;
                    }
                    else if (bmi < 35)
                    {
                        setBox.Margin = new Thickness(110, -40, 0, 0);
                        bmiText.Text = "Obese Class 1";
                        bmiText.TextColor = Color.Orange;
                    }
                    else if (bmi < 40)
                    {
                        setBox.Margin = new Thickness(200, -40, 0, 0);
                        bmiText.Text = "Obese Class 2";
                        bmiText.TextColor = Color.Red;
                    }
                    else if (bmi >= 40)
                    {
                        setBox.Margin = new Thickness(300, -40, 0, 0);
                        bmiText.Text = "Obese Class 3";
                        bmiText.TextColor = Color.Brown;
                    }
                }
                else
                {
                    bmiText.Text = "Age <20";
                }
                kgEntry.Text = "";
                mEntry.Text = "";
                ageEntry.Text = "";
                setBox.IsVisible = true;
            }
        }
    }
    
}