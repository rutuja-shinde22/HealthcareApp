using HealthcareApp.Model;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        string FromDate;
        string ToDate;
        string _clientId;
        string _branchId;
        string _opdId;
      


        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="January",
                ValueLabel = "200"
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "March",
                ValueLabel = "400"
                
            },
            new Entry(-100)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
                ValueLabel = "100"
            },
            };


        public DashboardPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
                _opdId = Application.Current.Properties["OpdPatientId"].ToString();
            }
            var lastmonth = DateTime.Today.AddMonths(-1);
            startdatePicker.MinimumDate = lastmonth;
            //var entries1 = new List<Entry>();

            //entries1.Add(new Entry(200) { Color = SKColor.Parse("#FF1943"), Label = "January", ValueLabel = "200" });


            LoadAppointmentHistoryGraph();
            LoadHealthGraph();

        }

        public async void LoadAppointmentHistoryGraph()
        {
           
            //Get Values from service
            var details = await App.HealthSoapService.GetSpecializationWiseCount("OPD186", "1", "10-Aug-2020", "30-Aug-2020");
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<AppointmentHistoryGraph>>(details);
                var entries1 = new List<Entry>();
                foreach (var g in res)
                {
                    entries1.Add(new Entry(Convert.ToInt32(g.Count))
                    { Label = g.specialization, ValueLabel = g.Count ,Color= SKColor.Parse("#FF1943")});
                }

                appoinmentHistoryChart.Chart = new PointChart() { Entries = entries1 };
            }
           
        }

        public async void LoadHealthGraph()
        {
            healthChart.Chart = new LineChart() { Entries = entries };
            //Get Values from service
            var details = await App.HealthSoapService.GetVitalDetails(_opdId, FromDate, ToDate);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<AppointmentHistoryGraph>>(details);
            }
        }

        private void DatePicker_FromDateSelected(object sender, DateChangedEventArgs e)
        {
            FromDate = e.NewDate.ToString("dd-MMM-yyyy");
        }

        private void DatePicker_ToDateSelected(object sender, DateChangedEventArgs e)
        {
            ToDate = e.NewDate.ToString("dd-MMM-yyyy");
        }
    }
}