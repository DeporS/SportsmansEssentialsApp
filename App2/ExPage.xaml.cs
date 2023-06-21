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
    public partial class ExPage : ContentPage
    {
        

        public ExPage()
        {
            InitializeComponent();
        }

        async void OnHomeDrop(object sender, DropEventArgs e)
        {
            await DisplayAlert("Correct", "You want to train at home!", "OK");
        }

        async void OnGymDrop(object sender, DropEventArgs e)
        {
            await DisplayAlert("Correct", "You want to train at the gym!", "OK");
        }
    }
}