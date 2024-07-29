using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AEB
{
    public partial class AC : Form
    {
        public AC()
        {
            InitializeComponent();
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {

            new Form1().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AC().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Music().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Map().Show();
            this.Hide();
        }
    }
}
