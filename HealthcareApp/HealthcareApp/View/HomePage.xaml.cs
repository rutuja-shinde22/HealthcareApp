//using Android.Graphics;
//using Android.Util;
using HealthcareApp.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public string _clientId;
        public string _branchId;
        public string _patientName;

        public PhotoSize PhotoSize { get; private set; }
        public string profileImage { get; private set; }
        public ImageSource SelectedImage { get; private set; }



        public HomePage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
                _patientName = Application.Current.Properties["PatientName"].ToString();

            }
            patientName.Text = _patientName;
            DisplayUserImage();

        }
        protected override async void OnAppearing()
        {


            base.OnAppearing();

        }
        public async void DisplayUserImage()
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

        private void ScheduleAppointmentTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScheduleAppointmentPage());
        }
        private void MyAppointmentTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyAppointmentPage());
        }
        private void ViewPrescriptionTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewPrescriptionPage());
        }

        private void DownloadReportTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewReportPage());
        }
        private void Dashboard_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DashboardPage());
        }
        private void HealthTipsTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HealthTipsPage());
        }
        private void UploadDocumentTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UploadDocumentPage());
        }

        //private async void SelectPictureFromGallery(object sender, EventArgs e)
        //{
        //    //int commandParameter = 11;
        //    //Permission permission = (Permission)commandParameter;
        //    //var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        //    //if (permissionStatus != PermissionStatus.Granted)
        //    //{
        //    //    var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
        //    //    var userResponce = response[permission];
        //    //    Debug.WriteLine($"Permission {permission} {userResponce}");
        //    //}
        //    //else
        //    //{
        //    //    Debug.WriteLine($"Permission {permission} {permissionStatus}");
        //    //}

        //    int commandParameter = 11;
        //    Permission permission = (Permission)commandParameter;
        //    var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        //    if (permissionStatus != PermissionStatus.Granted)
        //    {
        //        var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);

        //    }
        //    if (permissionStatus != PermissionStatus.Granted)
        //    {
        //        return;

        //    }

        //    try
        //    {
        //        await CrossMedia.Current.Initialize();

        //        if (!CrossMedia.Current.IsPickPhotoSupported)
        //        {
        //            await DisplayAlert("Not Supported", "Your device does not support this functionality", "Ok");
        //            return;
        //        }
        //        //added using Plugin.Media.Abstractions
        //        //if you want to take a picture use StoreCameraMediaOption insted of pickmediaoption

        //        var mediaOptions = new PickMediaOptions();
        //        {
        //            PhotoSize = PhotoSize.Small;

        //        }
        //        //if you want to take a picture use TakePhotoAsync insted of PickPhototoAsync

        //        var selectdImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

        //        if (userImage == null)
        //        {
        //            await DisplayAlert("Error", "could not get the image,Please try again", "Ok");
        //            return;
        //        }
        //        SelectedImage = ImageSource.FromStream(() => selectdImageFile.GetStream());
        //        string path = selectdImageFile.Path.ToString();
        //        //Convert Selected ImageSource to Byte[]
        //        StreamImageSource streamImageSource = (StreamImageSource)SelectedImage;
        //        System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
        //        Task<Stream> task = streamImageSource.Stream(cancellationToken);
        //        Stream stream = task.Result;


        //        byte[] b;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            stream.CopyTo(ms);
        //            b = ms.ToArray();
        //        }
        //        BitmapFactory.Options options = new BitmapFactory.Options();
        //        options.InPreferredConfig = Bitmap.Config.Argb8888;

        //        ////try to convert in bitmap for compress image but get exception 
        //        //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
        //        Bitmap bitmap = BitmapFactory.DecodeFile(path, options);
        //        MemoryStream stream1 = new MemoryStream();
        //        bitmap.Compress(Bitmap.CompressFormat.Png, 10, stream1);
        //        byte[] ba = stream1.ToArray();
        //        string str = Base64.EncodeToString(ba, Base64Flags.NoWrap);



        //        //Convert Byte[] to base64 string
        //        //string str = Convert.ToBase64String(b);

        //        int branchId = Convert.ToInt32(_branchId);

        //        //Update Iamge in database
        //        var details = await App.HealthSoapService.UpdatePhoto(_clientId, branchId, str);
        //        if ((details != null) && (details.Length > 0))
        //        {
        //            //Deserialize object and save in res
        //            var msg = "";
        //            var res = JsonConvert.DeserializeObject<List<ChangePasswordModel>>(details);
        //            foreach (ChangePasswordModel changePasswordModel in res)
        //            {
        //                msg = changePasswordModel.Message;
        //            }
        //            if (msg == "Update")
        //            {
        //                DisplayAlert("Profile Picture Updated", "Your Profile Picture Updated Successfully", "Ok");
        //            }
        //            else
        //            {
        //                DisplayAlert("Something went wrong!", "Please try again", "Ok");
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

        private void ToolbarItemLogout_Clicked(object sender, EventArgs e)
        {



            //Application.Current.Properties.Remove("Username");
            //Application.Current.Properties.Remove("ClientId");
            //Application.Current.Properties.Remove("BranchId");
            //Application.Current.Properties.Remove("PatientName");
            //Application.Current.Properties.Remove("Branch_EmailId");
            
            Navigation.PushAsync(new LoginPage());
        }

        private void ToolbarItemProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void ToolbarItemChangePassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }

      
    }
}