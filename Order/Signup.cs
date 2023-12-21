using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Order
{
    public partial class SignupForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-09BJIA6\\SQLEXPRESS;Initial Catalog=Final;Integrated Security=True;";
        public SignupForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            string fullname = tboxName.Text;
            string pass = tboxPass.Text;
            string usertype = cboxType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(usertype))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace 'Users' with your actual table name
                string insertQuery = "INSERT INTO UserAccount (Fullname, Pass, Usertype) VALUES (@Fullname, @Pass, @Usertype)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Fullname", fullname);
                    command.Parameters.AddWithValue("@Pass", pass);
                    command.Parameters.AddWithValue("@Usertype", usertype);

                    command.ExecuteNonQuery();

                    MessageBox.Show("User successfully signed up!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void labelLogin_Click(object sender, EventArgs e)
        {
            Login loginform = new Login();
            this.Hide();

            loginform.FormClosed += (s, args) =>
            {
                this.Close();
            };

            loginform.ShowDialog();
        }
    }
}