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
    public partial class Dashboard : Form
    {
        private SqlConnection connection = null;
        public Dashboard()
        {
            InitializeComponent();
            this.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sagar\source\repos\BikeRental\BikeRental\BikeRental.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            String queryBike = "select Count(*) from BikeDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(queryBike, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            bikesCountLabel.Text = dt.Rows[0][0].ToString();


            String customerCount = "select Count(*) from CustomerDetails";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(customerCount, connection);
            DataTable data = new DataTable();
            dataAdapter.Fill(data);
            customersLabel.Text = data.Rows[0][0].ToString();


            String usersCount = "select Count(*) from UserDetails";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(usersCount, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            usersLabel.Text = dataTable.Rows[0][0].ToString();

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void lable_Click(object sender, EventArgs e)
        {

        }

        private void usersLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
