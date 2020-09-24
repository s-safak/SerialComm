using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTSerialCom
{
    public partial class Form1 : Form
    {
        SerialClient serial1;
        int storedResponses;
        int maxLen;
        int minLen;
        int currentLen;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Proccessed (Bytes)");
            listBox1.Items.Add("0 Bs"); /*1*/
            listBox1.Items.Add("");
            listBox1.Items.Add("Length (Bytes)");
            listBox1.Items.Add("--------------------------");
            listBox1.Items.Add("Max");
            listBox1.Items.Add("0"); /*6*/
            listBox1.Items.Add("Min");
            listBox1.Items.Add("0"); /*8*/
            listBox1.Items.Add("Current");
            listBox1.Items.Add("0"); /*10*/
            listBox1.Items.Add("Value");
            listBox1.Items.Add("0"); /*12*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serial1 = new SerialClient("COM5", 9600);
            serial1.OnReceiving += new EventHandler<DataStreamEventArgs>(receiveHandler);
            if (!serial1.OpenConn())
            {
                MessageBox.Show(this, "The Port Cannot Be Opened", "Serial Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void receiveHandler(object sender, DataStreamEventArgs e)
        {
            storedResponses++;
            minLen = minLen > e.Response.Length ? e.Response.Length : minLen;
            maxLen = maxLen < e.Response.Length ? e.Response.Length : maxLen;
            currentLen = e.Response.Length;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serial1.CloseConn();
            serial1.OnReceiving -= new EventHandler<DataStreamEventArgs>(receiveHandler);
            serial1.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items[1] = storedResponses.ToString();
            listBox1.Items[6] = maxLen.ToString();
            listBox1.Items[8] = minLen.ToString();
            listBox1.Items[10] = currentLen.ToString();
        }
    }
}
