using Guna.UI2.WinForms;
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

namespace Order
{
    public partial class Admin : Form
    {
        private string connectionString = "Data Source=DESKTOP-09BJIA6\\SQLEXPRESS;Initial Catalog=Final;Integrated Security=True;";
        private Login loginFormInstance = new Login();
        public Admin()
        {
            InitializeComponent();
            RetrieveAdminViewData();
            RetrieveUserAccountData();

        }

        private void RetrieveAdminViewData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Username, UserType, LoginTime FROM AdminView";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create DataTable to store the results
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // Explicitly set column names
                            guna2DataGridView.Columns.Add("Id", "ID");
                            guna2DataGridView.Columns.Add("Username", "Username");
                            guna2DataGridView.Columns.Add("UserType", "User Type");
                            guna2DataGridView.Columns.Add("LoginTime", "Login Time");

                            // Adjust columns Headers to 15
                            guna2DataGridView.ColumnHeadersHeight = 15;
                            // Clear existing columns
                            guna2DataGridView.Columns.Clear();

                            // Bind the DataTable to the DataGridView
                            guna2DataGridView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void RetrieveUserAccountData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Fullname, Pass, Usertype FROM UserAccount";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create DataTable to store the results
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // Explicitly set column names
                            guna2DataGridView1.Columns.Add("Fullname", "Full Name");
                            guna2DataGridView1.Columns.Add("Pass", "Password");
                            guna2DataGridView1.Columns.Add("Usertype", "User Type");

                            // Adjust columns Headers to 15
                            guna2DataGridView1.ColumnHeadersHeight = 15;

                            // Clear existing columns
                            guna2DataGridView1.Columns.Clear();

                            // Bind the DataTable to the DataGridView
                            guna2DataGridView1.DataSource = dataTable;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DeleteUserAccountData(string fullname)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the DELETE query
                    string deleteQuery = "DELETE FROM UserAccount WHERE Fullname = @Fullname";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Fullname", fullname);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"User '{fullname}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Refresh the DataGridView or update the display
                             RetrieveUserAccountData();
                        }
                        else
                        {
                            MessageBox.Show($"User '{fullname}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ButtonLogs_Click(object sender, EventArgs e)
        {
            guna2DataGridView.Visible = true;
            guna2Panel3.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView.Visible = false;
            guna2Panel3.Visible = true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string fullnameToDelete = guna2TextBox.Text.Trim();

            if (!string.IsNullOrEmpty(fullnameToDelete))
            {
                // Call the method to delete user data
                DeleteUserAccountData(fullnameToDelete);
            }
            else
            {
                MessageBox.Show("Please enter the Fullname to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // Set the text for guna2HtmlLabel4
            string labelText = "Hello Boss, " + loginFormInstance.GetLoggedInUsername() + "!";
            guna2HtmlLabel4.Text = labelText;

            // Calculate the width of the label's text
            using (Graphics g = CreateGraphics())
            {
                SizeF textSize = g.MeasureString(labelText, guna2HtmlLabel4.Font);

                // Calculate the X position to center the label
                int xPosition = (guna2Panel6.Width - (int)textSize.Width) / 2;

                // Set the location of the label to center it
                guna2HtmlLabel4.Location = new Point(xPosition, guna2HtmlLabel4.Location.Y);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2Panel5.Size == new Size(24, 493))
            {
                guna2Panel5.Size = new Size(200, 493);
                this.Size = new Size(1084, 482);
            }
            else
            {
                
                guna2Panel5.Size = new Size(24, 493);
                this.Size = new Size(903, 482);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            loginFormInstance.Show();
            this.Dispose();
        }

        private void guna2Panel2Pic_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
