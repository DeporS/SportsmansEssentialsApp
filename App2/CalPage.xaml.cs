using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class CalPage : ContentPage
    {
        const string MaxCalKey = "MaxCal";
        const string CurrentCalKey = "CurrentCal";
        const string MaxProKey = "MaxPro";
        const string MaxProHighKey = "MaxProHigh";
        const string CurrentProKey = "CurrentPro";
        const string MaxFatKey = "MaxFat";
        const string MaxFatHighKey = "MaxFatHigh";
        const string CurrentFatKey = "CurrentFat";
        const string MaxCarbKey = "MaxCarb";
        const string MaxCarbHighKey = "MaxCarbHigh";
        const string CurrentCarbKey = "CurrentCarb";
        const string LastResetKey = "LastReset";

        float currentCal = 0;
        float maxCal = 2500;
        float currentPro = 0;
        float maxPro = 150;
        float maxProHigh = 0;
        float currentFat = 0;
        float maxFat = 118;
        float maxFatHigh = 0;
        float currentCarb = 0;
        float maxCarb = 500;
        float maxCarbHigh = 0;

        

        public CalPage()
        {
            InitializeComponent();

            // Wczytaj wartości z pamięci urządzenia, jeśli są dostępne
            if (Application.Current.Properties.ContainsKey(MaxCalKey))
            {
                maxCal = (float)Application.Current.Properties[MaxCalKey];
            }
            if (Application.Current.Properties.ContainsKey(CurrentCalKey))
            {
                currentCal = (float)Application.Current.Properties[CurrentCalKey];
            }

            if (Application.Current.Properties.ContainsKey(MaxProKey))
            {
                maxPro = (float)Application.Current.Properties[MaxProKey];
            }
            if (Application.Current.Properties.ContainsKey(MaxProHighKey))
            {
                maxProHigh = (float)Application.Current.Properties[MaxProHighKey];
            }
            if (Application.Current.Properties.ContainsKey(CurrentProKey))
            {
                currentPro = (float)Application.Current.Properties[CurrentProKey];
            }

            if (Application.Current.Properties.ContainsKey(MaxFatKey))
            {
                maxFat = (float)Application.Current.Properties[MaxFatKey];
            }
            if (Application.Current.Properties.ContainsKey(MaxFatHighKey))
            {
                maxFatHigh = (float)Application.Current.Properties[MaxFatHighKey];
            }
            if (Application.Current.Properties.ContainsKey(CurrentFatKey))
            {
                currentFat = (float)Application.Current.Properties[CurrentFatKey];
            }

            if (Application.Current.Properties.ContainsKey(MaxCarbKey))
            {
                maxCarb = (float)Application.Current.Properties[MaxCarbKey];
            }
            if (Application.Current.Properties.ContainsKey(MaxCarbHighKey))
            {
                maxCarbHigh = (float)Application.Current.Properties[MaxCarbHighKey];
            }
            if (Application.Current.Properties.ContainsKey(CurrentCarbKey))
            {
                currentCarb = (float)Application.Current.Properties[CurrentCarbKey];
            }

            SaveCalories();
            LoadBar();

            // Tworzenie Timera do resetowania kalorii o północy
            try
            {
                var now = DateTime.Now;
                var resetTime = new DateTime(now.Year, now.Month, now.Day, 16, 0, 0);
                var timeToReset = resetTime > now ? (int)(resetTime - now).TotalMilliseconds : (int)(resetTime.AddDays(1) - now).TotalMilliseconds;
                var timer = new System.Timers.Timer(timeToReset);
                timer.Elapsed += (sender, args) =>
                {
                    ResetCalories();
                    var nextResetTime = resetTime.AddDays(1);
                    var timeToNextReset = (int)(nextResetTime - DateTime.Now).TotalMilliseconds;
                    timer.Interval = timeToNextReset;
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                scannedText.Text = ex.Message;
            }

            // Sprawdź, czy minął dzień od ostatniego resetu
            if (Application.Current.Properties.ContainsKey(LastResetKey))
            {
                
                DateTime lastReset = (DateTime)Application.Current.Properties[LastResetKey];
                //scannedText.Text = $"Last reset: {lastReset}\n";
                //scannedText.Text += $"Today's date: {DateTime.Today}";
                //Console.WriteLine($"Last reset: {lastReset}");
                //Console.WriteLine($"Today's date: {DateTime.Today}");
                if (lastReset.Date < DateTime.Today)
                {
                    
                    ResetCalories();
                    Application.Current.Properties[LastResetKey] = DateTime.Today;
                }
            }
            else
            {
                Application.Current.Properties[LastResetKey] = DateTime.Today;
            }

        }

        // Metoda do zapisywania wartości currentCal i maxCal po każdej zmianie
        void SaveCalories()
        {
            currentCal = (float)Math.Round(currentCal);
            currentPro = (float)Math.Round(currentPro);
            currentFat = (float)Math.Round(currentFat);
            currentCarb = (float)Math.Round(currentCarb);
            Application.Current.Properties[MaxCalKey] = maxCal;
            Application.Current.Properties[CurrentCalKey] = currentCal;
            Application.Current.Properties[MaxProKey] = maxPro;
            Application.Current.Properties[MaxProHighKey] = maxProHigh;
            Application.Current.Properties[CurrentProKey] = currentPro;
            Application.Current.Properties[MaxFatKey] = maxFat;
            Application.Current.Properties[MaxFatHighKey] = maxFatHigh;
            Application.Current.Properties[CurrentFatKey] = currentFat;
            Application.Current.Properties[MaxCarbKey] = maxCarb;
            Application.Current.Properties[MaxCarbHighKey] = maxCarbHigh;
            Application.Current.Properties[CurrentCarbKey] = currentCarb;
        }

        // Metoda do resetowania wartości currentCal o północy
        void ResetCalories()
        {
            currentCal = 0;
            currentPro = 0;
            currentFat = 0;
            currentCarb = 0;
            SaveCalories();

            LoadBar();
            // Zapisz datę ostatniego resetu
            Application.Current.Properties[LastResetKey] = DateTime.Today;
        }

        // Zaladowanie paska progressu
        public async void LoadBar()
        {
            calProgress.Text = currentCal + "/" + maxCal + " (" + (maxCal - currentCal) + " left)" ;
            proProgress.Text = currentPro + "/" + maxPro + "-" + maxProHigh + "g";
            fatProgress.Text = currentFat + "/" + maxFat + "-" + maxFatHigh + "g";
            carbProgress.Text = currentCarb + "/" + maxCarb + "-" + maxCarbHigh + "g";

            // Pasek kalorii
            if (currentCal > maxCal)
            {
                caloriesProgressBar.BackgroundColor = Color.DarkGray;
                caloriesProgressBar.ProgressColor = Color.OrangeRed;
                
                await caloriesProgressBar.ProgressTo((currentCal - maxCal) / maxCal, 500, Easing.Linear);
            }
            else
            {
                caloriesProgressBar.BackgroundColor = Color.LightGray;
                caloriesProgressBar.ProgressColor = Color.LightGreen;
                await caloriesProgressBar.ProgressTo(currentCal / maxCal, 500, Easing.Linear);
            }

            // Pasek Bialka
            if (currentPro > maxPro)
            {
                if (currentPro< maxProHigh)
                {
                    proteinProgressBar.BackgroundColor = Color.Gray;
                    proteinProgressBar.ProgressColor = Color.Orange;

                    await proteinProgressBar.ProgressTo((currentPro - maxPro) / (maxProHigh - maxPro), 500, Easing.Linear);
                }
                else
                {
                    proteinProgressBar.BackgroundColor = Color.DarkGray;
                    proteinProgressBar.ProgressColor = Color.OrangeRed;

                    await proteinProgressBar.ProgressTo((currentPro - maxProHigh) / maxProHigh, 500, Easing.Linear);
                }
            }
            else
            {
                proteinProgressBar.BackgroundColor = Color.LightGray;
                proteinProgressBar.ProgressColor = Color.Beige;
                await proteinProgressBar.ProgressTo(currentPro / maxPro, 500, Easing.Linear);
            }

            // Pasek Tluszczu
            if (currentFat > maxFat)
            {
                if (currentFat < maxFatHigh)
                {
                    fatProgressBar.BackgroundColor = Color.Gray;
                    fatProgressBar.ProgressColor = Color.Orange;

                    await fatProgressBar.ProgressTo((currentFat - maxFat) / (maxFatHigh - maxFat), 500, Easing.Linear);
                }
                else
                {
                    fatProgressBar.BackgroundColor = Color.DarkGray;
                    fatProgressBar.ProgressColor = Color.OrangeRed;

                    await fatProgressBar.ProgressTo((currentFat - maxFatHigh) / maxFatHigh, 500, Easing.Linear);
                }
            }
            else
            {
                fatProgressBar.BackgroundColor = Color.LightGray;
                fatProgressBar.ProgressColor = Color.Yellow;
                await fatProgressBar.ProgressTo(currentFat / maxFat, 500, Easing.Linear);
            }

            // Pasek Wegli
            if (currentCarb > maxCarb)
            {
                if(currentCarb < maxCarbHigh)
                {
                    carbProgressBar.BackgroundColor = Color.Gray;
                    carbProgressBar.ProgressColor = Color.Orange;

                    await carbProgressBar.ProgressTo((currentCarb - maxCarb) / (maxCarbHigh - maxCarb), 500, Easing.Linear);
                }
                else
                {
                    carbProgressBar.BackgroundColor = Color.DarkGray;
                    carbProgressBar.ProgressColor = Color.OrangeRed;

                    await carbProgressBar.ProgressTo((currentCarb - maxCarbHigh) / maxCarbHigh, 500, Easing.Linear);
                }
                
            }
            else
            {
                carbProgressBar.BackgroundColor = Color.LightGray;
                carbProgressBar.ProgressColor = Color.Black;
                await carbProgressBar.ProgressTo(currentCarb / maxCarb, 500, Easing.Linear);
            }
        }

        public async void AddItem(object sender, EventArgs e)
        {
            string calString = await DisplayPromptAsync("Fill the gap", "Calories:", "OK", keyboard: Keyboard.Numeric);
            if (calString == null || calString == "")
            {
                AddItem(sender, e);
                return;
            }
            string proString = await DisplayPromptAsync("Fill the gap", "Protein:", "OK", keyboard: Keyboard.Numeric);
            if (proString == null || proString == "")
            {
                AddItem(sender, e);
                return;
            }
            string fatString = await DisplayPromptAsync("Fill the gap", "Fat:", "OK", keyboard: Keyboard.Numeric);
            if (fatString == null || fatString == "")
            {
                AddItem(sender, e);
                return;
            }
            string carbString = await DisplayPromptAsync("Fill the gap", "Carbohydrates:", "OK", keyboard: Keyboard.Numeric);
            if (fatString == null || fatString == "")
            {
                AddItem(sender, e);
                return;
            }
            currentCal += float.Parse(calString);
            currentPro += float.Parse(proString);
            currentFat += float.Parse(fatString);
            currentCarb += float.Parse(carbString);
            SaveCalories();

            LoadBar();
        }

        public async void SetCal(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalPageSet());
            Navigation.RemovePage(this);
        }

        public void ChangeCal()
        {
            var change = (SetClass.weight - SetClass.goal) * 7700 / (SetClass.months * 30);
            maxCal = (float)Math.Round((88.362 + (13.397 * SetClass.weight) + (4.799 * SetClass.height) - (5.667 * SetClass.age)) * SetClass.trains - change);
            maxPro = (float)Math.Round(1.2 * SetClass.weight);
            maxProHigh = (float)Math.Round(1.7 * SetClass.weight);
            maxFat = (float)Math.Round(maxCal * 0.20 / 9);
            maxFatHigh = (float)Math.Round(maxCal * 0.30 / 9);
            maxCarb = (float)Math.Round((maxCal - maxProHigh * 4 - (maxCal * 0.30)) / 4);
            maxCarbHigh = (float)Math.Round((maxCal - maxPro * 4 - (maxCal * 0.20)) / 4);


            SaveCalories();

            LoadBar();
            
        }

        public async Task<JObject> GetProductByBarcode(string barcode)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"https://world.openfoodfacts.org/api/v0/product/{barcode}.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return JObject.Parse(json);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex) { 
                    await DisplayAlert("Error", ex.Message, "OK");
                    return null;
                }
                
            }
        }

        bool isScanned;
        public void ScanItem(object sender, EventArgs e)
        {
            fruitImage.IsVisible = false;
            scanner.IsVisible = true;
            addBut.IsVisible = false;
            scanBut.IsVisible = false;
            resetBut.IsVisible = false;
            setBut.IsVisible = false;
            cancelBut.IsVisible = true;
            isScanned = false;
        }

        public void TurnOffScanner()
        {
            scanner.IsVisible = false;
            fruitImage.IsVisible = true;
            addBut.IsVisible = true;
            scanBut.IsVisible = true;
            resetBut.IsVisible = true;
            setBut.IsVisible = true;
            cancelBut.IsVisible = false;
        }

        public void CancelScaner(object sender, EventArgs e)
        {
            TurnOffScanner();
        }

        void ScanFun(ZXing.Result result)
        {
            
            // Sprawdzenie, czy skanowanie zostało już wykonane
            if (isScanned)
                return;

            string barcode = "xd";

            Device.BeginInvokeOnMainThread(() =>
            {
                TurnOffScanner();
                barcode = result.Text;
                ScanResult(barcode);

                // Ustawienie flagi skanowania na true
                isScanned = true;
            });
        }

        public async void ScanResult(string barcode)
        {
            // Pobierz szczegóły produktu z Open Food Facts
            if (barcode != "xd")
            {
                try
                {
                    //calProgress.Text = barcode;
                    //string barcode = await DisplayPromptAsync("Add Item", "Barcode:", "OK", keyboard: Keyboard.Numeric); ;
                    var productDetails = await GetProductByBarcode(barcode);

                    if (productDetails != null)
                    {
                        // 1 - Wyświetl całą zawartość JSON-a w formie tekstu 2 - finalnie
                        var pick1 = 2;
                        if (pick1 == 1)
                        {
                            string jsonText = productDetails.ToString();
                            await DisplayAlert("JSON", jsonText, "OK");
                        }
                        else if (pick1 == 2)
                        {
                            var product = productDetails["product"];
                            var nutriments = productDetails["product"]["nutriments"];

                            var brandName = product["brands"].ToString();
                            var description = product["category_properties"]["ciqual_food_name:en"].ToString();
                            var carbohydrates = nutriments["carbohydrates_100g"].Value<decimal?>();
                            var energyKcal = nutriments["energy-kcal_100g"].Value<decimal?>();
                            var fat = nutriments["fat_100g"].Value<decimal?>();
                            var proteins = nutriments["proteins_100g"].Value<decimal?>();

                            var result = await DisplayPromptAsync(brandName, $"Per 100g\nEnergy (kcal): {energyKcal}\nProteins: {proteins}\nFat: {fat}\nCarbohydrates: {carbohydrates}", "Add", "Cancel", placeholder: "grams");

                            if (result != null)
                            {
                                var multiplier = float.Parse(result.ToString()) / 100;
                                currentCal += (float)energyKcal * multiplier;
                                currentCarb += (float)carbohydrates * multiplier;
                                currentFat += (float)fat * multiplier;
                                currentPro += (float)proteins * multiplier;

                                SaveCalories();
                                LoadBar();
                            }
                            
                        }

                    }
                    else
                    {
                        // Komunikat o błędzie
                        await DisplayAlert("Error", "Failed to get product details", "OK");
                    }


                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }


        public void Reset(object sender, EventArgs e)
        {
            ResetCalories();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SaveCalories();

            // Zapisz datę ostatniego resetu przy wyjściu z aplikacji
            Application.Current.Properties[LastResetKey] = DateTime.Today;
        }
    }


}