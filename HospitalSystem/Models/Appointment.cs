using System;

namespace HospitalSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; } 
        public string Reason { get; set; }

        public Appointment() 
        {
            Status = "Scheduled";
        }
    }
}
