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
	public partial class NavPages : ContentPage
	{
		public NavPages ()
		{
			InitializeComponent ();
		}
        private async void ScheduleAppointment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleAppointmentPage());
        }
        private async void ViewPrescription_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewPrescriptionPage());
        }

        private async void viewReport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewReportPage());
        }

        private async void HealthTips_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HealthTipsPage());
        }

        private async void Doctordetails_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new DoctorDetailPage());
        }

        
    }
}