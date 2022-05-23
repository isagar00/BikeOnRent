using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeRental
{
    public partial class StartWindow : Form
    {
        int startPoint = 0;
        public StartWindow()
        {
            InitializeComponent();
        }

        private void progressTimer_Tick(object sender, EventArgs e)
        {
            // Incrementing startPoint by 1 and assigning to progress bar so progress bar will show progress and once startPoint is 100, it will load login form as progress is completed
            this.startPoint += 1;
            progressBar.Value = this.startPoint;
            progressBar.Text = "" + this.startPoint;
            //Once progress bar is 100, will hide start window and  load login form and reseting progressBar to 0 so that the progress bar should start from 0 next time.  
            if (progressBar.Value == 100)
            {
                progressBar.Value = 0;
                progressTimer.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }    

        private void StartWindow_Load(object sender, EventArgs e)
        {
            progressTimer.Start();
        }
    }
}
