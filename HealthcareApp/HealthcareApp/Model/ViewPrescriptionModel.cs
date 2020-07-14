using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareApp.Model
{
    public class ViewPrescriptionModel
    {
        public string Date { get; set; }
        public string DoctorName { get; set; }
        public string PractisingFrom { get; set; }
        public string DoctorId { get; set; }
        public int BranchId { get; set; }
        public int AppointmentId { get; set; }
        public string DocImage { get; set; }


    }
}
