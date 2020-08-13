using HealthcareApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp
{
    public interface ICommonInterface
    {
        //Here We Can Call models for service in there formats
        Task<string>Login1(string username, string password);
        Task<string>GetPrescriptionDetails(string clientid, int Branchid);
        Task<string> GetReport1(string GuestId, string BranchId);
        Task<string> FillDoctorList1(string BranchId, string specialist);
        Task<string> HealthTips1(string BranchId);
        Task<string> GetReportinPdf1(string ID);
        Task<string> GetProfileDetails(int RegiNo, int BranchId);
        Task<string> ChangePassword(string username, string password, string RegiNo, string OldPassword);
        Task<string> GetPatientImage1(string PatientId, string BranchId);
        Task<string> UpdatePhoto(string RegiNo, int BranchId, string Image);
        Task<string> ForGotPassword(string emailid);
        Task<string> UpdateProfile(int RegiNo, int BranchId, string address);
        Task<string> PrintPrescription(string clientId, int BranchId, int AppointmentId);
        Task<string> BookAppointment1(string ClientId, string Time, string BranchId, string DocId, string AppointmentDate,string IsConsultation,string UPIID,string PaymentMode);
        Task<string> DoctorTimeSlot1(string DocId, string Days, string BranchId, string Date);
        Task<string> FillSpecialisation(int BranchId);
        Task<string> AppoinmentList1(string ClientId, string BranchId, string SpecialistId);
        Task<string> UploadDocument(string PatientID, string DocumentType, string DocumentName, string DocumentExtension, string UploadDocument, string BranchID);

    }
}
