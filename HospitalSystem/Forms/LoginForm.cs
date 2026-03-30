using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem.Forms
{
   
    public class LoginForm : Form
    {
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblEmail;
        private Label lblPassword;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblMessage;
        private Panel panelHeader;
        private Panel panelMain;

        public LoginForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Hospital Patient Portal - Login";
            this.Size = new Size(480, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(0, 102, 178)
            };

            lblTitle = new Label
            {
                Text = "🏥  NHS Patient Portal",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 480,
                Height = 50,
                Top = 20,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblSubtitle = new Label
            {
                Text = "Secure Patient Self-Service System",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightCyan,
                AutoSize = false,
                Width = 480,
                Height = 30,
                Top = 72,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);

           
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(50, 20, 50, 20)
            };

            lblEmail = new Label
            {
                Text = "Email Address",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Left = 60,
                Top = 50,
                AutoSize = true
            };

            txtEmail = new TextBox
            {
                Left = 60,
                Top = 75,
                Width = 340,
                Height = 35,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "john.smith@email.com"
            };

            lblPassword = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Left = 60,
                Top = 130,
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Left = 60,
                Top = 155,
                Width = 340,
                Height = 35,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                Text = "password123"
            };

            btnLogin = new Button
            {
                Text = "Login to Portal",
                Left = 60,
                Top = 220,
                Width = 340,
                Height = 45,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 102, 178),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            lblMessage = new Label
            {
                Left = 60,
                Top = 280,
                Width = 340,
                Height = 40,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            Label lblOr = new Label
            {
                Text = "─────────  or  ─────────",
                Left = 60,
                Top = 320,
                Width = 340,
                Height = 25,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            btnRegister = new Button
            {
                Text = "Register as New Patient",
                Left = 60,
                Top = 355,
                Width = 340,
                Height = 45,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 102, 178),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(0, 102, 178);
            btnRegister.Click += BtnRegister_Click;

            Label lblDemo = new Label
            {
                Text = "Demo credentials pre-filled above",
                Left = 60,
                Top = 415,
                Width = 340,
                Height = 20,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(btnLogin);
            panelMain.Controls.Add(lblMessage);
            panelMain.Controls.Add(lblOr);
            panelMain.Controls.Add(btnRegister);
            panelMain.Controls.Add(lblDemo);

            this.Controls.Add(panelMain);
            this.Controls.Add(panelHeader);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Please enter your email and password.";
                return;
            }

            Patient patient = DataStore.GetPatientByEmail(email);

            if (patient != null && patient.Password == password)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Login successful! Opening portal...";

                
                DashboardForm dashboard = new DashboardForm(patient);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Invalid email or password. Please try again.";
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
