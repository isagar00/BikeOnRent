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

namespace BikeRental
{
    public partial class Customer : Form
    {
        private SqlConnection connection = null;
        public Customer()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void populate()
        {
            connection.Open();
            String query = "select * from CustomerDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            customerListDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (customerIdTextBox.Text == ""
               || customerNameTextBox.Text == ""
               || addressTextBox.Text == ""
               || phoneTextBox.Text == ""
               )
            {
                MessageBox.Show("Missing information. Empty values are not allowed.");
            }
            else
            {
                try
                {
                    if (connection != null)
                    {
                        connection.Open();
                        String query = "insert into CustomerDetails values(" + customerIdTextBox.Text + ",'" + customerNameTextBox.Text + "','" + addressTextBox.Text + "','" + phoneTextBox.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bike added successfully");
                        connection.Close();
                        populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (customerIdTextBox.Text == ""
              || customerNameTextBox.Text == ""
              || addressTextBox.Text == ""
              || phoneTextBox.Text == ""
              )
            {
                MessageBox.Show("Missing information. Empty values are not allowed.");
            }
            else
            {
                try
                {
                    if (connection != null)
                    {
                        connection.Open();
                        String query = "update CustomerDetails set CustomerName='" + customerNameTextBox.Text + "',Address='" + addressTextBox.Text + "',Phone='" + phoneTextBox.Text + "' where CustomerId= " + customerIdTextBox.Text + ";";                        
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer updated successfully");
                        connection.Close();
                        populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void customerListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            customerIdTextBox.Text = customerListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            customerNameTextBox.Text = customerListDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            addressTextBox.Text = customerListDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            phoneTextBox.Text = customerListDataGridView.SelectedRows[0].Cells[3].Value.ToString();           
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (customerIdTextBox.Text == "")
            {
                MessageBox.Show("Missing information. Please enter valid id.");
            }
            else
            {
                try
                {
                    connection.Open();
                    String query = "delete from CustomerDetails where CustomerId ='" + customerIdTextBox.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully");
                    connection.Close();
                    populate();
                    customerIdTextBox.Text = "";
                    customerNameTextBox.Text = "";
                    addressTextBox.Text = "";
                    phoneTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customerIdTextBox_TextChanged(object sender, EventArgs e)
        {
            String query = "select count(*) from CustomerDetails where CustomerId ='" + customerIdTextBox.Text + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows[0][0].ToString() == "1")
            {
                addButton.Enabled = false;
            }
            else
            {
                addButton.Enabled = true;
            }
        }

       
    }
}
