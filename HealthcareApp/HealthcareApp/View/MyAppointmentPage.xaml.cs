﻿using HealthcareApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthcareApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAppointmentPage : ContentPage
    {
        public string _clientId;
        public string _branchId;
        public string _specializationId;
        private Uri uri;
        string MeetUrl;
        public DateTime CurrentDate = DateTime.Now.Date;
        public DateTime appointmentDate;
        public string VedioCunsultationStatus { get; set; }
        public string resconstatus;
        public string resdate;

            
        public MyAppointmentPage()
        {
            InitializeComponent();
         
            if (Application.Current.Properties.ContainsKey("ClientId"))
            {
                _clientId = Application.Current.Properties["ClientId"].ToString();
                _branchId = Application.Current.Properties["BranchId"].ToString();
            }
            displayBookedAppointments();
        }
        


        public async void displayBookedAppointments()
        {
            //display Appointment list based on specialistId
            var details = await App.HealthSoapService.AppoinmentList1(_clientId, _branchId, _specializationId);
            if ((details != null) && (details.Length > 0))
            {
                //Deserialize object and save in res
                var res = JsonConvert.DeserializeObject<List<MyAppointmentsModule>>(details);
                foreach(var item in res)
                {
                    resconstatus = item.videoConsultation;
                    resdate = item.AppoinemtDate;

                    appointmentDate = Convert.ToDateTime(resdate);
                    int result = DateTime.Compare(appointmentDate, CurrentDate);
                    if (resconstatus == "True" && result>=0)
                    {
                        item.v = "True";
                       // item.bgcolor = "True";
                        
                    }
                    else
                    {
                        item.v = "False";
                       // item.bgcolor = "False";
                    }
                    if (result >= 0)
                    {
                        item.bgcolor = "True";
                    }
                    else
                    {
                        item.bgcolor = "False";
                    }

                }

                listView.ItemsSource = res;
            }

         }

        private async void JionMeetingButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.CommandParameter as MyAppointmentsModule;
            string _isVedioConsultation = item.videoConsultation;

            
            if (_isVedioConsultation == "True")
            {
                MeetUrl = item.MeetingURL;
                if (string.IsNullOrEmpty(MeetUrl))
                {
                    await DisplayAlert("", "Meeting will start within some time, please try after some time", "Ok");
                    return;
                }
                uri = new Uri(MeetUrl);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            else
            {
                await DisplayAlert("", "This Appointment is not selected as vedio cunsultation", "Ok");
                return;
            }
        }
    }
}