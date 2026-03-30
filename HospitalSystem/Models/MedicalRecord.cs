using System;

namespace HospitalSystem.Models
{
    public class MedicalRecord
    {
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }

        public MedicalRecord() { }
    }
}
