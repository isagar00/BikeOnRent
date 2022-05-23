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
    public partial class Login : Form
    {
        SqlConnection connection = null;
        public Login()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            connection.Open();           
            String query = "select count(*) from UserDetails where UserName ='" + userNameText.Text + "' and Password='" + passwordText.Text + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable); 
            if(dataTable.Rows[0][0].ToString() == "1")
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter correct credentials Or user does not exist");
            }
            connection.Close();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
