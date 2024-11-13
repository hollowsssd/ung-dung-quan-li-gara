using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_car
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int StarP = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            StarP += 5;
            Run.Value = StarP;
            label1PT.Text = StarP + "%";
            if (Run.Value == 100)
            {
                Run.Value = 0;
                Login lgi = new Login();
                lgi.Show();
                this.Hide();
                timer1.Stop();
            }

        }

        private void Run_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
