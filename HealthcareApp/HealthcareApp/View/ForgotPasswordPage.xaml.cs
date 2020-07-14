using HealthcareApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

       private void BackToLoginTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Send_ButtonClicked(object sender, EventArgs e)
        {
            
            var emailid = EmailEntry.Text;
            var details = await App.HealthSoapService.ForGotPassword(emailid);
            if (details != null)
            {
                //Deserialize object and save in res
                var msg = "";
                var res = JsonConvert.DeserializeObject<List<ChangePasswordModel>>(details);
                foreach (ChangePasswordModel changePasswordModel in res)
                {
                    msg = changePasswordModel.Message;
                }
                if (msg == "Fail")
                {
                    DisplayAlert("Invalid Mail Id", "Please Enter Registered Mail Id", "Ok");
                    EmailEntry.Text = string.Empty;
                }
                else
                {
                    DisplayAlert("Success", "Username and Password sent to your Mail", "Ok");
                    EmailEntry.Text = string.Empty;
                }
            }
            

        }
    }
}