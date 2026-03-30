using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem.Forms
{
    
    public class RegisterForm : Form
    {
        private Panel panelHeader;
        private TextBox txtFullName, txtEmail, txtPassword, txtConfirmPassword, txtDOB, txtPhone, txtAddress;
        private Button btnRegister, btnBack;
        private Label lblMessage;

        public RegisterForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Patient Portal - Register";
            this.Size = new Size(500, 740);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(0, 150, 100)
            };

            Label lblTitle = new Label
            {
                Text = "Register as a New Patient",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTitle);

            
            int startY = 100;
            int spacing = 60;

            AddLabel("Full Name *", 60, startY);
            txtFullName = AddTextBox(60, startY + 25, 360);

            AddLabel("Email Address *", 60, startY + spacing);
            txtEmail = AddTextBox(60, startY + spacing + 25, 360);

            AddLabel("Password *", 60, startY + spacing * 2);
            txtPassword = AddPasswordBox(60, startY + spacing * 2 + 25, 360);

            AddLabel("Confirm Password *", 60, startY + spacing * 3);
            txtConfirmPassword = AddPasswordBox(60, startY + spacing * 3 + 25, 360);

            AddLabel("Date of Birth (DD/MM/YYYY)", 60, startY + spacing * 4);
            txtDOB = AddTextBox(60, startY + spacing * 4 + 25, 360);
            txtDOB.Text = "01/01/1990";

            AddLabel("Phone Number", 60, startY + spacing * 5);
            txtPhone = AddTextBox(60, startY + spacing * 5 + 25, 360);

            AddLabel("Address", 60, startY + spacing * 6);
            txtAddress = AddTextBox(60, startY + spacing * 6 + 25, 360);

            lblMessage = new Label
            {
                Left = 60,
                Top = startY + spacing * 7 + 5,
                Width = 360,
                Height = 35,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            btnRegister = new Button
            {
                Text = "Create Account",
                Left = 60,
                Top = startY + spacing * 7 + 45,
                Width = 360,
                Height = 42,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnBack = new Button
            {
                Text = "← Back to Login",
                Left = 60,
                Top = startY + spacing * 7 + 100,
                Width = 360,
                Height = 35,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 102, 178),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.Click += BtnBack_Click;

            this.Controls.Add(panelHeader);
            this.Controls.Add(lblMessage);
            this.Controls.Add(btnRegister);
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

        private TextBox AddTextBox(int left, int top, int width)
        {
            TextBox txt = new TextBox
            {
                Left = left,
                Top = top,
                Width = width,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }

        private TextBox AddPasswordBox(int left, int top, int width)
        {
            TextBox txt = AddTextBox(left, top, width);
            txt.PasswordChar = '●';
            return txt;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Please fill in all required fields (*)";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.Text = "Passwords do not match.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                lblMessage.Text = "Please enter a valid email address.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (DataStore.GetPatientByEmail(txtEmail.Text) != null)
            {
                lblMessage.Text = "An account with this email already exists.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            
            Patient newPatient = new Patient(
                DataStore.GetNextPatientId(),
                txtFullName.Text.Trim(),
                txtEmail.Text.Trim(),
                txtPassword.Text,
                txtDOB.Text.Trim(),
                txtPhone.Text.Trim(),
                string.IsNullOrWhiteSpace(txtAddress.Text) ? "Not provided" : txtAddress.Text.Trim()
            );

            DataStore.Patients.Add(newPatient);

            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = "Account created successfully!";

            MessageBox.Show($"Welcome, {newPatient.FullName}!\nYour account has been created.\nYou can now log in.",
                "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
