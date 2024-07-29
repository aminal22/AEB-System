using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace AEB
{
    public partial class WarningForm : Form
    {
        public WarningForm()
        {
            InitializeComponent();
        }

        private void WarningForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Danger! A vehicle is approaching.";
            // Customize the message or add additional logic if needed.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
