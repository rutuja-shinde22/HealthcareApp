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
            try {
                //get selected day in required format
            selectedDay = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetAbbreviatedDayName(DateTime.Parse(selectedDate).DayOfWeek);

                //get Timeslots array
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
                    availability.Text = "Not Available! Please select another date";
                        TimelotsFrame.IsVisible = false;
                    return;
                }
                    i = 0;
                    while (i < count)
                    {
                        var btn = new Button()
                        {
                            Text = item.Slots[i], //Whatever prop you wonna put as title;
                            StyleId = item.Slots[i]   //use a property from event as id to be passed to handler

                        };
                        //check selected day and display time slots depends on seleceted day
                        DateTime TodayDate = Convert.ToDateTime(selectedDate);
                        DateTime CurrentDate = DateTime.Now.Date;
                        int result = DateTime.Compare(TodayDate, CurrentDate);

                        //selected day in future display all time slots
                        if (result == 1)
                        {
                            TimelotsFrame.IsVisible = true;
                            availability.Text = "Available";
                            btn.Clicked += OnDynamicBtnClicked;
                            MyButtons.Children.Add(btn);
                            i++;
                        }
                        //selected day is today display valid time slots
                        else if (result == 0)
                        {
                            DateTime _currentTime = Convert.ToDateTime(Currenttime);
                            string timeslotvalue = btn.Text;
                            DateTime _time = Convert.ToDateTime(timeslotvalue);
                            int res = DateTime.Compare(_time, _currentTime);
                            //time in past
                            if (res < 0)
                            {
                                TimelotsFrame.IsVisible = false;
                                btn.IsVisible = false;
                                availability.Text = "Not Available! Please select another date";
                               

                            }
                            //current time
                            else if (res == 0)
                            {
                                TimelotsFrame.IsVisible = true;
                                btn.IsVisible = true;
                                availability.Text = "Available";
                            }
                            //future time
                            else
                            {
                               TimelotsFrame.IsVisible = true;
                                btn.IsVisible = true;
                                availability.Text = "Available";
                            }
                            btn.Clicked += OnDynamicBtnClicked;
                            MyButtons.Children.Add(btn);
                            i++;
                        }
                    }
            }
            }catch(Exception ex)
            {
                string m=ex.Message;
            }
        }

        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            selectedTimeSlot = button.StyleId;
            DisplayAlert("Selected Time", selectedTimeSlot, "ok");
            button.TextColor = Color.Blue;

        }



        private async void BookAppointmentButtonClicked(object sender, EventArgs e)
        {
            updId = PayDetailsEntry.Text;

            //check timesloat selection
            if (selectedTimeSlot == null)
            {
                DisplayAlert("Time Not Selected", "Please Select Time Slot", "Ok");
                return;
            }

            //ckeck payment mode
            if (!string.IsNullOrEmpty(selectedPaymentMode) && string.IsNullOrEmpty(updId))
            {
                DisplayAlert("", "Please Enter Payment Details", "Ok");
                return;
            }

            //Check payment details when veddio cunsultation is on
            if(vedioConsultationStatus== "true"&&string.IsNullOrEmpty(selectedPaymentMode))
            {
                DisplayAlert("", "Please Enter Payment Details", "Ok");
                return;
            }

            //book appointent webapi call
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
                    await DisplayAlert("", "Your appointment is booked successfully", "Ok");

                }
                else
                {
                    await DisplayAlert("", "Please enter values correctly", "Ok");
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
                payvisiblity.IsVisible = true;

            }
            else if (!VedioCunsultationCheckbox.IsChecked)
            {
                vedioConsultationStatus = "false";
                payvisiblity.IsVisible = false;
            }
        }
    }

}