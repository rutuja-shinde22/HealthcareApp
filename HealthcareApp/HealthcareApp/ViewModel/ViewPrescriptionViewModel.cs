using HealthcareApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthcareApp.ViewModel
{
    public class ViewPrescriptionViewModel
    {
        public ObservableCollection<ViewPrescriptionModel> prescriptionList { get; set; }

        public ViewPrescriptionViewModel()
        {
            prescriptionList = new ObservableCollection<ViewPrescriptionModel>()
            {
                new ViewPrescriptionModel(){Date="123",DoctorName="asn",PractisingFrom="jnak",DoctorId="2",BranchId=1,AppointmentId=23}
                //new ViewPrescriptionModel(){DoctorName="Consulted Dr Radhika Arora- Dentist",HospitalName="19 year experience overall(14 years as specialist)",Address="periodologiest,dentist",Date="6 jan 2020"},
                //new ViewPrescriptionModel(){DoctorName="Consulted Dr Radhika Arora- Dentist",HospitalName="19 year experience overall(14 years as specialist)",Address="periodologiest,dentist",Date="6 jan 2020"},
                //new ViewPrescriptionModel(){DoctorName="Consulted Dr Radhika Arora- Dentist",image="man",HospitalName="19 year experience overall(14 years as specialist)",Address="periodologiest,dentist",Date="6 jan 2020"},
                //new ViewPrescriptionModel(){DoctorName="Consulted Dr Radhika Arora- Dentist",image="man",HospitalName="19 year experience overall(14 years as specialist)",Address="periodologiest,dentist",Date="6 jan 2020"},
                //new ViewPrescriptionModel(){DoctorName="Consulted Dr Radhika Arora- Dentist",image="man",HospitalName="19 year experience overall(14 years as specialist)",Address="periodologiest,dentist",Date="6 jan 2020"}
            };
        }
    }
}