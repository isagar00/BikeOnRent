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
    public partial class Rental : Form
    {
        private SqlConnection connection = null;
        public Rental()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void customerListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rentIdTextBox.Text = bikeRentalListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            bikeRegNoComboBox.SelectedValue = bikeRentalListDataGridView.SelectedRows[0].Cells[1].Value.ToString();                      
            rentFeeTextBox.Text = bikeRentalListDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }     
        private void customerNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void rentalDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void fillBikeRegNo()
        {
            connection.Open();
            String query = "select RegNumber from BikeDetails";
            SqlCommand cmd  = new SqlCommand(query, connection);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("RegNumber", typeof(int));
            dataTable.Load(sqlDataReader);
            bikeRegNoComboBox.ValueMember = "RegNumber";
            bikeRegNoComboBox.DataSource = dataTable;
            connection.Close();
        }

        private void fillCustomerId()
        {
            connection.Open();
            String query = "select CustomerId from CustomerDetails";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Load(sqlDataReader);
            customerIdComboBox.ValueMember = "CustomerId";
            customerIdComboBox.DataSource = dataTable;
            connection.Close();
        }

        private void fetchCustomerName()
        {
            connection.Open();
            String query = "select * from CustomerDetails where CustomerId=" + customerIdComboBox.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, connection);           
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            foreach(DataRow row in dataTable.Rows)
            {
                customerNameTextBox.Text = row["CustomerName"].ToString();
            }
            customerIdComboBox.ValueMember = "CustomerId";
            customerIdComboBox.DataSource = dataTable;
            connection.Close();
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillBikeRegNo();
            fillCustomerId();
            populate();
        }

        private void customerIComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Close();
            fetchCustomerName();
        }

        private void populate()
        {
            connection.Open();
            String query = "select * from RentDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            bikeRentalListDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void updateOnRent()
        {
            connection.Open();
            String query = "update BikeDetails set Available='" + "No" +"' where RegNumber= " + bikeRegNoComboBox.SelectedValue + ";";          
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();            
            connection.Close();
            populate();
        }

        private void updateOnRentDeleted()
        {
            connection.Open();
            String query = "update BikeDetails set Available='" + "Yes" + "' where RegNumber= " + bikeRegNoComboBox.SelectedValue + ";";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            populate();
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            if (rentIdTextBox.Text == ""
               || customerNameTextBox.Text == ""
               || rentFeeTextBox.Text == ""            
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
                        String query = "insert into RentDetails values(" + rentIdTextBox.Text + ",'" + bikeRegNoComboBox.SelectedValue.ToString() + "','" + customerNameTextBox.Text + "','" +rentalDateTimePicker1.Text + "','" + returnDateTimePicker1.Text + "','" + Convert.ToInt32(rentFeeTextBox.Text) + "')";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bike rented  successfully");
                        connection.Close();
                        updateOnRent();
                        populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (rentIdTextBox.Text == "")
            {
                MessageBox.Show("Missing information. Please enter valid id.");
            }
            else
            {
                try
                {
                    connection.Open();
                    String query = "delete from RentDetails where RentId ='" + rentIdTextBox.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rented bike detail deleted successfully");
                    connection.Close();
                    populate();
                    updateOnRentDeleted();
                    updateOnRentDeleted();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (rentIdTextBox.Text == "" || rentFeeTextBox.Text == "" || customerNameTextBox.Text == "" || bikeRegNoComboBox.SelectedItem.ToString() == "" || customerIdComboBox.SelectedItem.ToString() == "")
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
                        String query = "update RentDetails set RentFee='" + rentFeeTextBox.Text + "' where RentId= " + rentIdTextBox.Text + ";";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bike rent updated successfully");
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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void rentIdTextBox_TextChanged(object sender, EventArgs e)
        {

            String query = "select count(*) from RentDetails where RentId ='" + rentIdTextBox.Text + "'";
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
