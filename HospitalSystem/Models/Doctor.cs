using System;
using System.Collections.Generic;

namespace HospitalSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string Specialisation { get; set; }
        public List<string> AvailableSlots { get; set; }

        public Doctor() 
        {
            AvailableSlots = new List<string>();
        }

        public Doctor(int id, string fullName, string specialisation, List<string> slots)
        {
            DoctorId = id;
            FullName = fullName;
            Specialisation = specialisation;
            AvailableSlots = slots ?? new List<string>();
        }
    }
}
