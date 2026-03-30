using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalSystem.Models;

namespace HospitalSystem.Forms
{
    
    public class DashboardForm : Form
    {
        private Patient _currentPatient;

        public DashboardForm(Patient patient)
        {
            _currentPatient = patient;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "NHS Patient Portal - Dashboard";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

        
            Panel panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(0, 102, 178)
            };

            Label lblWelcome = new Label
            {
                Text = $"Welcome back, {_currentPatient.FullName}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Left = 20,
                Top = 20,
                AutoSize = true
            };

            Label lblDate = new Label
            {
                Text = $"Today: {DateTime.Now:dddd, dd MMMM yyyy}",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightCyan,
                Left = 20,
                Top = 55,
                AutoSize = true
            };

            Button btnLogout = new Button
            {
                Text = "Logout",
                Left = 590,
                Top = 35,
                Width = 90,
                Height = 32,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => { new LoginForm().Show(); this.Close(); };

            panelHeader.Controls.Add(lblWelcome);
            panelHeader.Controls.Add(lblDate);
            panelHeader.Controls.Add(btnLogout);


            Label lblTitle = new Label
            {
                Text = "What would you like to do today?",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 80),
                Left = 30,
                Top = 120,
                AutoSize = true
            };

           
            Panel tile1 = CreateTile("📅", "Book Appointment", "Find a doctor & book\na time slot", 30, 160, Color.FromArgb(0, 102, 178));
            Panel tile2 = CreateTile("📋", "My Medical Records", "View your diagnoses\n& prescriptions", 360, 160, Color.FromArgb(0, 140, 80));
            Panel tile3 = CreateTile("🗓", "My Appointments", "View, manage and\ncancel bookings", 30, 330, Color.FromArgb(180, 100, 0));
            Panel tile4 = CreateTile("👤", "My Profile", "Update personal\ninformation", 360, 330, Color.FromArgb(100, 0, 160));

            tile1.Click += (s, e) => { new BookAppointmentForm(_currentPatient).Show(); };
            foreach (Control c in tile1.Controls) c.Click += (s, e) => { new BookAppointmentForm(_currentPatient).Show(); };

            tile2.Click += (s, e) => { new MedicalRecordsForm(_currentPatient).Show(); };
            foreach (Control c in tile2.Controls) c.Click += (s, e) => { new MedicalRecordsForm(_currentPatient).Show(); };

            tile3.Click += (s, e) => { new MyAppointmentsForm(_currentPatient).Show(); };
            foreach (Control c in tile3.Controls) c.Click += (s, e) => { new MyAppointmentsForm(_currentPatient).Show(); };

            tile4.Click += (s, e) => { new ProfileForm(_currentPatient).Show(); };
            foreach (Control c in tile4.Controls) c.Click += (s, e) => { new ProfileForm(_currentPatient).Show(); };

            this.Controls.Add(panelHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(tile1);
            this.Controls.Add(tile2);
            this.Controls.Add(tile3);
            this.Controls.Add(tile4);
        }

        private Panel CreateTile(string icon, string title, string description, int left, int top, Color color)
        {
            Panel tile = new Panel
            {
                Left = left,
                Top = top,
                Width = 300,
                Height = 140,
                BackColor = color,
                Cursor = Cursors.Hand
            };

            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 22),
                ForeColor = Color.White,
                Left = 12,
                Top = 12,
                Width = 50,
                Height = 50,
                AutoSize = false,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Left = 68,
                Top = 18,
                Width = 220,
                Height = 28,
                AutoSize = false,
                BackColor = Color.Transparent
            };

            Label lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(220, 220, 220),
                Left = 68,
                Top = 50,
                Width = 220,
                Height = 50,
                AutoSize = false,
                BackColor = Color.Transparent
            };

            tile.Controls.Add(lblIcon);
            tile.Controls.Add(lblTitle);
            tile.Controls.Add(lblDesc);
            return tile;
        }
    }
}
