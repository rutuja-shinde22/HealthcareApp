using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareApp.Model
{
    public class ReportModel
    {
        public string Date { get; set; }
        public string UploadId { get; set; }
        public string FilePath { get; set; }
        public string DocId { get; set; }
        public string DocImage { get; set; }
    }
}
