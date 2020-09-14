using HealthcareApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Drawing;
using System.Reflection;



namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPrescriptionPage : ContentPage
    {
        List<SendResult> res;
        public string _clientId;
        public string _branchId;
        string base64Image1;
        public string appointmentStatus;

        public ViewPrescriptionPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();

            }

            DisplayPrescriptionList();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }
        public async void DisplayPrescriptionList()
        {
            try
            {
                int branchid = Convert.ToInt32(_branchId);
                //fetch and attache prescriptiondetails from web service
                var details = await App.HealthSoapService.GetPrescriptionDetails(_clientId, branchid);
                if (details.Length > 0)
                {
                    res = JsonConvert.DeserializeObject<List<SendResult>>(details);
                    foreach(var item in res)
                    {
                        appointmentStatus = item.Appointmentstatus;
                        if (appointmentStatus == "Complete")
                        {
                            item.v = "True";
                            // item.bgcolor = "True";

                        }
                        else
                        {
                            item.v = "False";
                            // item.bgcolor = "False";
                        }
                    }
                    listView.ItemsSource = res;

                }
            }
            catch (Exception ex)
            {

                string error = ex.Message;
            }

        }

        private async void DownloadPrescriptionClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.CommandParameter as SendResult;
            int appointmentId = item.AppointmentId;
            int branchid = Convert.ToInt32(_branchId);


            //Get Imagefrom WebApi
            var details = await App.HealthSoapService.PrintPrescription(_clientId, branchid, appointmentId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<ReportPDFModel>>(details);
                foreach (ReportPDFModel reportPDFModel in res)
                {
                    base64Image1 = reportPDFModel.Image;
                }

            }
            if (base64Image1 == null)
            {
                DisplayAlert("Not Uploaded!", "Prescription not Uploaded,Please try after some time", "Ok");
                return;
            }
            try
            {
                //convert received image from webservice into bytes
                byte[] imgBytes = Convert.FromBase64String(base64Image1);

                ////convert image to memorystream
                MemoryStream mStrm = new MemoryStream();
                mStrm.Write(imgBytes, 0, imgBytes.Length);


                //Load the existing PDF document.
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(mStrm, true);

                //Get the existing PDF page.
                PdfLoadedPage loadedPage = loadedDocument.Pages[0] as PdfLoadedPage;

                //Save the stream as a file in the device and invoke it for viewing
                Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Prescription.pdf", "application/pdf", mStrm);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public class SendResult
        {
            public string Date { get; set; }
            public string DoctorName { get; set; }
            public string PractisingFrom { get; set; }
            public string DoctorId { get; set; }
            public int BranchId { get; set; }
            public int AppointmentId { get; set; }
            public string DocImage { get; set; }
            public string Appointmentstatus { get; set; }
            public string v { get; set; }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  

            listView.BeginRefresh();
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listView.ItemsSource = res;
            }

            else
            {
                listView.ItemsSource = res.Where(i => i.DoctorName.ToUpper().Contains(e.NewTextValue.ToUpper()));

            }
            listView.EndRefresh();

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var index = (listView.ItemsSource as List<SendResult>).IndexOf(e.SelectedItem as SendResult);
        }

    }
}