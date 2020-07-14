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
    public partial class ProfilePage : ContentPage
    {
        public string _clientId;
        public string _branchId;

        public string profileImage { get; private set; }
        public ProfilePage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();

            }
            getPatientImage();
            getSetProfileDetails();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public async void getPatientImage()
        {
            //fetch and set userImage
            var details1 = await App.HealthSoapService.GetPatientImage1(_clientId, _branchId);
            if ((details1 != null) && (details1.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<DoctorImageModel>>(details1);
                foreach (DoctorImageModel doctorImageModel in res)
                {
                    profileImage = doctorImageModel.Image;
                }
                byte[] Base64Stream = Convert.FromBase64String(profileImage);
                userImage.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }
        }
        public async void getSetProfileDetails()
        {
            int clientId = Convert.ToInt32(_clientId);
            int branchId = Convert.ToInt32(_branchId);
            var details = await App.HealthSoapService.GetProfileDetails(clientId, branchId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<ProfileModel>>(details);

                //set value to each variable
                foreach (ProfileModel profileModel in res)
                {
                    Name.Text = profileModel.PatientName;
                    BOD.Text = profileModel.DOB.Remove(10);
                    EmailId.Text = profileModel.Email;
                    MobileNo.Text = profileModel.MobileNo;
                    Address.Text = profileModel.Address;
                    UserName.Text = profileModel.PatientName;
                }

            }
        }

        private void LogoutTextTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            int clientId = Convert.ToInt32(_clientId);
            int branchId = Convert.ToInt32(_branchId);
            var updatedAddress = Address.Text;
            var details = await App.HealthSoapService.UpdateProfile(clientId, branchId, updatedAddress);
            if (details != null)
            {
                //Deserialize object and save in res
                var msg = "";
                var res = JsonConvert.DeserializeObject<List<ChangePasswordModel>>(details);
                foreach (ChangePasswordModel changePasswordModel in res)
                {
                    msg = changePasswordModel.Message;
                }
                if (msg == "Updated")
                {
                    DisplayAlert("Address Updated", "Address Updated Successfully", "Ok");
                }
                else
                {
                    DisplayAlert("Something went wrong!", "Please Enter Address Correctly", "Ok");
                }
            }
        }
    }
}