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
    public partial class Users : Form
    {
        private SqlConnection connection = null;
        public Users()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void populate()
        {
            connection.Open();
            String query = "select * from UserDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            userListDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void Users_Load(object sender, EventArgs e)
        {
           populate();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (userIdTextBox.Text == "" || userNameTextBox.Text == "" || passwordTextBox.Text == "")
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
                        String query = "insert into UserDetails values(" + userIdTextBox.Text + ",'" + userNameTextBox.Text + "','" + passwordTextBox.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User added successfully");
                        connection.Close();
                        populate();
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (userIdTextBox.Text == "")
            {
                MessageBox.Show("Missing information. Please enter valid id.");
            }
            else
            {
                try
                {
                    connection.Open();
                    String query = "delete from UserDetails where Id ='" + userIdTextBox.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully");
                    connection.Close();
                    populate();
                    userIdTextBox.Text = "";
                    userNameTextBox.Text = "";
                    passwordTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (userIdTextBox.Text == "" || userNameTextBox.Text == "" || passwordTextBox.Text == "")
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
                        String query = "update UserDetails set UserName='" + userIdTextBox.Text + "',Password='" + passwordTextBox.Text +"' where Id= "+userIdTextBox.Text+";";                                                                                                 
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User updated successfully");
                        connection.Close();
                        populate();
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
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

        private void userListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            userIdTextBox.Text = userListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            userNameTextBox.Text = userListDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            passwordTextBox.Text = userListDataGridView.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void userIdTextBox_TextChanged(object sender, EventArgs e)
        {

            String query = "select count(*) from UserDetails where Id ='" + userIdTextBox.Text + "'";
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
