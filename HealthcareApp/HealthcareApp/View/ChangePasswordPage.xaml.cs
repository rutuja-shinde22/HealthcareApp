using HealthcareApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public string profileImage { get; private set; }
        public string _clientId;
        public string _branchId;
        public string _userName;

        public ChangePasswordPage()
        {
            InitializeComponent();
            TipLable.IsVisible = false;
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
                _userName = Application.Current.Properties["Username"].ToString();

            }
            getPatientImage();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }
        public async void getPatientImage()
        {
            var details = await App.HealthSoapService.GetPatientImage1(_clientId, _branchId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<DoctorImageModel>>(details);
                foreach (DoctorImageModel doctorImageModel in res)
                {
                    profileImage = doctorImageModel.Image;
                }
                byte[] Base64Stream = Convert.FromBase64String(profileImage);
                userImage.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }
        }


        private async void ChangePassword_BurronClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldPassword.Text) || string.IsNullOrEmpty(newPassword.Text) || string.IsNullOrEmpty(reEnteredPassword.Text))
            {
                await DisplayAlert("", "Entries cannot be Blank,Please fill all details", "Ok");
                return;
            }
            if ((newPassword.TextColor == Color.Red) || (reEnteredPassword.TextColor == Color.Red))
            {
                await DisplayAlert("", "Please follow password formats correctly! ", "Ok");
                return;
            }
            if (newPassword.Text == reEnteredPassword.Text)
            {

                var _password = newPassword.Text;
                var _OldPassword = oldPassword.Text;

                var details = await App.HealthSoapService.ChangePassword(_userName, _password, _clientId, _OldPassword);
                if (details != null)
                {
                    //Deserialize object and save in res
                    var msg = "";
                    var res = JsonConvert.DeserializeObject<List<ChangePasswordModel>>(details);
                    foreach (ChangePasswordModel changePasswordModel in res)
                    {
                        msg = changePasswordModel.Message;
                    }
                    if (msg == "Update")
                    {
                        await DisplayAlert("", "Password updated successfully", "Ok");
                        oldPassword.Text = string.Empty;
                        newPassword.Text = string.Empty;
                        reEnteredPassword.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        await DisplayAlert("", "Old password is invalid, Please enter correct old password", "Ok");
                        oldPassword.Text = string.Empty;
                        return;
                    }
                }

            }
            else
            {
                //pass and re-enterd-pass not match
                DisplayAlert("", "Please re-enter password correctly", "Ok");
                reEnteredPassword.Text = string.Empty;
                return;
            }

        }

        private void NewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TipLable.IsVisible = true;
        }

        private void OldPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TipLable.IsVisible = false;
        }

        private void ReEnteredPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TipLable.IsVisible = false;
        }
    }
}