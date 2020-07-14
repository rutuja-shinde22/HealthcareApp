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
	public partial class HealthTipsPage : ContentPage
	{
		public HealthTipsPage ()
		{
			InitializeComponent ();
		}
        public string _branchId;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("BranchId"))
            {
                _branchId = Application.Current.Properties["BranchId"].ToString();
            }

            var details = await App.HealthSoapService.HealthTips1(_branchId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<HealthTipsModel>>(details);
                listView.ItemsSource = res;
            }
        }

    }
}