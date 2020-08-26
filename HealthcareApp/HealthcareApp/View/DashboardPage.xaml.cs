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
	public partial class DashboardPage : ContentPage
	{
        string FromDate;
        string ToDate;
        string _clientId;
        string _branchId;
        string _opdId;

        public DashboardPage ()
		{
			InitializeComponent ();
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
                _opdId = Application.Current.Properties["OpdPatientId"].ToString();
            }
            LoadHealthGraph();
            LoadAppointmentHistoryGraph();
		}

        public async void LoadAppointmentHistoryGraph()
        {
            //Get Values from service
            var details = await App.HealthSoapService.GetSpecializationWiseCount(_opdId, _branchId, FromDate, ToDate);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<HealthGraphModel>>(details);
            }
        }

        public async void LoadHealthGraph()
        {
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