using System;

namespace HospitalSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Patient() { }

        public Patient(int id, string fullName, string email, string password, string dob, string phone, string address)
        {
            PatientId = id;
            FullName = fullName;
            Email = email;
            Password = password;
            DateOfBirth = dob;
            PhoneNumber = phone;
            Address = address;
        }
    }
}
