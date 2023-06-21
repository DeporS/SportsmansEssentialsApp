using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using OpenAI_API;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        
        OpenAIAPI api = new OpenAIAPI("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");

        string hisInput;
        string hisOutput;

        public Page3()
        {
            InitializeComponent();

        }
        

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(userInput.Text))
            {
                But1.IsEnabled = false;
                centralText.Text += "U: " + userInput.Text;
                centralText.Text += "\n\n";
                var botInput = userInput.Text;
                userInput.Text = "";
                centralText.Text += "T: ";

                await Task.Run(async () =>
                {
                    var chat = api.Chat.CreateConversation();

                    // give instruction as System
                    chat.AppendSystemMessage("You are a personal trainer who answers questions related to the gym, exercise, and diet. Please provide brief, concise, and to-the-point answers. If a simple 'yes' or 'no' is sufficient, you can use that. Respond in the same language as the question. If someone asks about a topic unrelated to the gym, exercise, or diet, simply reply with 'I cannot help you with that.'");

                    // give a few examples as user and assistant
                    chat.AppendUserInput("How much water should i drink a day?");
                    chat.AppendExampleChatbotOutput("An adult should drink from 1.5 to 2 liters of water - depending on gender, ambient temperature and physical activity. During intense exercise and hot weather, even up to 4-5 liters a day.");
                    chat.AppendUserInput("Is this an animal? House");
                    chat.AppendExampleChatbotOutput("I can't help you with that");

                    // 1 message history
                    if (hisInput != null)
                    {
                        chat.AppendUserInput(hisInput);
                        chat.AppendExampleChatbotOutput(hisOutput);
                    }

                    // Asking a question
                    chat.AppendUserInput(botInput);
                    



                    // and get the response
                    try
                    {
                        hisOutput = null;
                        await chat.StreamResponseFromChatbotAsync(res =>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                centralText.Text += res;
                                hisOutput += res;
                            });
                        });
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            centralText.Text = ex.Message;
                        });
                    }
                });
                centralText.Text += "\n\n";
                But1.IsEnabled = true;
                hisInput = botInput;

            }
            else
            {
                await DisplayAlert("Warning", "Please enter a message before clicking the button.", "OK");
            }
            
        }

        void NavigateToHisPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HisPage());
        }


    }
}