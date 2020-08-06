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
        List<TimeSlotsModel> timeslotsList;
        public string DocId;
        public string _branchId;
        public string _clientId;
        public string selectedDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        public string Currenttime = DateTime.Now.ToString("h:mm tt");
        public string selectedDay;
        public string selectedTimeSlot;
        public int i;
        public int count;
        public int halfcount;
        public string selectedPaymentMode;
        public string updId;
        public string vedioConsultationStatus = "false";
        public string first;
        public string middle;

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
             MyButtons.Children.Clear(); //just in case so you can call this code several times np..
                
          foreach (var item in timeslotsList)
            {
                 count = item.Slots.Count();
                    halfcount = count / 2;
                    first = item.Slots.First();
                    var last = item.Slots.Last();
                    middle = item.Slots[halfcount];
                   startTime.Text = first;
                    endTime.Text = middle;
                    
                
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
                            StyleId = item.Slots[i],  //use a property from event as id to be passed to handler
                            TabIndex = i
                        
                        };
                        int m = Convert.ToInt32(halfcount);
                        //set all buttons to default color
                        foreach (Button b in MyButtons.Children)
                        {
                            if (m >= b.TabIndex)
                            {
                                b.BackgroundColor = Color.LightGray;
                                b.TextColor = Color.Black;
                               
                            }
                            else
                            {
                                b.BackgroundColor = Color.LightSalmon;
                                b.TextColor = Color.Black;
                            }
                        }
                        //check selected day and display time slots depends on seleceted day
                        DateTime TodayDate = Convert.ToDateTime(selectedDate);
                        DateTime CurrentDate = DateTime.Now.Date;
                        int result = DateTime.Compare(TodayDate, CurrentDate);

                        //selected day in future display all time slots
                        if (result == 1)
                        {
                            TimelotsFrame.IsVisible = true;
                            vediotimeslotstack.IsVisible = true;
                            payvisiblity.IsVisible = true;
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
                                vediotimeslotstack.IsVisible = false;
                                payvisiblity.IsVisible = false;
                                availability.Text = "Not Available! Please select another date";
                               

                            }
                            //current time
                            else if (res == 0)
                            {
                                TimelotsFrame.IsVisible = true;
                                btn.IsVisible = true;
                                vediotimeslotstack.IsVisible = true;
                                payvisiblity.IsVisible = true;
                                availability.Text = "Available";
                            }
                            //future time
                            else
                            {
                               TimelotsFrame.IsVisible = true;
                                btn.IsVisible = true;
                                vediotimeslotstack.IsVisible = true;
                                payvisiblity.IsVisible = true;
                                availability.Text = "Available";
                            }
                            btn.Clicked += OnDynamicBtnClicked;
                            MyButtons.Children.Add(btn);
                            
                            i++;
                        }
                    }
                    setButtonColor();
            }
            }catch(Exception ex)
            {
                string m=ex.Message;
            }
        }
      public void setButtonColor()
        {
            int m = Convert.ToInt32(halfcount);
            //set all buttons to default color
            foreach (Button b in MyButtons.Children)
            {
                if (m >= b.TabIndex)
                {
                    b.BackgroundColor = Color.LightGray;
                    b.TextColor = Color.Black;
                }
                else
                {
                    b.BackgroundColor = Color.LightSalmon;
                    b.TextColor = Color.Black;
                }
            }
        }
        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            setButtonColor();
            var button = sender as Button;
            selectedTimeSlot = button.StyleId;
            var index = button.TabIndex;
           // DisplayAlert("Selected Time", selectedTimeSlot, "ok");
            button.BackgroundColor = Color.FromHex("#3498DB");
            button.TextColor = Color.White;

          //display online payment options olny for first half appointments
            if (index<halfcount)
            {
                stackVisiblity.IsVisible = true;
            }
            else
            {
                stackVisiblity.IsVisible = false;
            }
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
            try {
               
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
                    await Navigation.PushAsync(new HomePage());

                }
                    else if(msg== "Appointment already booked under this doctor")
                    {
                        await DisplayAlert("", "You already have an appointment with this doctor", "Ok");
                    }
                else
                {
                    await DisplayAlert("", "Please enter values correctly", "Ok");
                }
            }
            }catch(Exception ex)
            {
                var m=ex.Message;
            }
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            selectedDate = e.NewDate.ToString("dd-MMM-yyyy");

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
               // payvisiblity.IsVisible = true;

            }
            else if (!VedioCunsultationCheckbox.IsChecked)
            {
                vedioConsultationStatus = "false";
               // payvisiblity.IsVisible = true;
            }
        }
    }

}