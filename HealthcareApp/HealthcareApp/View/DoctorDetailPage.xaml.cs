using HealthcareApp.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
    public partial class DoctorDetailPage : ContentPage
    {
        bool buttonClicked = false;
        List<TimeSlotsModel> timeslotsList;
        public string DocId;
        public string _branchId;
        public string _clientId;
        public string selectedDate = DateTime.Now.Date.ToLongDateString();
        public string Currenttime = DateTime.Now.ToString("h:mm tt");
        public string selectedDay;
        public string selectedTimeSlot;
        public int i;
        public string selectedPaymentMode;
        public string updId;
        public string vedioConsultationStatus = "false";

        public DoctorDetailPage(string doctorName, string practicingFrom, string docId)
        {
            InitializeComponent();
            DoctorName.Text = doctorName;
            PracticingFrom.Text = practicingFrom;
            DocId = docId;
            datePicker.MinimumDate = DateTime.Now;
            picker.ItemsSource = paymentModeList;

            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
                //_userName = Application.Current.Properties["Username"].ToString();

            }
            DisplayTimeSloats();

        }

        public List<PaymentModeModel> paymentModeList = new List<PaymentModeModel>()
        {
            new PaymentModeModel(){PaymentMode="Net Banking"},
            new PaymentModeModel(){PaymentMode="UPI"}
        };

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        public async void DisplayTimeSloats()
        {
            selectedDay = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetAbbreviatedDayName(DateTime.Parse(selectedDate).DayOfWeek);
            var details = await App.HealthSoapService.DoctorTimeSlot1(DocId, selectedDay, _branchId, selectedDate);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                timeslotsList = JsonConvert.DeserializeObject<List<TimeSlotsModel>>(details);
            }
            //set values to buttons
            //  MyButtons.Children.Clear(); //just in case so you can call this code several times np..
            foreach (var item in timeslotsList)
            {
                int count = item.Slots.Count();
                if (count == 1)
                {
                    availability.Text = "Not Available";
                    return;
                }
                while (i < count)
                {
                    var btn = new Button()
                    {
                        Text = item.Slots[i], //Whatever prop you wonna put as title;
                        StyleId = item.Slots[i]   //use a property from event as id to be passed to handler

                    };
                    btn.Clicked += OnDynamicBtnClicked;
                    MyButtons.Children.Add(btn);
                    i++;

                }
            }
        }

        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            selectedTimeSlot = button.StyleId;

            DisplayAlert("Selected Time", selectedTimeSlot, "ok");
            // button.TextColor = Color.Blue;
            //button.BackgroundColor = Color.White;

        }



        private async void BookAppointmentButtonClicked(object sender, EventArgs e)
        {
            updId = PayDetailsEntry.Text;
            if (selectedTimeSlot == null)
            {
                DisplayAlert("Time Slot Not Selected", "Please Select Time Slot", "Ok");
                return;
            }
            if (!string.IsNullOrEmpty(selectedPaymentMode) && string.IsNullOrEmpty(updId))
            {
                DisplayAlert("", "Please Enter Payment Details", "Ok");
                return;
            }
            var details = await App.HealthSoapService.BookAppointment1(_clientId, selectedTimeSlot, _branchId, DocId, selectedDate, vedioConsultationStatus, updId, selectedPaymentMode);
            if (details != null)
            {
                //Deserialize object and save in res
                var msg = "";
                var res = JsonConvert.DeserializeObject<List<ChangePasswordModel>>(details);
                foreach (ChangePasswordModel changePasswordModel in res)
                {
                    msg = changePasswordModel.Message;
                }
                if (msg == "Success")
                {
                    await DisplayAlert("Appointment Booked", "Your appointment is booked successfully", "Ok");

                }
                else
                {
                    await DisplayAlert("Something went wrong!", "Please enter values correctly", "Ok");
                }
            }
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            selectedDate = e.NewDate.ToLongDateString();

            selectedDay = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetAbbreviatedDayName(DateTime.Parse(selectedDate).DayOfWeek);
            // selectedDay = System.DateTime.Now.ToString(selectedDate);
            DisplayTimeSloats();

        }

        private void SelectedPaymentOptionChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            var myThinging = (PaymentModeModel)selectedItem;
            selectedPaymentMode = myThinging.PaymentMode;
            if (selectedPaymentMode == "UPI")
            {
                PayDetailsEntry.IsVisible = true;
                PayDetailsEntry.Placeholder = "Please Enter UPI Id";
            }
            else
            {
                PayDetailsEntry.IsVisible = true;
                PayDetailsEntry.Placeholder = "Please Enter Net Banking Id";
            }
        }

        private void VedioCunsultationCheckChanged(object sender, TappedEventArgs e)
        {
            if (VedioCunsultationCheckbox.IsChecked)
            {
                vedioConsultationStatus = "true";
            }
            else if (!VedioCunsultationCheckbox.IsChecked)
            {
                vedioConsultationStatus = "false";
            }
        }
    }

}