using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public class SetClass
    {
        public static float weight { get; set; }
        public static float height { get; set; }
        public static float age { get; set; }
        public static float goal { get; set; }
        public static float months { get; set; }
        public static double trains { get; set; }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalPageSet : ContentPage
    {


        
        public CalPageSet()
        {
            InitializeComponent();
        }

        public async void SetButton(object sender, EventArgs e)
        {
            if(weightEntry.Text != null && heightEntry.Text != null && ageEntry.Text != null && goalEntry.Text != null && monthsEntry.Text != null && weightEntry.Text != "" && heightEntry.Text != "" && ageEntry.Text != "" && goalEntry.Text != "" && monthsEntry.Text != "")
            {
                if(act1.IsChecked != false || act2.IsChecked != false || act3.IsChecked != false || act4.IsChecked != false || act5.IsChecked != false)
                {
                    
                    if(act1.IsChecked == true)
                    {
                        SetClass.trains = 1.2;
                    }
                    else if(act2.IsChecked == true)
                    {
                        SetClass.trains = 1.375;
                    }
                    else if(act3.IsChecked == true)
                    {
                        SetClass.trains = 1.55;
                    }
                    else if(act4.IsChecked == true)
                    {
                        SetClass.trains = 1.725;
                    }
                    else
                    {
                        SetClass.trains = 1.9;
                    }
                    SetClass.weight = float.Parse(weightEntry.Text);
                    SetClass.height = float.Parse(heightEntry.Text);
                    SetClass.age = float.Parse(ageEntry.Text);
                    SetClass.goal = float.Parse(goalEntry.Text);
                    SetClass.months = float.Parse(monthsEntry.Text);

                    CalPage calPage = new CalPage();
                    
                    calPage.ChangeCal();

                    await DisplayAlert("Success", "Changes saved successfully", "OK");
                    Navigation.RemovePage(this);

                    return;
                }
            }
            await DisplayAlert("Error", "Please fill everything!", "OK");
            return;
        }

        public void Activity1(object sender, SelectedItemChangedEventArgs e)
        {
            act2.IsChecked = false;
            act3.IsChecked = false;
            act4.IsChecked = false;
            act5.IsChecked = false;
            
        }
        public void Activity2(object sender, SelectedItemChangedEventArgs e)
        {
            act1.IsChecked = false;
            act3.IsChecked = false;
            act4.IsChecked = false;
            act5.IsChecked = false;
           
        }
        public void Activity3(object sender, SelectedItemChangedEventArgs e)
        {
            act2.IsChecked = false;
            act1.IsChecked = false;
            act4.IsChecked = false;
            act5.IsChecked = false;
            
        }
        public void Activity4(object sender, SelectedItemChangedEventArgs e)
        {
            act2.IsChecked = false;
            act3.IsChecked = false;
            act1.IsChecked = false;
            act5.IsChecked = false;
            
        }
        public void Activity5(object sender, SelectedItemChangedEventArgs e)
        {
            act2.IsChecked = false;
            act3.IsChecked = false;
            act4.IsChecked = false;
            act1.IsChecked = false;
            
        }
    }
}