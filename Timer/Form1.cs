using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;
        string strTime = "";
        string startFolder = AppDomain.CurrentDomain.BaseDirectory;
        List<string> lines = new List<string>();
        public Form1()
        {
            InitializeComponent();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes == 60)
            {
                hours++;
                minutes = 0;
            }
            lbl_hour.Text = hours.ToString();
            lbl_minute.Text = minutes.ToString();
            lbl_second.Text = seconds.ToString();
            strTime = $"{hours.ToString()}h {minutes.ToString()}m {seconds.ToString()}s";
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            btn_save.Enabled = false;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            hours = 0;
            minutes = 0;
            seconds = 0;
            btn_save.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add($"{textBox1.Text} {strTime} {DateTime.Now.ToString("dd.MM.yyyy")}");
            if (!File.Exists($"{startFolder}/logs.txt"))
            {
                File.WriteAllText($"{startFolder}/logs.txt",
                $"{textBox1.Text} {strTime} {DateTime.Now.ToString("dd.MM.yyyy")}");
            }

            if (File.Exists($"{startFolder}/logs.txt"))
            {
                //writing part
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(startFolder, "logs.txt")))
                {
                    foreach (var item in listBox1.Items)
                        outputFile.WriteLine(item.ToString());
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
        }

    }
}