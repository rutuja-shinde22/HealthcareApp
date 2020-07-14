using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareApp.Model
{
    public class Login
    {
        public string Username { get; set; }

        public string ClientId { get; set; }

        public string BranchId { get; set; }
        public string PatientSalutation { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }

        public int OpdPatientId { get; set; }
        public string BranchName { get; set; }
        public string Branch_Address { get; set; }
        
        public string Branch_EmailId { get; set; }

        // public string password { get; set; }

        //add another feilds as per Service Responce
    }
}
