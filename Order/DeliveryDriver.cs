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
using Guna.UI2.WinForms;

namespace Order
{
    public partial class DeliveryDriver : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-09BJIA6\\SQLEXPRESS;Initial Catalog=Final;Integrated Security=True;";
        private Login loginFormInstance = new Login();
        public DeliveryDriver()
        {
            InitializeComponent();

            LoadOrderData();

        }
        private void LoadOrderData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Assuming you have a table named Delivered
                    string query = "SELECT OrderID, UserName, OrderDetails, TotalPrice, OrderStatus FROM OrderInformation";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create DataTable to store the results
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);


                            // Explicitly set column names
                            guna2DataGridView.Columns.Add("OrderID", "Order ID");
                            guna2DataGridView.Columns.Add("UserName", "User Name");
                            guna2DataGridView.Columns.Add("OrderDetails", "Order Details");
                            guna2DataGridView.Columns.Add("TotalPrice", "Total Price");
                            guna2DataGridView.Columns.Add("OrderStatus", "Order Status");

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

        private void guna2ButtonAccept_Click(object sender, EventArgs e)
        {
            // Get the OrderID from user input (assuming it's entered in a TextBox named guna2TextBox1)
            if (int.TryParse(guna2TBox.Text, out int orderIdToUpdate))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Assuming you have a table named Delivered
                        string updateQuery = "UPDATE OrderInformation SET OrderStatus = 'Accepted' WHERE OrderID = @OrderId AND OrderStatus = 'Pending'";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", orderIdToUpdate);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"OrderID {orderIdToUpdate} updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // After updating, reload the data to reflect changes in the DataGridView
                                LoadOrderData();
                                guna2TBox.Text = "";
                            }
                            else
                            {
                                MessageBox.Show($"No rows updated. OrderID {orderIdToUpdate} may not exist or its status is not 'Pending'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid OrderID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ButtonDecline_Click(object sender, EventArgs e)
        {
            // Get the OrderID from user input (assuming it's entered in a TextBox named guna2TextBox1)
            if (int.TryParse(guna2TBox.Text, out int orderIdToUpdate))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Assuming you have a table named Delivered
                        string updateQuery = "UPDATE OrderInformation SET OrderStatus = 'Declined' WHERE OrderID = @OrderId AND OrderStatus = 'Pending'";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", orderIdToUpdate);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"OrderID {orderIdToUpdate} updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // After updating, reload the data to reflect changes in the DataGridView
                                LoadOrderData();
                                guna2TBox.Text = "";
                            }
                            else
                            {
                                MessageBox.Show($"No rows updated. OrderID {orderIdToUpdate} may not exist or its status is not 'Pending'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid OrderID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ButtonDelete_Click(object sender, EventArgs e)
        {
            // Get the OrderID from user input (assuming it's entered in a TextBox named guna2TextBoxOrderId)
            if (int.TryParse(guna2TBox.Text, out int orderIdToDelete))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Assuming you have a table named Delivered
                        string deleteQuery = "DELETE FROM OrderInformation WHERE OrderID = @OrderId";

                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", orderIdToDelete);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"OrderID {orderIdToDelete} deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // After deleting, reload the data to reflect changes in the DataGridView
                                LoadOrderData();
                                guna2TBox.Text = "";
                            }
                            else
                            {
                                MessageBox.Show($"No rows deleted. OrderID {orderIdToDelete} may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid OrderID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeliveryDriver_Load(object sender, EventArgs e)
        {
            // Set the text for guna2HtmlLabel4
            string labelText = "Howdy," + loginFormInstance.GetLoggedInUsername() + "!";
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

        private void guna2ButtonLogout_Click(object sender, EventArgs e)
        {
            loginFormInstance.Show();
            this.Dispose();
        }

        private void guna2ButtonOrders_Click(object sender, EventArgs e)
        {
            guna2Panel3.Visible = true;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (guna2Panel5.Size == new Size(200, 502))
            {
                guna2Panel5.Size = new Size(26, 502); 
                this.Size = new Size(813, 541);
            }
            else
            {

                guna2Panel5.Size = new Size(200, 502); // full panel size
                this.Size = new Size(1077, 541); // full form size
            }
        }

        private void guna2DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
