using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem.Forms
{

    public class BookAppointmentForm : Form
    {
        private Patient _currentPatient;
        private ComboBox cmbDoctor;
        private DateTimePicker dtpDate;
        private ComboBox cmbTimeSlot;
        private TextBox txtReason;
        private Label lblDoctorSpecialism;
        private Label lblMessage;
        private Button btnBook, btnBack;

        public BookAppointmentForm(Patient patient)
        {
            _currentPatient = patient;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Book an Appointment";
            this.Size = new Size(520, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            
            Panel panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(0, 102, 178)
            };
            Label lblTitle = new Label
            {
                Text = "📅  Book an Appointment",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTitle);

           
            AddLabel("Select Doctor", 40, 95);
            cmbDoctor = new ComboBox
            {
                Left = 40,
                Top = 120,
                Width = 420,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            foreach (var doc in DataStore.Doctors)
                cmbDoctor.Items.Add($"{doc.FullName} — {doc.Specialisation}");
            cmbDoctor.SelectedIndex = 0;
            cmbDoctor.SelectedIndexChanged += CmbDoctor_SelectedIndexChanged;

            lblDoctorSpecialism = new Label
            {
                Left = 40,
                Top = 155,
                Width = 420,
                Height = 22,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(0, 120, 80),
                AutoSize = false
            };
            UpdateDoctorInfo();

           
            AddLabel("Preferred Date", 40, 185);
            dtpDate = new DateTimePicker
            {
                Left = 40,
                Top = 210,
                Width = 420,
                Font = new Font("Segoe UI", 10),
                MinDate = DateTime.Today.AddDays(1),
                MaxDate = DateTime.Today.AddMonths(3),
                Format = DateTimePickerFormat.Long
            };

            
            AddLabel("Available Time Slots", 40, 255);
            cmbTimeSlot = new ComboBox
            {
                Left = 40,
                Top = 280,
                Width = 420,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            LoadTimeSlots();

        
            AddLabel("Reason for Appointment", 40, 325);
            txtReason = new TextBox
            {
                Left = 40,
                Top = 350,
                Width = 420,
                Height = 70,
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Briefly describe your reason for visiting..."
            };

            lblMessage = new Label
            {
                Left = 40,
                Top = 435,
                Width = 420,
                Height = 30,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            btnBook = new Button
            {
                Text = "Confirm Booking",
                Left = 40,
                Top = 470,
                Width = 200,
                Height = 42,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 102, 178),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBook.FlatAppearance.BorderSize = 0;
            btnBook.Click += BtnBook_Click;

            btnBack = new Button
            {
                Text = "← Back",
                Left = 260,
                Top = 470,
                Width = 200,
                Height = 42,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 102, 178),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.Click += (s, e) => this.Close();

            this.Controls.Add(panelHeader);
            this.Controls.Add(cmbDoctor);
            this.Controls.Add(lblDoctorSpecialism);
            this.Controls.Add(dtpDate);
            this.Controls.Add(cmbTimeSlot);
            this.Controls.Add(txtReason);
            this.Controls.Add(lblMessage);
            this.Controls.Add(btnBook);
            this.Controls.Add(btnBack);
        }

        private void AddLabel(string text, int left, int top)
        {
            Label lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Left = left,
                Top = top,
                AutoSize = true
            };
            this.Controls.Add(lbl);
        }

        private void CmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDoctorInfo();
            LoadTimeSlots();
        }

        private void UpdateDoctorInfo()
        {
            if (cmbDoctor.SelectedIndex >= 0)
            {
                var doctor = DataStore.Doctors[cmbDoctor.SelectedIndex];
                lblDoctorSpecialism.Text = $"✔ Specialisation: {doctor.Specialisation}  |  {doctor.AvailableSlots.Count} slots available";
            }
        }

        private void LoadTimeSlots()
        {
            cmbTimeSlot.Items.Clear();
            if (cmbDoctor.SelectedIndex >= 0)
            {
                var doctor = DataStore.Doctors[cmbDoctor.SelectedIndex];
                foreach (var slot in doctor.AvailableSlots)
                    cmbTimeSlot.Items.Add(slot);
                if (cmbTimeSlot.Items.Count > 0)
                    cmbTimeSlot.SelectedIndex = 0;
            }
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (cmbDoctor.SelectedIndex < 0 || cmbTimeSlot.SelectedIndex < 0)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please select a doctor and time slot.";
                return;
            }

            var doctor = DataStore.Doctors[cmbDoctor.SelectedIndex];

            Appointment appointment = new Appointment
            {
                AppointmentId = DataStore.GetNextAppointmentId(),
                PatientId = _currentPatient.PatientId,
                DoctorId = doctor.DoctorId,
                PatientName = _currentPatient.FullName,
                DoctorName = doctor.FullName,
                AppointmentDate = dtpDate.Value.Date,
                TimeSlot = cmbTimeSlot.SelectedItem.ToString(),
                Status = "Scheduled",
                Reason = string.IsNullOrWhiteSpace(txtReason.Text) || txtReason.Text == "Briefly describe your reason for visiting..." ? "Not specified" : txtReason.Text
            };

            DataStore.Appointments.Add(appointment);

            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = "✔ Appointment booked successfully!";

            MessageBox.Show(
                $"Your appointment has been booked!\n\n" +
                $"Doctor: {doctor.FullName}\n" +
                $"Date: {appointment.AppointmentDate:dd/MM/yyyy}\n" +
                $"Time: {appointment.TimeSlot}\n\n" +
                $"Please arrive 10 minutes early.",
                "Booking Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}
