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
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }
        private async void Login_Button_Pressed(object sender, EventArgs e)
        {

            //get username & password from entry
            var _username = Username.Text;
            var _password = Password.Text;

            //  Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Please Wait...");

            //Get details from service 
            var details = await App.HealthSoapService.Login1(_username, _password);
            if ((details != null) && (details.Length > 289))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<Login>>(details);

                foreach (Login login in res)
                {
                    Application.Current.Properties["Username"] = login.Username;
                    Application.Current.Properties["ClientId"] = login.ClientId;
                    Application.Current.Properties["BranchId"] = login.BranchId;
                    Application.Current.Properties["PatientName"] = login.PatientName;
                    Application.Current.Properties["Branch_EmailId"] = login.Branch_EmailId;
                }



                //Navigate to next page if username and password match 
                await Navigation.PushAsync(new HomePage());

            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid username or password", "Ok");
                Username.Text = string.Empty;
                Password.Text = string.Empty;
            }
        }

        private void SignUpTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void ForgotPasswordTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}