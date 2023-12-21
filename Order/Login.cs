using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Order.Model;

namespace Order
{
    public partial class Login : Form
    {

        private const string connectionString = "Data Source=DESKTOP-09BJIA6\\SQLEXPRESS;Initial Catalog=Final;Integrated Security=True;";
        private static string loggedInUsername;


        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            checkloginPass.CheckedChanged += checkloginPass_CheckedChanged;
            LoginSign.Click +=LoginSign_Click;
        }
        private void checkloginPass_CheckedChanged(object sender, EventArgs e)
        {
            textloginPass.PasswordChar = checkloginPass.Checked ? '\0' : '*';
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textloginName.Text;
            string password = textloginPass.Text;
            string Usertype = combologinType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Usertype))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Pass, Usertype FROM UserAccount WHERE Fullname = @Username";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Pass"].ToString().Trim();
                            string storedUserType = reader["Usertype"].ToString().Trim();

                            if (password == storedPassword)
                            {
                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                loggedInUsername = username;
                                // Show the corresponding form based on userType
                                switch (storedUserType)
                                {
                                    case "Customer":
                                        LogUserLogin(username, "Customer");
                                        User user = new User();
                                        user.Show();
                                        break;
                                    case "Admin":
                                        LogUserLogin(username, "Admin");
                                        Admin adminForm = new Admin();
                                        adminForm.Show();
                                        break;
                                    case "Seller":
                                        LogUserLogin(username, "Seller");
                                        Seller sellerForm = new Seller();
                                        sellerForm.Show();
                                        break;
                                    case "Delivery Driver":
                                        LogUserLogin(username, "Delivery Driver");
                                        DeliveryDriver deliveryDriverForm = new DeliveryDriver();
                                        deliveryDriverForm.Show();
                                        break;
                                    default:
                                        MessageBox.Show("Invalid userType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }

                                // Hide the Login form
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void LogUserLogin(string username, string userType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO AdminView (Username, UserType, LoginTime) VALUES (@Username, @UserType, @LoginTime)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@UserType", userType);
                    command.Parameters.AddWithValue("@LoginTime", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetLoggedInUsername()
        {
            return loggedInUsername;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void LoginSign_Click(object sender, EventArgs e)
        {
            SignupForm signupform = new SignupForm();
            this.Hide();

            signupform.FormClosed += (s, args) =>
            {
                this.Close();
            };

            signupform.ShowDialog();
        }
        public static class InputBox
        {
            public static string Show(string caption, string prompt, string defaultText = "")
            {
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = caption;
                label.Text = prompt;
                textBox.Text = defaultText;

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                DialogResult dialogResult = form.ShowDialog();

                return dialogResult == DialogResult.OK ? textBox.Text : string.Empty;
            }
        }
        private void ShowCopyableMessageBox(string caption, string message)
        {
            Form messageBoxForm = new Form();
            TextBox textBox = new TextBox();
            Button copyButton = new Button();

            messageBoxForm.Text = caption;
            textBox.Text = message;

            copyButton.Text = "Copy";
            copyButton.DialogResult = DialogResult.OK;

            textBox.SetBounds(12, 12, 372, 120);
            copyButton.SetBounds(309, 140, 75, 23);

            textBox.ReadOnly = true;
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.WordWrap = false;

            copyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            messageBoxForm.ClientSize = new Size(396, 175);
            messageBoxForm.Controls.AddRange(new Control[] { textBox, copyButton });
            messageBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            messageBoxForm.StartPosition = FormStartPosition.CenterScreen;
            messageBoxForm.MinimizeBox = false;
            messageBoxForm.MaximizeBox = false;

            DialogResult dialogResult = messageBoxForm.ShowDialog();

            // If the user clicks the Copy button, copy the text to the clipboard
            if (dialogResult == DialogResult.OK)
            {
                Clipboard.SetText(textBox.Text);
            }
        }
            private void label1_Click(object sender, EventArgs e)
            {
                string userInput = InputBox.Show("Input Box", "Enter Fullname:");

                // Retrieve the password from the database based on Fullname
                string password = RetrievePasswordFromDatabase(userInput);

                // Update the label with the details
                if (!string.IsNullOrEmpty(password))
                {
                    ShowCopyableMessageBox("Password Retrieval", $"{password}");
                }
                else
                {
                    MessageBox.Show("User not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        private string RetrievePasswordFromDatabase(string fullname)
        {
            string password = null;
            string userType = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Pass, Usertype FROM UserAccount WHERE Fullname = @Fullname";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Fullname", fullname);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["Pass"].ToString().Trim();
                            userType = reader["Usertype"].ToString().Trim();

                            // Check if the Usertype is "Admin"
                            if (string.Equals(userType, "Admin", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Forbidden!");
                                password = "RESTRICTED";
                            }
                        }
                    }
                }
            }

            return password;
        }
    }
}
