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
	public partial class MasterDetailPageNav : MasterDetailPage
	{
		public MasterDetailPageNav()
		{
			InitializeComponent();
            Detail = new NavigationPage(new ScheduleAppointmentPage());
            IsPresented = false;
        }
        private void SheduleAppointmentButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ScheduleAppointmentPage());
            IsPresented = false;
        }

        private void ViewReportButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ViewReportPage());
            IsPresented = false;
        }

        private void ViewPrescriptionButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ViewPrescriptionPage());
            IsPresented = false;
        }

        private void HealthTipsButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HealthTipsPage());
            IsPresented = false;
        }
    }
}


