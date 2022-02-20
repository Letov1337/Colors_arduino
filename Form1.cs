using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using ManagedBass.DirectX8;
namespace Colors_arduino
{
    public partial class Form1 : MaterialForm
    {

        public Form1()
        {
            InitializeComponent();
            // material_skin
            colorDialog1.FullOpen = true;
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            //
        }
        public void com_update()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            if (ports.Length != 0)
            {
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedIndex = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            com_update();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // установка цвета формы
            //this.BackColor = colorDialog1.Color;
            materialLabel1.BackColor = colorDialog1.Color;
            materialLabel1.Text = colorDialog1.Color.ToString();
            materialLabel2.Text = "RGB:" + colorDialog1.Color.R.ToString() + ":" + colorDialog1.Color.G.ToString() + ":" + colorDialog1.Color.B.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            materialLabel3.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Подключиться")
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button1.Text = "Отключиться";
                }
                catch
                {
                    MessageBox.Show("Ошибка подключение");
                }
            }
            else if (button1.Text == "Отключиться")
            {
                serialPort1.Close();
                button1.Text = "Подключиться";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            if (ports.Length != 0)
            {
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            serialPort1.WriteLine("a" + trackBar1.Value.ToString());
            label1.Text = trackBar1.Value.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.Write(comboBox2.SelectedIndex.ToString());
            label1.Text = comboBox2.SelectedIndex.ToString();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("b");
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                Random rnd = new Random();
                int value = rnd.Next(0,255);
                serialPort1.WriteLine("a" + value.ToString());
                label1.Text = trackBar1.Value.ToString();
            }
        }
    }
}
