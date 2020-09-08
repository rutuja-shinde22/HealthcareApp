using HealthcareApp.Model;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
        byte[] Documentbyte;
        public string filenamewithextension;
      public  string[] Filetype;
       public string FileExtension;
        public string documentType;
        public string SelectedDoc;
        public string _clientId;
        public string _branchId;
        public string fileEx;
        public ImageSource SelectedImage { get; private set; }


        public UploadDocumentPage()
        {
            InitializeComponent();
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
            try
            {
               // check & ask permission
              
                int commandParameter = 11;
                Permission permission = (Permission)commandParameter;
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (permissionStatus != PermissionStatus.Granted)
                {
                    var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);

                }
                if (permissionStatus != PermissionStatus.Granted)
                {
                    return;

                }

                //Open Gallery & sect doc,convert into base64
                await CrossMedia.Current.Initialize();

                FileData filedata = await CrossFilePicker.Current.PickFile();
                Documentbyte = filedata.DataArray;
                if (string.IsNullOrEmpty(filedata.FileName) == false)
                {
                    string FileExtension = Path.GetExtension(filedata.FileName);
                    string FileNamewithoutextetion = Path.GetFileNameWithoutExtension(filedata.FileName);
                    filenamewithextension = Path.GetFileName(filedata.FileName);

                    uploadedDoc.Text = filenamewithextension;
                    Filetype = FileExtension.ToString().Split('.');
                    fileEx = Filetype[1];

                }
                else
                {

                    if (filedata.FilePath.Split('\\').Length > 0)
                    {

                        string Filenamefropath = filedata.FilePath.Split('/')[filedata.FilePath.Split('/').Length - 1];



                        string FileExtension = Path.GetExtension(Filenamefropath);
                        string FileNamewithoutextetion = Path.GetFileNameWithoutExtension(Filenamefropath);


                        uploadedDoc.Text = FileNamewithoutextetion;
                        Filetype = FileExtension.ToString().Split('.');


                    }

                }
                SelectedDoc = Convert.ToBase64String(Documentbyte);
            }catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private async void UploadButton_ButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string docName = documentName.Text;
                if ((string.IsNullOrEmpty(_clientId)) || (string.IsNullOrEmpty(documentType)) || (string.IsNullOrEmpty(docName)) || (string.IsNullOrEmpty(SelectedDoc)) || (string.IsNullOrEmpty(_branchId)))
                {
                    await DisplayAlert("", "Please Enter all values", "Ok");
                    return;
                }
                var details = await App.HealthSoapService.UploadDocument(_clientId, documentType, filenamewithextension, fileEx, SelectedDoc, _branchId);

                if (details == "\"Fail\"")
                {
                    await DisplayAlert("", "Document not Uploaded!", "Ok");
                    return;
                }
                else
                {
                    await DisplayAlert("", "Document Uploaded successfully", "Ok");
                    uploadedDoc.IsVisible = false;
                    return;
                }
                //else
                //{
                //    await DisplayAlert("", "Something went wrong", "Ok");
                //    return;
                //}
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
