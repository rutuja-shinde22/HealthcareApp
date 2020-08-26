using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthcareApp.Model;

[assembly: Dependency(typeof(HealthcareApp.Droid.CommanServic))]
namespace HealthcareApp.Droid
{
    public sealed class CommanServic : ICommonInterface
    {
        DBData.Service service;

        public CommanServic()
        {
            service = new DBData.Service()
            {
                Url = "http://webapi.medismarthis.com/Service.asmx"
            };
        }

        public async Task<string> FillSpecialisation(int BranchId)
        {
            try
            {
                var result = service.FillSpecialisation(BranchId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<string> FillDoctorList1(string BranchId, string specialist)
        {
            try
            {
                var result = service.FillDoctorList1(BranchId, specialist);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        
        public async Task<string> GetPrescriptionDetails(string clientid,int Branchid)
        {
            try
            {
                var result = service.GetPrescriptionDetails(clientid, Branchid);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task<string> PrintPrescription(string clientId, int BranchId,int AppointmentId)
        {
            try
            {
                var result = service.PrintPrescription(clientId, BranchId, AppointmentId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> GetReport1(string GuestId, string BranchId)
        {
            try
            {
                var result = service.GetReport1(GuestId, BranchId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> GetReportinPdf1(string ID)
        {
            try
            {
                var result = service.GetReportinPdf1(ID);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> HealthTips1(string BranchId)
        {
            try
            {
                var result = service.HealthTips1(BranchId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> GetProfileDetails(int RegiNo, int BranchId)
        {
            try
            {
                var result = service.GetProfileDetails(RegiNo, BranchId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

            public async Task<string> GetPatientImage1(string PatientId, string BranchId)
        {
            try
            {
                var result = service.GetPatientImage1(PatientId, BranchId);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> UpdatePhoto(string RegiNo, int BranchId, string Image)
        {
            try
            {
                var result = service.UpdatePhoto(RegiNo, BranchId,Image);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<string> ForGotPassword(string emailId)
        {
            try
            {
                var result = service.ForGotPassword(emailId);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> ChangePassword(string username, string password, string RegiNo, string OldPassword)
        {
            try
            {
                var result = service.ChangePassword(username,password,RegiNo, OldPassword);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> UpdateProfile(int RegiNo, int BranchId, string address)
        {
            try
            {
                var result = service.UpdateProfile(RegiNo, BranchId, address);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> BookAppointment1(string ClientId, string Time, string BranchId, string DocId, string AppointmentDate, string IsConsultation, string UPIID, string PaymentMode)
        {
            try
            {
                var result = service.BookAppointment1(ClientId, Time, BranchId, DocId, AppointmentDate,IsConsultation,UPIID,PaymentMode);
            return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> DoctorTimeSlot1(string DocId, string Days, string BranchId, string Date)
        {
            try
            {
                var result = service.DoctorTimeSlot1(DocId, Days, BranchId, Date);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }


        public async Task<string> Login1(string username, string password)
        {
            try
            {
                
                var result = service.Login1(username, password);
                return result;
            }
            catch(Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> AppoinmentList1(string ClientId, string BranchId, string SpecialistId)
        {
            try
            {
                var result = service.AppoinmentList1(ClientId, BranchId, SpecialistId);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> UploadDocument(string PatientID, string DocumentType, string DocumentName, string DocumentExtension, string UploadDocument, string BranchID)
        {
            try
            {
                var result = service.UploadDocument(PatientID, DocumentType, DocumentName, DocumentExtension, UploadDocument,BranchID);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> GetSpecializationWiseCount(string PatientID, string BranchId, string FromDate, string ToDate)
        {
            try
            {
                var result = service.GetSpecializationWiseCount(PatientID, BranchId, FromDate, ToDate);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<string> GetVitalDetails(string PatientID, string FromDate, string ToDate)
        {
            try
            {
                var result = service.GetVitalDetails(PatientID, FromDate, ToDate);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
