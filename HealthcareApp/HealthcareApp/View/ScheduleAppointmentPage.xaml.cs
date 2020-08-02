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

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleAppointmentPage : ContentPage
    {

        // List<ScheduleAppointmentModel> res;
        public string _clientId;
        public string _branchId;
        public string SelectedId;
        string error_msg;
        //  public string _specialistId="1";
        public ScheduleAppointmentPage()
        {
            try
            {
                InitializeComponent();
                
                displaySpecialization();
            }
            catch (Exception ex)
            {
                error_msg = ex.Message;
            }
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("ClientId"))
                {
                    _clientId = Application.Current.Properties["ClientId"].ToString();
                    _branchId = Application.Current.Properties["BranchId"].ToString();
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                error_msg = ex.Message;
            }
        }

        public async void displaySpecialization()
        {
            // displayDoctorList();
            //get specializtion list
            int branchId = Convert.ToInt32(_branchId);
            var Specializationdetails = await App.HealthSoapService.FillSpecialisation(branchId);
            if ((Specializationdetails != null) && (Specializationdetails.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<SpecializationModel>>(Specializationdetails);
                picker.ItemsSource = res;
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //  ListView btn = (ListView)sender;
            ////  int clubid = (int)btn.CommandParameter;

            //  await Navigation.PushAsync(new DoctorDetailPage());
            if(e.SelectedItem!=null)
            {
                var details = e.SelectedItem as ScheduleAppointmentModel;

                listView.SelectedItem = null;
                await Navigation.PushAsync(new DoctorDetailPage(details.DoctorName, details.PractisingFrom, details.DoctorId));

            }


        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            var myThinging = (SpecializationModel)selectedItem;
            SelectedId = myThinging.ID;
            displayDoctorList();
        }

        public async void displayDoctorList()
        {
            try
            { 
            //display Appointment list based on specialistId
            var details = await App.HealthSoapService.FillDoctorList1(_branchId, SelectedId);
            if ((details != null) && (details.Length > 0))
            {
               // listView.BeginRefresh();
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<ScheduleAppointmentModel>>(details);
                listView.ItemsSource = res;
               //  listView.EndRefresh();
            }
            }catch(Exception e)
            {
                var msg = e.Message;
            }
        }

        //private void DropDownImageClicked(object sender, EventArgs e)
        //{
            
        //}
    }
}