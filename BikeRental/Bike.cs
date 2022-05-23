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
    public partial class Bike : Form
    {
        private SqlConnection connection = null;

        public Bike()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
      
        private void populate()
        {
            connection.Open();
            String query = "select * from BikeDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query,connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            bikeListDataGridView.DataSource = ds.Tables[0];          
            connection.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(regNoTextBox.Text == "" 
               || brandTextBox.Text == "" 
               || modelTextBox.Text == "" 
               || priceTextBox.Text == ""
               ) {
                MessageBox.Show("Missing information. Empty values are not allowed.");
            }
            else
            {
                try
                {
                    if(connection != null)
                    {
                        connection.Open();
                        String query  = "insert into BikeDetails values(" + regNoTextBox.Text + ",'" + brandTextBox.Text + "','" + modelTextBox.Text + "','" + availableComboBox.SelectedItem.ToString() + "','" + priceTextBox.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bike added successfully");
                        connection.Close();
                        populate();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void Bike_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (regNoTextBox.Text == "")
            {
                MessageBox.Show("Missing information. Please enter valid id.");
            }
            else
            {
                try
                {
                    connection.Open();
                    String query = "delete from bikeDetails where RegNumber ='" + regNoTextBox.Text +"';";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bike deleted successfully");
                    connection.Close();
                    populate();
                    regNoTextBox.Text = "";
                    brandTextBox.Text = "";
                    modelTextBox.Text = "";
                    priceTextBox.Text = "";
                    availableComboBox.SelectedValue = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (regNoTextBox.Text == "" || brandTextBox.Text == "" || modelTextBox.Text == "")
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
                        String query = "update BikeDetails set Brand='"+ brandTextBox.Text + "',Model='" + modelTextBox.Text + "',Available='" + availableComboBox.SelectedItem.ToString() + "',Price='" + priceTextBox.Text + "'where RegNumber= " + regNoTextBox.Text +";";                        
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bike updated successfully");
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

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void bikeListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            regNoTextBox.Text = bikeListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            brandTextBox.Text = bikeListDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            modelTextBox.Text = bikeListDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            availableComboBox.SelectedItem = bikeListDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            priceTextBox.Text = bikeListDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void regNoTextBox_TextChanged(object sender, EventArgs e)
        {
            String query = "select count(*) from BikeDetails where RegNumber ='" + regNoTextBox.Text + "'";
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
