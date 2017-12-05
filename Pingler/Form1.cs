using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;

namespace Pingler
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }


        // Button of add domain names list.
        private void button1_Click(object sender, EventArgs e)
        {
            selectdomain = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            progressBar1.Value = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader oku = new StreamReader(openFileDialog1.FileName);
                string satir = oku.ReadLine();


                //Warning if text file is empty.
                if (satir == null)
                {
                    MessageBox.Show("You can not select an empty file.", "Selected File Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                while (satir != null)
                {
                    pingbtn.Enabled = true;
                    listBox1.Items.Add(satir);
                    satir = oku.ReadLine();
                }

            }

        }

        private void pingbtn_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        //Selected domain name value.
        int selectdomain;

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Enabled = false;
            pingbtn.Enabled = false;
            int alldomainsnumber = listBox1.Items.Count;
            progressBar1.Maximum = alldomainsnumber;
            listBox1.SelectedIndex = selectdomain;
            islemlabel.Text = "In the process: " + Convert.ToString(listBox1.SelectedItem);
            progressBar1.Value += 1;

            try
            {
                Ping pingdomain = new Ping();
                PingReply replydomain = pingdomain.Send(Convert.ToString(listBox1.SelectedItem), 1000);
                if (replydomain != null)
                {
                    listBox2.Items.Add(Convert.ToString(listBox1.SelectedItem) + " | " + replydomain.Address);
                }
                
            }

            catch
            {
                listBox2.Items.Add(Convert.ToString(listBox1.SelectedItem) + " | Error!");
            }

            selectdomain++;
            if (selectdomain == alldomainsnumber)
            {
                timer1.Stop();
                islemlabel.Text = "In the process: " + Convert.ToString(listBox1.SelectedItem);
                timer2.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pingbtn.Enabled = false;
            rprbtn.Enabled = false;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            islemlabel.Text = "The process is complete.";
            timer2.Stop();
            listBox1.Items.Clear();
            pingbtn.Enabled = false;
            rprbtn.Enabled = true;
            button1.Enabled = true;
        }

        private void rprbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon :] ");
        }
    }
}
