using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order
{
    public partial class User : Form
    {
      
        private Login loginFormInstance = new Login();
        private decimal totalOrderPrice = 0;
        private Dictionary<string, int> foodPrices;
        private const string connectionString = "Data Source=DESKTOP-09BJIA6\\SQLEXPRESS;Initial Catalog=Final;Integrated Security=True;";


        public User()
        {
            InitializeComponent();
            InitializeFoodPrices();
            
        }
        private void InitializeFoodPrices()
        {
            foodPrices = new Dictionary<string, int>()
            {
                { label1.Text, 120 },
                { label3.Text, 60 },
                { label6.Text, 80 },
                { label7.Text, 60 },
                { label9.Text, 130 },
                { label11.Text, 50 },
                { label21.Text, 165 },
                { label23.Text, 199 },
                { label20.Text, 235 },
                { label13.Text, 456 },
                // Add more food items as needed
            };
        }

        

        private void pictureComboMeal_Click(object sender, EventArgs e)
        {
            guna2GroupBox1.Visible = true;
            guna2GroupBox2.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Success");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            testButton.Visible = true;
            guna2Button2.Visible = false;
            guna2Button3.Visible = false;
            guna2Button8.Visible = false;
            guna2Button10.Visible = false;
            guna2Button12.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            guna2Panel1.Visible = true;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label1.Text;
                    int quantity = int.Parse(guna2ComboBox1.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    testButton.Visible = false;
                    guna2Panel1.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Panel2.Visible = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label3.Text;
                    int quantity = int.Parse(guna2ComboBox2.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button2.Visible = false;
                    guna2Panel2.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            guna2Button2.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            guna2Button2.Visible = true;
            testButton.Visible = false;
            guna2Button3.Visible = false;
            guna2Button8.Visible = false;
            guna2Button10.Visible = false;
            guna2Button12.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string usernameToRetrieve = loginFormInstance.GetLoggedInUsername();

            guna2Panel4.Visible = true;
            // Fetch data from the Orders table
            List<OrderItem> orderItems = GetOrderItemsFromDatabase(usernameToRetrieve);

            // Display the data in guna2Panel4
            DisplayOrderItems(orderItems);
        }
        private List<OrderItem> GetOrderItemsFromDatabase(string username)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Select query to retrieve data from the Orders table for a specific user
                string selectQuery = "SELECT UserName, FoodName, Quantity, Price FROM Orders WHERE UserName = @UserName";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read data from the reader
                            string foodName = reader["FoodName"].ToString();
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            decimal price = Convert.ToDecimal(reader["Price"]);

                            // Create OrderItem object and add to the list
                            OrderItem orderItem = new OrderItem(username, foodName, quantity, price);
                            orderItems.Add(orderItem);
                        }
                    }
                }
            }

            return orderItems;
        }


        private void DisplayOrderItems(List<OrderItem> orderItems)
        {
            // Clear previous controls in the panel
            guna2Panel4.Controls.Clear();

            totalOrderPrice = 0;

            // Display data in guna2Panel4
            int yOffset = 20; // Initial Y offset

           
            foreach (var orderItem in orderItems)
            {
                // Create labels to display order information
                Label lblOrder = new Label();
              //  lblOrder.Text = $"{orderItem.UserName} ordered {orderItem.Quantity} {orderItem.FoodName} for {orderItem.Price:C}";
                lblOrder.Text = $"{orderItem.FoodName} x {orderItem.Quantity} qty = {orderItem.Price}";
               // lblOrder.Location = new Point(20, yOffset);
                lblOrder.AutoSize = true;
                lblOrder.Font = new Font("Courier New", 12, FontStyle.Bold);

                int xPos = (guna2Panel4.Width - lblOrder.Width) / 2;

                // Set the location of the label
                lblOrder.Location = new Point(xPos, yOffset);

                // Add label to the panel
                guna2Panel4.Controls.Add(lblOrder);

                // Increment Y offset for the next label
                yOffset += lblOrder.Height + 5;

                totalOrderPrice += orderItem.Price;


            }

            Label lblSubtotal = new Label();
            lblSubtotal.Text = $"\u20B1{totalOrderPrice:N2}";
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Courier New", 20, FontStyle.Bold);

            // Set the location of the subtotal label beside guna2HtmlLabel4
            lblSubtotal.Location = new Point(guna2HtmlLabel4.Right + 20, guna2HtmlLabel4.Top);

            // Calculate total including a fixed amount (50)
            decimal total = totalOrderPrice + 50;

            Label lblTotal = new Label();
            lblTotal.Text = $"\u20B1{total:N2}";
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Courier New", 20, FontStyle.Bold);

            // Set the location of the total label beside guna2HtmlLabel4
            lblTotal.Location = new Point(guna2HtmlLabel2.Right + 20, guna2HtmlLabel2.Top);

            guna2Panel4.Controls.Add(guna2HtmlLabel1);
            guna2Panel4.Controls.Add(guna2Button6);
            guna2Panel4.Controls.Add(guna2Button13);
            guna2Panel4.Controls.Add(guna2HtmlLabel2);
            guna2Panel4.Controls.Add(guna2HtmlLabel3);
            guna2Panel4.Controls.Add(guna2HtmlLabel4);
            guna2Panel4.Controls.Add(lblSubtotal);
            guna2Panel4.Controls.Add(lblTotal);

        }

        // Define a class to represent an order item
        public class OrderItem
        {
            public string UserName { get; set; }
            public string FoodName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public OrderItem(string userName, string foodName, int quantity, decimal price)
            {
                UserName = userName;
                FoodName = foodName;
                Quantity = quantity;
                Price = price;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Get the username for whom orders should be deleted
            string username = loginFormInstance.GetLoggedInUsername();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username is null. Cannot proceed with the deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there are orders for the user
                bool ordersExist = CheckIfOrdersExist(username, connection);

                if (!ordersExist)
                {
                    MessageBox.Show($"Order first before placing order.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit the method if no orders are found
                }

                List<(string FoodName, int Quantity, decimal Price)> orderDetails = GetOrderDetailsForUser(username);
                MarkOrderAsPending(username, orderDetails);
                PrintOrderDetails(orderDetails);

                // Begin a SQL transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {

                    try
                    {
                        // Step 1: Move data from Orders to Receipt
                        string moveDataQuery = "INSERT INTO Receipt (UserName, FoodName, Quantity, Price) " +
                                               "SELECT UserName, FoodName, Quantity, Price FROM Orders WHERE UserName = @UserName";
                        using (SqlCommand moveDataCommand = new SqlCommand(moveDataQuery, connection, transaction))
                        {
                            moveDataCommand.Parameters.AddWithValue("@UserName", username);
                            moveDataCommand.ExecuteNonQuery();
                        }

                        // Step 2: Delete data from Orders
                        string deleteQuery = "DELETE FROM Orders WHERE UserName = @UserName";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@UserName", username);
                            deleteCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction if both steps are successful
                        transaction.Commit();

                        
                        //Hides the panel
                        guna2Panel4.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        // An error occurred, rollback the transaction
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private bool CheckIfOrdersExist(string username, SqlConnection connection)
        {
            string checkQuery = "SELECT COUNT(*) FROM Orders WHERE UserName = @UserName";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@UserName", username);
                int orderCount = (int)checkCommand.ExecuteScalar();
                return orderCount > 0;
            }
        }
        //guna2Panel4.Visible = false;
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            guna2Button3.Visible = true;
            guna2Button2.Visible = false;
            testButton.Visible = false;
            guna2Button8.Visible = false;
            guna2Button10.Visible = false;
            guna2Button12.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;

        }


        private void pictureBox8_Click(object sender, EventArgs e)
        {
            guna2Button10.Visible = true;
            guna2Button8.Visible = false;
            guna2Button3.Visible = false;
            guna2Button2.Visible = false;
            testButton.Visible = false;
            guna2Button12.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2Panel7.Visible = true;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label6.Text;
                    int quantity = int.Parse(guna2ComboBox3.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button3.Visible = false;
                    guna2Panel3.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Panel3.Visible = true;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            guna2Button12.Visible = true;
            guna2Button10.Visible = false;
            guna2Button8.Visible = false;
            guna2Button3.Visible = false;
            guna2Button2.Visible = false;
            testButton.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;
        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2Panel6.Visible = true;
        }

      

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label9.Text;
                    int quantity = int.Parse(guna2ComboBox5.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button10.Visible = false;
                    guna2Panel6.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label11.Text;
                    int quantity = int.Parse(guna2ComboBox6.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button12.Visible = false;
                    guna2Panel7.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private bool ProcessOrder(string foodName, int quantity)
        {
            
            if (IsQuantityAvailable(foodName, quantity))
            {
                UpdateQuantityAvailable(foodName, quantity);

                return true;
            }
            else
            {
                MessageBox.Show($"Sorry, {foodName} is out of stock or has insufficient quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
        private bool IsQuantityAvailable(string foodName, int quantity)
        {
            // Retrieve the current quantity available from the database
            int currentQuantity = GetQuantityAvailableFromDatabase(foodName);
 

            // Check if there's enough quantity available
            return currentQuantity >= quantity;
        }

        private int GetQuantityAvailableFromDatabase(string foodName)
        {
            int quantityAvailable = 0;

            // SQL query to retrieve quantity available for the specified food item
            string selectQuery = "SELECT QuantityAvailable FROM FoodItems WHERE FoodName = @FoodName";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Add parameter to the command
                        command.Parameters.AddWithValue("@FoodName", foodName);

                        // Execute the query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows returned
                            if (reader.Read())
                            {
                                // Read the quantity available from the reader
                                quantityAvailable = Convert.ToInt32(reader["QuantityAvailable"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                MessageBox.Show($"Error retrieving quantity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Math.Max(0, quantityAvailable);
        }



        private void UpdateQuantityAvailable(string foodName, int quantity)
        {
            try
            {
                // SQL query to update quantity available for the specified food item
                string updateQuery = "UPDATE FoodItems SET QuantityAvailable = QuantityAvailable - @Quantity WHERE FoodName = @FoodName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@FoodName", foodName);
                        command.Parameters.AddWithValue("@Quantity", quantity);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                MessageBox.Show($"Error updating quantity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MarkOrderAsPending(string userName, List<(string FoodName, int Quantity, decimal Price)> orderDetails, string orderStatus = "Pending")
        {
            try
            {
                // Concatenate order details into a single string
                string concatenatedOrderDetails = string.Join(", ", orderDetails.Select(item =>
                    $"{item.Quantity} {item.FoodName} (₱{item.Price.ToString("0.00")})"
                ));

                // Calculate total price
                decimal totalPrice = orderDetails.Sum(item => (item.Quantity * foodPrices[item.FoodName]) + 50);
                // SQL query to insert into Delivered table
                string insertQuery = "INSERT INTO OrderInformation (UserName, OrderDetails, TotalPrice, OrderStatus) VALUES (@UserName, @OrderDetails, @TotalPrice, @OrderStatus)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@OrderDetails", concatenatedOrderDetails);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@OrderStatus", orderStatus);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                MessageBox.Show($"Error marking order as delivered: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<(string FoodName, int Quantity, decimal Price)> GetOrderDetailsForUser(string username)
        {
            List<(string FoodName, int Quantity, decimal Price)> orderDetails = new List<(string FoodName, int Quantity, decimal Price)>();

            try
            {
                // SQL query to retrieve order details for the specific user
                string selectQuery = "SELECT FoodName, Quantity, Price FROM Orders WHERE UserName = @UserName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Add parameter to the command
                        command.Parameters.AddWithValue("@UserName", username);

                        // Execute the query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Read order details from the reader and add them to the list
                                string foodName = reader["FoodName"].ToString();
                                int quantity = Convert.ToInt32(reader["Quantity"]);
                                decimal price = Convert.ToDecimal(reader["Price"]);

                                orderDetails.Add((foodName, quantity, price));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                MessageBox.Show($"Error retrieving order details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return orderDetails;
        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            guna2Panel5.Visible = true;
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label7.Text;
                    int quantity = int.Parse(guna2ComboBox4.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button8.Visible = false;
                    guna2Panel5.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }
                    

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            guna2Button8.Visible = true;
            guna2Button3.Visible = false;
            guna2Button2.Visible = false;
            testButton.Visible = false;
            guna2Button10.Visible = false;
            guna2Button12.Visible = false;
            guna2Panel1.Visible = false;
            guna2Panel2.Visible = false;
            guna2Panel3.Visible = false;
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;
            guna2Panel7.Visible = false;
        }

        private void User_Load(object sender, EventArgs e)
        {
            guna2HtmlLabel5.Text = "Howdy," + loginFormInstance.GetLoggedInUsername() + "!";
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            loginFormInstance.Show();
            this.Dispose();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            guna2Button26.Visible = true;
            guna2Button24.Visible = false;
            guna2Button23.Visible = false;
            guna2Button16.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel15.Visible = false;
            guna2Panel14.Visible = false;
            guna2Panel11.Visible = false;

        }

        private void guna2Button26_Click(object sender, EventArgs e)
        {
            guna2Panel16.Visible = true;
        }

        private void guna2Button25_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label21.Text;
                    int quantity = int.Parse(guna2ComboBox12.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button26.Visible = false;
                    guna2Panel16.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureFamilyMeal_Click(object sender, EventArgs e)
        {
            guna2GroupBox1.Visible = false;
            guna2GroupBox2.Visible = true;
        }

        private void guna2Button22_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label23.Text;
                    int quantity = int.Parse(guna2ComboBox11.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button24.Visible = false;
                    guna2Panel15.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            guna2Button24.Visible = true;
            guna2Button26.Visible = false;
            guna2Button23.Visible = false;
            guna2Button16.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel15.Visible = false;
            guna2Panel14.Visible = false;
            guna2Panel11.Visible = false;
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            guna2Panel15.Visible = true;

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            guna2Button23.Visible = true;
            guna2Button26.Visible = false;
            guna2Button24.Visible = false;
            guna2Button16.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel16.Visible = false;
        }

        private void guna2Button23_Click(object sender, EventArgs e)
        {
            guna2Panel14.Visible = true;
        }

        private void guna2Button21_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label20.Text;
                    int quantity = int.Parse(guna2ComboBox10.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button23.Visible = false;
                    guna2Panel14.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            guna2Button16.Visible = true;
            guna2Button26.Visible = false;
            guna2Button24.Visible = false;
            guna2Button23.Visible = false;
            guna2Panel16.Visible = false;
            guna2Panel15.Visible = false;
            guna2Panel14.Visible = false;
            guna2Panel11.Visible = false;
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            guna2Panel11.Visible = true;
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            if (loginFormInstance != null)
            {
                string username = loginFormInstance.GetLoggedInUsername();
                if (username != null)
                {
                    string foodName = label13.Text;
                    int quantity = int.Parse(guna2ComboBox7.SelectedItem.ToString());
                    int price = foodPrices[foodName] * quantity;
                    bool orderProcessed = ProcessOrder(foodName, quantity);
                    guna2Button16.Visible = false;
                    guna2Panel11.Visible = false;

                    if (orderProcessed)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string insertQuery = "INSERT INTO Orders (UserName, FoodName, Quantity, Price) VALUES (@UserName, @FoodName, @Quantity, @Price)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Ensure @UserName is supplied with a value
                                command.Parameters.AddWithValue("@UserName", username);
                                command.Parameters.AddWithValue("@FoodName", foodName);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                command.ExecuteNonQuery();


                            }
                        }
                        MessageBox.Show($"You ordered {quantity} {foodName}, it cost {price}.");
                    }

                }
                else
                {
                    MessageBox.Show("Username is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LoginFormInstance is null. Cannot proceed with the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintOrderDetails(List<(string FoodName, int Quantity, decimal Price)> orderDetails)
        {
            // Create a PrintDocument object
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, ev) => PrintPageHandler(s, ev, orderDetails);

            // Display the PrintDialog to choose the printer and set other print options
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Print the document
                printDocument.Print();
            }
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e, List<(string FoodName, int Quantity, decimal Price)> orderDetails)
        {
            // Business name
            string businessName = "Foodielivery";

            // Create Fonts for printing
            using (Font fontBusinessName = new Font("Arial", 15, FontStyle.Bold))
            using (Font fontDetails = new Font("Arial", 15))
            using (Font fontTotal = new Font("Arial", 20, FontStyle.Bold))
            {
                // Specify the printing area
                RectangleF area = e.PageSettings.PrintableArea;

                // Set initial positions
                float xPosition = area.X + 10; // Add some left margin
                float yPosition = area.Y + 10; // Add some top margin

                // Print business name in the upper left
                e.Graphics.DrawString(businessName, fontBusinessName, Brushes.Black, xPosition, yPosition);

                // Move to the next line
                yPosition += fontBusinessName.GetHeight() + 100; // Add some space after business name

                // Create a StringBuilder to concatenate the order details
                StringBuilder orderDetailsStringBuilder = new StringBuilder();

                // Add order details
                foreach (var detail in orderDetails)
                {
                    orderDetailsStringBuilder.AppendLine($"Food Name: {detail.FoodName}, Quantity: {detail.Quantity}, Price: ₱{detail.Price:N2}");
                }

                // Add Delivery Fee
                orderDetailsStringBuilder.AppendLine("Delivery Fee: ₱50");

                // Add Total Price
                decimal totalOrderPrice = orderDetails.Sum(detail => detail.Price) + 50;

                // Print order details below business name
                e.Graphics.DrawString(orderDetailsStringBuilder.ToString(), fontDetails, Brushes.Black, xPosition, yPosition);

                // Move to the next line after order details
                yPosition += e.Graphics.MeasureString(orderDetailsStringBuilder.ToString(), fontDetails).Height + 10;

                // Print Total Price with a separate Font object
                e.Graphics.DrawString($"Total Price: ₱{totalOrderPrice:N2}", fontTotal, Brushes.Black, xPosition, yPosition);
            }
        }



        private void guna2Button17_Click(object sender, EventArgs e)
        {
            if (guna2Panel9.Size == new Size(200, 644))
            {
                guna2Panel9.Size = new Size(27, 644);
                this.Size = new Size(993, 683);
            }
            else
            {

                guna2Panel9.Size = new Size(200, 644); // full panel size
                this.Size = new Size(1166, 683); // full form size
            }
        }
    }

}
