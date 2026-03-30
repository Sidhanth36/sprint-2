using System;
using System.Collections.Generic;
using HospitalSystem.Models;

namespace HospitalSystem.Data
{
    
    public static class DataStore
    {
        public static List<Patient> Patients = new List<Patient>();
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Appointment> Appointments = new List<Appointment>();
        public static List<MedicalRecord> MedicalRecords = new List<MedicalRecord>();

        private static int _nextPatientId = 3;
        private static int _nextAppointmentId = 3;

        static DataStore()
        {
            SeedData();
        }

        private static void SeedData()
        {
            
            Patients.Add(new Patient(1, "John Smith", "john.smith@email.com", "password123",
                "15/06/1985", "07700 900001", "12 High Street, Canterbury, CT1 1AA"));
            Patients.Add(new Patient(2, "Sarah Johnson", "sarah.j@email.com", "pass456",
                "22/03/1992", "07700 900002", "45 Park Road, Coventry, CV1 2BB"));

            
            Doctors.Add(new Doctor(1, "Dr. Emily Clarke", "General Practitioner",
                new List<string> { "09:00", "09:30", "10:00", "10:30", "11:00", "14:00", "14:30", "15:00" }));
            Doctors.Add(new Doctor(2, "Dr. James Patel", "Cardiology",
                new List<string> { "09:00", "10:00", "11:00", "13:00", "14:00", "15:00" }));
            Doctors.Add(new Doctor(3, "Dr. Linda Hughes", "Dermatology",
                new List<string> { "08:30", "09:30", "11:30", "13:30", "15:30" }));
            Doctors.Add(new Doctor(4, "Dr. Michael Brown", "Orthopaedics",
                new List<string> { "09:00", "10:30", "12:00", "14:00", "16:00" }));

            
            Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                PatientId = 1,
                DoctorId = 1,
                PatientName = "John Smith",
                DoctorName = "Dr. Emily Clarke",
                AppointmentDate = DateTime.Today.AddDays(3),
                TimeSlot = "10:00",
                Status = "Scheduled",
                Reason = "Annual check-up"
            });
            Appointments.Add(new Appointment
            {
                AppointmentId = 2,
                PatientId = 2,
                DoctorId = 2,
                PatientName = "Sarah Johnson",
                DoctorName = "Dr. James Patel",
                AppointmentDate = DateTime.Today.AddDays(7),
                TimeSlot = "14:00",
                Status = "Scheduled",
                Reason = "Heart palpitations"
            });

            MedicalRecords.Add(new MedicalRecord
            {
                RecordId = 1,
                PatientId = 1,
                DoctorName = "Dr. Emily Clarke",
                RecordDate = new DateTime(2025, 11, 10),
                Diagnosis = "Mild Hypertension",
                Treatment = "Lifestyle changes recommended",
                Prescription = "Amlodipine 5mg - once daily",
                Notes = "Patient advised to reduce salt intake and exercise regularly."
            });
            MedicalRecords.Add(new MedicalRecord
            {
                RecordId = 2,
                PatientId = 1,
                DoctorName = "Dr. Michael Brown",
                RecordDate = new DateTime(2025, 8, 5),
                Diagnosis = "Lower Back Pain",
                Treatment = "Physiotherapy sessions (6 weeks)",
                Prescription = "Ibuprofen 400mg - as needed",
                Notes = "MRI scan booked for follow-up."
            });
            MedicalRecords.Add(new MedicalRecord
            {
                RecordId = 3,
                PatientId = 2,
                DoctorName = "Dr. James Patel",
                RecordDate = new DateTime(2025, 12, 20),
                Diagnosis = "Atrial Fibrillation (paroxysmal)",
                Treatment = "Medication and monitoring",
                Prescription = "Bisoprolol 2.5mg - once daily",
                Notes = "ECG confirmed irregular rhythm. Follow-up in 3 months."
            });
        }

        public static int GetNextPatientId() => _nextPatientId++;
        public static int GetNextAppointmentId() => _nextAppointmentId++;

        public static Patient GetPatientByEmail(string email)
        {
            return Patients.Find(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public static List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return Appointments.FindAll(a => a.PatientId == patientId);
        }

        public static List<MedicalRecord> GetRecordsForPatient(int patientId)
        {
            return MedicalRecords.FindAll(r => r.PatientId == patientId);
        }
    }
}
