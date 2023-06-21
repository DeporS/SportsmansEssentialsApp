using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private string filePath;

        public Page1()
        {
            InitializeComponent();

            // Inicjalizacja ścieżki do pliku
            filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "data.txt");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Zapisanie wartości wprowadzonych w Entry
            var BP_PR_value = BP_PR.Text;
            var BC_W_value = BC_W.Text;
            var BC_R_value = BC_R.Text;
            var PU_R_value = PU_R.Text;
            var T_D_value = T_D.Text;
            var T_T_value = T_T.Text;

            // Zapisanie wartości do pliku
            string[] lines = {
                $"BP_PR={BP_PR_value}",
                $"BC_W={BC_W_value}",
                $"BC_R={BC_R_value}",
                $"PU_R={PU_R_value}",
                $"T_D={T_D_value}",
                $"T_T={T_T_value}"
            };

            File.WriteAllLines(filePath, lines);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Wczytanie wartości z pliku
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string key = parts[0];
                        string value = parts[1];

                        switch (key)
                        {
                            case "BP_PR":
                                BP_PR.Text = value;
                                break;
                            case "BC_W":
                                BC_W.Text = value;
                                break;
                            case "BC_R":
                                BC_R.Text = value;
                                break;
                            case "PU_R":
                                PU_R.Text = value;
                                break;
                            case "T_D":
                                T_D.Text = value;
                                break;
                            case "T_T":
                                T_T.Text = value;
                                break;
                        }
                    }
                }
            }
        }
    }
}
