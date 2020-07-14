using HealthcareApp.Model;
using HealthcareApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Drawing;
using System.IO;
using System.Reflection;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewReportPage : ContentPage
    {
        public string _clientId;
        public string _branchId;
        string base64Image1;
        string imageType;
        public ViewReportPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();

            }

            DisplayReportList();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }
        public async void DisplayReportList()
        {
            var details = await App.HealthSoapService.GetReport1(_clientId, _branchId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<ReportModel>>(details);
                listView.ItemsSource = res;

            }
        }
        private async void Download_Report(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.CommandParameter as ReportModel;
            string uploadId = item.UploadId;


            //Get Imagefrom WebApi
            var details = await App.HealthSoapService.GetReportinPdf1(uploadId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<DownloadReportModule>>(details);
                foreach (DownloadReportModule downloadReportModule in res)
                {
                    base64Image1 = downloadReportModule.Image;
                    imageType = downloadReportModule.type;
                }

            }
            if (base64Image1 == null)
            {
                DisplayAlert("Not Uploaded!", "Report not Uploaded,Please try after some time", "Ok");
                return;
            }
            try
            {
                if (imageType == "png")
                {
                    //syncfusion pdf download code
                    // Get the input PDF document as stream.
                    Stream documentStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("HealthcareApp.Assets.blank.pdf");

                    //Load the existing PDF document.
                    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(documentStream);

                    //Get the existing PDF page.
                    PdfLoadedPage loadedPage = loadedDocument.Pages[0] as PdfLoadedPage;

                    //convert received image from webservice into bytes
                    byte[] imgBytes = Convert.FromBase64String(base64Image1);

                    ////convert image to memorystream
                    MemoryStream mStrm = new MemoryStream();
                    mStrm.Write(imgBytes, 0, imgBytes.Length);

                    //Create a new PdfBitmap instance.
                    PdfBitmap image = new PdfBitmap(mStrm);

                    //Draw the image
                    loadedPage.Graphics.DrawImage(image, new RectangleF(new PointF(40, 150), new SizeF(515, 215)));

                    MemoryStream stream = new MemoryStream();

                    //Save the document.
                    loadedDocument.Save(stream);

                    //Close the document.
                    loadedDocument.Close(true);

                    stream.Position = 0;

                    //Save the stream as a file in the device and invoke it for viewing
                    Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Report.pdf", "application/pdf", stream);
                }
                else if (imageType == "pdf")
                {
                    //convert received image from webservice into bytes
                    byte[] imgBytes = Convert.FromBase64String(base64Image1);

                    ////convert image to memorystream
                    MemoryStream mStrm = new MemoryStream();
                    mStrm.Write(imgBytes, 0, imgBytes.Length);

                    //Load the existing PDF document.
                    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(mStrm);

                    //Get the existing PDF page.
                    PdfLoadedPage loadedPage = loadedDocument.Pages[0] as PdfLoadedPage;

                    //Save the stream as a file in the device and invoke it for viewing
                    Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Report.pdf", "application/pdf", mStrm);
                }
                else
                {
                    DisplayAlert("can't download", "Report is in Unsupportad format!", "Ok");
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
