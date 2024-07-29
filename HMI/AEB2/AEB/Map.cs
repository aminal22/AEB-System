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
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com/maps");

            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs
         e)
        {
            if (webBrowser1.Document
         != null)
            {
                // Inject JavaScript to search for Rabat, Morocco
                webBrowser1.Document.InvokeScript("javascript:void(document.querySelector('#searchbox input').value='Rabat, Morocco'; document.querySelector('#searchbox button').click();");
            }
        }



        private void Dashboard_Click_1(object sender, EventArgs e)
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
