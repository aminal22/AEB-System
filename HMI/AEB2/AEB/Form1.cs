using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace AEB
{
    public partial class Form1 : Form
    {
        private int speed =90; // Initialize speed value
        private Font panelFont; // Font for the panel text

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Allow the form to capture key presses
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            panelFont = new Font(this.Font.FontFamily, 24, FontStyle.Bold); // Set the font size to 24 and make it bold
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

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            if (panel2 != null)
            {
                e.Graphics.DrawString(panel2.Text, panelFont, Brushes.Black, new PointF(10, 10));
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            if (panel1 != null)
            {
                e.Graphics.DrawString(panel1.Text, panelFont, Brushes.Black, new PointF(10, 10));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Logic for handling comboBox1 selection change, if any
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1 == null || comboBox1 == null)
            {
                return;
            }

            if (checkBox1.Checked)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    serialPort1.DataReceived += serialPort1_DataReceived;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox1.Checked = false; // Revert the check if there was an error
                }
            }
            else
            {
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.DataReceived -= serialPort1_DataReceived;
                        serialPort1.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort1.ReadLine();
                string[] values = data.Split(',');

                // Assuming values[0] for panel1 and values[1] for panel2
                if (values.Length >= 2)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            UpdatePanelText(values);
                        }));
                    }
                    else
                    {
                        UpdatePanelText(values);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePanelText(string[] values)
        {
            if (panel1 != null && panel2 != null)
            {
                panel1.Text = values[0];
                panel2.Text = values[1];
                panel1.Invalidate(); // Trigger a repaint
                panel2.Invalidate(); // Trigger a repaint
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Serial Ports
            string[] ports = SerialPort.GetPortNames();
            if (comboBox1 != null)
            {
                comboBox1.Items.AddRange(ports);
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }

            if (checkBox1 != null)
            {
                checkBox1.Checked = false;
            }

            // Set default values for panel texts
            if (panel1 != null && panel2 != null)
            {
                panel1.Text = speed.ToString();
                panel2.Text = "3";

                panel1.Invalidate(); // Trigger a repaint
                panel2.Invalidate(); // Trigger a repaint
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (panel1 == null) return;

            if (e.KeyCode == Keys.A)
            {
                speed = Math.Min(speed + 5, 200); // Increase speed but limit to 200
            }
            else if (e.KeyCode == Keys.Space)
            {
                speed = Math.Max(speed - 5, 0); // Decrease speed but limit to 0
            }
            else if (e.KeyCode == Keys.E)
            {
                ShowWarningMessage();
            }

            panel1.Text = speed.ToString();
            panel1.Invalidate(); // Trigger a repaint
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
      

        private void ShowWarningMessage()
        {
            if (warningLabel != null)
            {
                warningLabel.Text = "Danger! A vehicle is approaching.";
                warningLabel.Visible = true;
                warningTimer.Start();
            }
        }

        private void warningTimer_Tick(object sender, EventArgs e)
        {
            if (warningLabel != null)
            {
                warningLabel.Visible = false;
            }
            warningTimer.Stop();
        }
    }
}
