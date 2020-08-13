using HealthcareApp.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class UploadDocumentPage : ContentPage
	{
        public string documentType;
        public string SelectedDoc;
        public string _clientId;
        public string _branchId;
        public ImageSource SelectedImage { get; private set; }


        public UploadDocumentPage ()
		{
			InitializeComponent ();
            picker.ItemsSource = paymentModeList;

            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
            }

        }

        public List<DocumentTypeModel> paymentModeList = new List<DocumentTypeModel>()
        {
            new DocumentTypeModel(){DocumentType="X-RAY REPORT"},
            new DocumentTypeModel(){DocumentType="Consultation paper"},
            new DocumentTypeModel(){DocumentType="Other Lab Reports "},
            new DocumentTypeModel(){DocumentType="Documents"}
        };


        public PhotoSize PhotoSize { get; private set; }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            var myThinging = (DocumentTypeModel)selectedItem;
            documentType = myThinging.DocumentType;
        }

        private async void SelectDocument_ButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not Supported", "Your device does not support this functionality", "Ok");
                return;
            }
            //added using Plugin.Media.Abstractions
            //if you want to take a picture use StoreCameraMediaOption insted of pickmediaoption

            var mediaOptions = new PickMediaOptions();
            {
                PhotoSize = PhotoSize.Small;
            }
            //if you want to take a picture use TakePhotoAsync insted of PickPhototoAsync

            var selectdImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectdImageFile == null)
            {
                await DisplayAlert("Error", "could not get the image,Please try again", "Ok");
                return;
            }
            else
            {
                await DisplayAlert("", "Image selected successfully", "Ok");
            }
            SelectedImage = ImageSource.FromStream(() => selectdImageFile.GetStream());
            string path = selectdImageFile.Path.ToString();

          //  Convert Selected ImageSource to Byte[]

            StreamImageSource streamImageSource = (StreamImageSource)SelectedImage;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;


            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                b = ms.ToArray();
            }
            //Convert Byte[] to base64 string
            SelectedDoc = Convert.ToBase64String(b);
        }

        private async void UploadButton_ButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string docName = documentName.Text;
                if((string.IsNullOrEmpty(_clientId))|| (string.IsNullOrEmpty(documentType))|| (string.IsNullOrEmpty(docName)) || (string.IsNullOrEmpty(SelectedDoc))||(string.IsNullOrEmpty(_branchId)))
                {
                    await DisplayAlert("", "Please Enter all values", "Ok");
                    return;
                }
                var details = await App.HealthSoapService.UploadDocument(_clientId, documentType, docName, "png", SelectedDoc, _branchId);

                if (details == "\"Fail\"")
                {
                    await DisplayAlert("", "Document not Uploaded!", "Ok");
                    return;
                }
                else 
                {
                    await DisplayAlert("", "Document Uploaded successfully", "Ok");
                    return;
                }
                //else
                //{
                //    await DisplayAlert("", "Something went wrong", "Ok");
                //    return;
                //}
            }catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
    }
